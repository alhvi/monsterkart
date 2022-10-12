using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midLap : MonoBehaviour
{
    public GameObject midlap;
    public GameObject lap;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lap.SetActive(true);
            midlap.SetActive(false);
        }
    }

}
