﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float speed;


    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime,0 );
    }
}
