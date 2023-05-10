using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject levelPanel;//Reference to the Level Panel
    public GameObject mainMenuPanel;//Reference to the Main Menu Panel
    public GameObject pausePanel;
    public GameObject settingspanel;
    public Animator transition;
  
    public void ActivateLevelPanel()//Function to activate Level Panel
    {
        mainMenuPanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    public void DeactivateLevelPanel()// Functin to deactivate Level Panel
    {
        mainMenuPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void LoadLevelnext(int level)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(level));
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        
    }
    public void ActivateSettingPanel()
    {
        mainMenuPanel.SetActive(false);
        settingspanel.SetActive(true);
    }
    public void DeactivateSettingPanel()
    {
        mainMenuPanel.SetActive(true);
        settingspanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    IEnumerator LoadLevel(int levelindex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelindex);
    }
}
