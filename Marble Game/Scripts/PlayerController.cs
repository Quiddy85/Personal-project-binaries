using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variables
    public Rigidbody marbleRb;
    private float force = 1000;
    private float timerSeconds = 5;
    [SerializeField] int gameTimer = 300;
    [SerializeField] int addTimer = 2;
    [SerializeField] int minusTimer = 10;
    private AudioSource playerAudio;
    public int gemsCollected;
    public bool hasPowerup = false;

    [SerializeField] ParticleSystem sparkParticles;
    [SerializeField] ParticleSystem enemyDeathParticles;

    [SerializeField] AudioClip bumpSound;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] AudioClip gemsCollectSound;
    [SerializeField] AudioClip powerupCollectSound;

    [SerializeField] TextMeshProUGUI gemsText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;

    [SerializeField] GameObject powerupIndicator;
    [SerializeField] Vector3 powerupIndicatorOffset = new Vector3(0, -0.5f, 0);



    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        marbleRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // Start game timer
        InvokeRepeating("GameTimer", 0, 1);

        // Push the marble into physics existance
        marbleRb.AddTorque(Vector3.right * force);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the powerup indicator with the player
        powerupIndicator.transform.position = transform.position + powerupIndicatorOffset;

        // Get axis
        float horizonalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Apply torque when player moves the stick
        if (gameTimer > 0)
        {
            marbleRb.AddTorque(Vector3.right * force * horizonalInput, ForceMode.Impulse);
            marbleRb.AddTorque(Vector3.forward * force * verticalInput, ForceMode.Impulse);
        }

        // Game over
        if (gameTimer <= 0)
        {
            addTimer = 0;
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Debug.Log("Game Over!");
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        // Display time and gems
        gemsText.SetText("Gems: " + gemsCollected);
        timerText.SetText("Time: " + gameTimer);

        // Put marble back on ground if fall
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }

    // Collect powerups, collect coins, destroy enemies with powerup
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupCollectSound, 1.0f);
            gameTimer += addTimer * 5; // If player hits powerup add x seconds
            hasPowerup = true;
            StartCoroutine(PowerupTimer());
            powerupIndicator.SetActive(true);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(gemsCollectSound, 1.0f);
            gameTimer += addTimer; // If player hits coin add x seconds
            gemsCollected++; // add to coin score
        }
        
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(enemyDeathSound, 1.0f);
            Instantiate(enemyDeathParticles, other.transform.position, other.transform.rotation);
            Invoke("DestroyEnemyDeathPatrticle", 10f);

        }
    }

    // Destroy the enemy death particle
    void DestroyEnemyDeathPatrticle()
    {
        Destroy(GameObject.FindGameObjectWithTag("Particle"));
    }

    // Activate sparks and bump sounds
    private void OnCollisionEnter(Collision collision)
    {
        // play particles and sounds
        sparkParticles.Play();
        playerAudio.PlayOneShot(bumpSound, 1.0f);

        // If players hits a robot lose x seconds
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameTimer -= minusTimer;
        }
    }

    // Powerup timer
    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(timerSeconds);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Game timer
    void GameTimer()
    {
        if (gameTimer > 0)
        {
            gameTimer--;
        }
    }

    // Restart the game when mouse clicks button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
