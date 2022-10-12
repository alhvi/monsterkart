using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public List<GameObject> wheels;
    public float wheelSpeed = 150;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Accelerate"))
        {
            foreach (GameObject W in wheels)
            {
                W.transform.Rotate(0, 0, -wheelSpeed);
            }

        }
    }
}
