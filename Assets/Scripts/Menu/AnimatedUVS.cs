using UnityEngine;
using System.Collections;
using UnityEngine;

public class AnimatedUVS : MonoBehaviour
{

    public float Speed = 0.75f;

    public Renderer Renderer;
    public bool X = false;
    public bool Y = false;


    void Start()
    {
        Renderer = GetComponent<Renderer>();
    }

    void Update()
    {

        float TextureOffset = Time.time * Speed;
        if (X == true) { 
        Renderer.material.SetTextureOffset("_MainTex", new Vector2(TextureOffset, 0));
         }
        if (Y == true)
        {
            Renderer.material.SetTextureOffset("_MainTex", new Vector2(0, TextureOffset));
        }
    }

}