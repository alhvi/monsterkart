using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimatedUI : MonoBehaviour
{
    public float Speed = 0.75f;

    public bool X = false;
    public Image maae;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float TextureOffset = Time.time * Speed;
        if (X == true)
        {
            maae.material.SetTextureOffset("_MainTex", new Vector2(TextureOffset, 0));
        }
    }
}
