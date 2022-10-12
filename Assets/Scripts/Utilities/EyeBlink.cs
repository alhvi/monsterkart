//Anima los las texturas de los ojos en los personajes :) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour {
    public Material eyeMaterial;
    public Texture EyeIdleT;
    public Texture EyeCloT;
    public float maxTime = 5;
    public float minTime = 2;

    private float time;

    private float spawnTime;

    void Start()
    {
        SetRandomTime();
        time = minTime;
    }

    void FixedUpdate()
    {

        time += Time.deltaTime;

        if (time >= spawnTime)
        {
            Wink();
            SetRandomTime();
        }

    }


    void Wink()
    {
        time = 0; 
        eyeMaterial.mainTexture = EyeCloT;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        float wait_time = Random.Range(0.1f, 0.3f);
        yield return new WaitForSeconds(wait_time);
        eyeMaterial.mainTexture = EyeIdleT;
    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}
