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

    //Game State Variables
    public bool baseDestroyed = false;
    public bool levelComplete = false;
    public bool waveComplete = false;

    public BaseScript BaseDestroyed;

    private void Update()
    {
        if (BaseDestroyed)
        {
            LoseGame();
        }

        if (levelComplete)
        {
            LevelComplete();
        }

        if (waveComplete)
        {
            WaveComplete();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NextWave()
    {
        //next wave logic (enemy spawner etc.)
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
    }

    public void LoseGame()
    {
        LoseScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Returning to Menu Scene...");
        SceneManager.LoadScene("Title_Scene");
    }
}
