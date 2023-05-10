using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{

    public int lifetoDecrease = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Banana":
            case "Apple":
            case "Watermelon":
            case "Cherry":
            case "Mango":
            case "Strawberry":
            case "Pineapple":
            case "Grapes":
            case "Orange":
            case "Blackberries":
            case "Blueberries":
            case "Dragonfruit":
            case "Plump":
            case "Pear":
                if (!GameManager.Instance.isShieldActive)
                {
                    if(GameManager.Instance.mode == 1)
                    {
                       // Add score for collecting a fruit
                       GameManager.Instance.DecreaseLife(lifetoDecrease);
                    }

                    // Destroy the collected fruit
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("Shield Active");
                    Destroy(collision.gameObject);
                }
                break;
            case "UnwantedItem":
            case "Heart":
            case "Magnet":
            case "Shield":
            case "ScoreMultiplier":
                Destroy(collision.gameObject);
                break;
        }
    }
           
    // Update is called once per frame
    void Update()
    {
       

    }
}
