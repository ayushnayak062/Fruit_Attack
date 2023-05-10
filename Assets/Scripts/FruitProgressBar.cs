using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitProgressBar : MonoBehaviour
{
    public FruitCollector fruitCollector;
    public Image totalFruitBar;
    public Image requiredFruitBar;

    public float totalFruitTargetFill;
    public float requiredFruitTargetFill;

    private void Start()
    {
        // Get the initial fill amounts for both progress bars
        totalFruitTargetFill = totalFruitBar.fillAmount;
        requiredFruitTargetFill = requiredFruitBar.fillAmount;
    }

    private void Update()
    {
        getFillFruit();
    }

    void getFillFruit()
    {

        // Calculate the fill amount for the total fruit progress bar
        float totalFruitFill = (float)FruitCollector.totalFruits / fruitCollector.requiedFruits;
        totalFruitFill = Mathf.Clamp01(totalFruitFill);
        totalFruitTargetFill = Mathf.MoveTowards(totalFruitTargetFill, totalFruitFill, Time.deltaTime);

        // Calculate the fill amount for the required fruit progress bar
        float requiredFruitFill = 0f;
        foreach (var fruitInfo in fruitCollector.fruitsToWin)
        {
            float fruitFill = (float)fruitCollector.fruitsCollected[fruitInfo.fruitTag] / fruitInfo.numFruitsToWin;
            requiredFruitFill += fruitFill;
        }
        requiredFruitFill /= fruitCollector.fruitsToWin.Count;
        requiredFruitFill = Mathf.Clamp01(requiredFruitFill);
        requiredFruitTargetFill = Mathf.MoveTowards(requiredFruitTargetFill, requiredFruitFill, Time.deltaTime);

        // Update the fill amounts of both progress bars
        totalFruitBar.fillAmount = totalFruitTargetFill;
        requiredFruitBar.fillAmount = requiredFruitTargetFill;
    }

    
}
