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
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject LevelCompleteScreen;
    public GameObject WaveCompleteScreen;

    public BaseScript baseScript;
    public EnemySpawnerScript enemySpawnerScript;

    private void Awake()
    {
        baseScript = FindObjectOfType<BaseScript>();
        enemySpawnerScript = FindObjectOfType<EnemySpawnerScript>();
    }

    private void Update()
    {
        if (baseScript.baseDestroyed)
        {
            LoseGame();
        }

        //if (enemySpawnerScript.levelComplete)
        //{
        //    LevelComplete();
        //}

        if (enemySpawnerScript.gameWon)
        {
            WinGame();
        }

        //show wave UI once per wave
        if (enemySpawnerScript.waveComplete && !WaveCompleteScreen.activeSelf && !enemySpawnerScript.gameWon)
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
        WaveCompleteScreen.SetActive(false);
        enemySpawnerScript.StartNextWave();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelComplete()
    {
        LevelCompleteScreen.SetActive(true);
    }

    public void WaveComplete()
    {
        WaveCompleteScreen.SetActive(true);
        
        //pause game
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        //pause game
        Time.timeScale = 0f;
        LoseScreen.SetActive(true);
    }

    public void WinGame()
    {
        //pause game
        Time.timeScale = 0f;
        WinScreen.SetActive(true);
    }

    public void QuitGame()
    {
        //resume game
        Time.timeScale = 1f;
        Debug.Log("Returning to Menu Scene...");
        SceneManager.LoadScene("Title_Scene");
       
    }
}
