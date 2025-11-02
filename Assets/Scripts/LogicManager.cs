using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class LogicManager : MonoBehaviour
{
    //public GameObject winLevelScreen;
    public GameObject LoseScreen;
    public GameObject LevelCompleteScreen;
    public GameObject WaveCompleteScreen;

    public bool currentLevel;


    public void NextLevel()
    {
        if (currentLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LevelComplete()
    {
        LevelCompleteScreen.SetActive(true);
    }

    public void WaveComplete()
    {
        WaveCompleteScreen.SetActive(true);

        Time.timeScale = 0f;

        //Start Next Wave Logic
    }

    public void LoseGame()
    {
        LoseScreen.SetActive(true);

    }

    public void QuitGame()
    {
        Debug.Log("Returning to Menu Scene...");
        SceneManager.LoadScene("MenuScene");
    }
}
