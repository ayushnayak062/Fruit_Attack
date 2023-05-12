using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;
    
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2 (1f, 0f) * speed * Time.deltaTime);
    }
}
