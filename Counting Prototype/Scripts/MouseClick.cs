using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    Vector3 cubeBoxPosition = new Vector3(0, 5, -15);
    Vector3 sphereBoxPosition = new Vector3(0, 5, -5);
    Vector3 capsuleBoxPosition = new Vector3(0, 5, 5);
    Vector3 cylinderBoxPosition = new Vector3(0, 5, 15);

    public Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

    float spawnOffsetX = 1;
    float spawnOddsetY = 1f;
    float spawnOffsetZ = 1;

    bool justSpawned = true;

    AudioSource audioSource;
    public AudioClip clickSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (justSpawned == true)
        {
            if (transform.position.x > 10)
            {
                Destroy(gameObject);
            }

            if (transform.position.x < 5)
            {
                Destroy(gameObject);
            }

            if (transform.position.z < -16)
            {
                Destroy(gameObject);
            }

            if (transform.position.z > 16)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        audioSource.PlayOneShot(clickSound, 1.0f);

        justSpawned = false;
        spawnOffsetX = Random.Range(-spawnOffsetX, spawnOffsetX);
        spawnOffsetZ = Random.Range(-spawnOffsetZ, spawnOffsetZ);

        Vector3 spawnOffset = new Vector3(spawnOffsetX, spawnOddsetY, spawnOffsetZ);

        if (gameObject.tag == "Cube")
        {
            transform.position = cubeBoxPosition + spawnOffset;
            transform.localScale = scale;
        }

        if (gameObject.tag == "Sphere")
        {
            transform.position = sphereBoxPosition + spawnOffset;
            transform.localScale = scale;
        }

        if (gameObject.tag == "Capsule")
        {
            transform.position = capsuleBoxPosition + spawnOffset;
            transform.localScale = scale;
        }

        if (gameObject.tag == "Cylinder")
        {
            transform.position = cylinderBoxPosition + spawnOffset;
            transform.localScale = scale;
        }
    }
}
