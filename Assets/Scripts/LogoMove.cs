using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMove : MonoBehaviour
{
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, 2.65f, transform.position.z) , 2f * Time.deltaTime);

        /*if (transform.position.y == 2.73f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2f, 0f), 1f * Time.deltaTime);
        }*/
    }

   
}
