using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickReset()
    {
        PlayerPrefs.DeleteKey("levelReached");
        Debug.Log("!");
    }
    public void OnclickResetHighScore()
    {
        PlayerPrefs.DeleteKey("Highscore");
    }
}
