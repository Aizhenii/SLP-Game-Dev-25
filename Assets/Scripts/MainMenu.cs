using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject mainMenuPanel;
    public bool isDisplayed = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void DisplayRules()
    {
        Debug.Log("Rules Displayed");
        mainMenuPanel.SetActive(false);
        rulesPanel.SetActive(true);

        isDisplayed = true;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Update()
    {
        // Rules Panel is Displayed and User Presses Escape
        if (isDisplayed && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Rules Screen Closed");

            rulesPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            isDisplayed = false;
        }
    }

}
