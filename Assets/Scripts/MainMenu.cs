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

    [SerializeField] private AudioClip hitMainBTN; //sound for pushing the button
    private AudioSource audioSource; //to play sound effects

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize   
    }//end of Start

    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1_Scene");
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
        //detect user click, can modify if you want -Elsa
        //only does sound features
        if (Input.GetMouseButton(0)){ //left click
            //play sound when button is pressed
            if (hitMainBTN != null)
                audioSource.PlayOneShot(hitMainBTN, 0.25f);
        }//end of if

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
