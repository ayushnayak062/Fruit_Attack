using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float screenHalfWidth;

    void Start()
    {
        // Calculate half width of the screen in world coordinates
        float halfPlayerWidth = transform.localScale.x / 2;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Screen wrap
        if (transform.position.x > screenHalfWidth)
        {
            WrapScreen(-screenHalfWidth);
        }
        else if (transform.position.x < -screenHalfWidth)
        {
            WrapScreen(screenHalfWidth);
        }
    }

    void WrapScreen(float targetX)
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        if (currentPosition.x > screenHalfWidth) // Moving right, wrap to left
        {
            transform.position = targetPosition + Vector3.right * (currentPosition.x - screenHalfWidth);
        }
        else if (currentPosition.x < -screenHalfWidth) // Moving left, wrap to right
        {
            transform.position = targetPosition + Vector3.left * (-currentPosition.x - screenHalfWidth);
        }
    }
}





