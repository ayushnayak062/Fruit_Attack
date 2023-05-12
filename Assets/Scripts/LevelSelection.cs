using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelReachedFestival", 1);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
                lvlButtons[i].interactable = false;
        }
        int levelAtc = PlayerPrefs.GetInt("levelReachedCreamPie", 1);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAtc)
                lvlButtons[i].interactable = false;
        }
    }

}

