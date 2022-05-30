using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsArray;
    public GameObject sensor;

    public Button restartButton;
    public Text gameOverText;

    int arrayIndex;
    int gameTimer = 60;

    float randomZ = 15;
    float randomSpawnZ;
    float spawnHeight = 1;
    float spawnDelay = 0.1f;

    bool gameActive = true;

    public Text gameTimerText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjects", 0, spawnDelay);
        InvokeRepeating("GameTimer", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects()
    {
        if (gameActive)
        {
            arrayIndex = Random.Range(0, objectsArray.Length);
            randomSpawnZ = Random.Range(-randomZ, randomZ);

            Instantiate(objectsArray[arrayIndex], new Vector3(7, spawnHeight, randomSpawnZ), objectsArray[arrayIndex].transform.rotation);
        }
    }

    void GameTimer()
    {
        gameTimer--;
        gameTimerText.text = "Time : " + gameTimer;

        if (gameTimer <= 0)
        {
            gameActive = false;
            sensor.SetActive(true);
            gameTimer = 0;

            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
