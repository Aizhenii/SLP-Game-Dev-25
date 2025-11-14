using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseOverlay;
    private bool isPaused = false;

    public GameObject PausedRulesPanel;
    private bool isRulesDisplayed = false;

    [SerializeField] private AudioClip hitPauseBTN; //sound for pushing the button
    private AudioSource audioSource; //to play sound effects

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize   
    }//end of Start

    // Update is called once per frame
    void Update()
    {
        //detect user click, can modify if you want -Elsa
        //only does sound features
        if (Input.GetMouseButton(0)){ //left click
            //play sound when button is pressed
            if (hitPauseBTN != null)
                audioSource.PlayOneShot(hitPauseBTN, 0.25f);
        }//end of if

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else if (isRulesDisplayed)
                CloseRules();
            else
                Resume();
        }
    }

    public void Pause()
    {
        PauseOverlay.SetActive(true);
        PausedRulesPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        isRulesDisplayed = false;
    }

    public void Resume()
    {
        PauseOverlay.SetActive(false);
        PausedRulesPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isRulesDisplayed = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title_Scene");
    }

    public void DisplayRules()
    {
        Debug.Log("Rules Displayed");
        PauseOverlay.SetActive(false);
        PausedRulesPanel.SetActive(true);
        isRulesDisplayed = true;
    }

    public void CloseRules()
    {
        Debug.Log("Rules Closed");
        PausedRulesPanel.SetActive(false);
        PauseOverlay.SetActive(true);
        isRulesDisplayed = false;
    }
}
