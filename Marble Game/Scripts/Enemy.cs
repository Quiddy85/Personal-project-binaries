using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 1;
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float maxSpeed = 10f;
    float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy positions an player positions
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 enemyPos = new Vector3(transform.position.x, 0, transform.position.z);

        // Move enemy towards player
        transform.forward = (playerPos - enemyPos);
        transform.Translate((Vector3.forward * randomSpeed) * Time.deltaTime);
    }
}
