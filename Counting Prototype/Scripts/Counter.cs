using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text cubeCounterText;
    public Text sphereCounterText;
    public Text capsuleCounterText;
    public Text cylinderCounterText;


    private int cubeCount = 0;
    private int sphereCount = 0;
    private int capsuleCount = 0;
    private int cylinderCount = 0;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Cube Box"))
        {
            cubeCount += 1;
            cubeCounterText.text = "Cubes : " + cubeCount;
        }

        if (gameObject.CompareTag("Sphere Box"))
        {
            sphereCount += 1;
            sphereCounterText.text = "Spheres : " + sphereCount;
        }

        if (gameObject.CompareTag("Capsule Box"))
        {
            capsuleCount += 1;
            capsuleCounterText.text = "Capsules : " + capsuleCount;
        }

        if (gameObject.CompareTag("Cylinder Box"))
        {
            cylinderCount += 1;
            cylinderCounterText.text = "Cylinders : " + cylinderCount;
        }
    }
}
