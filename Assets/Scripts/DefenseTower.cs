using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour{
    [Header("Defense Tower Stats")]
    public float health = 150f; //enemy attacks at 10 per hit
    private bool isDestroyed = false; //check if tower is out of health
    [SerializeField] private AudioClip damageSound; //sound for damaged tower
    [SerializeField] private AudioClip deathSound; //sound for dead tower
    private AudioSource audioSource; //to play sound effects

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize   
    }//end of Start

    // Update is called once per frame
    void Update(){
        if (isDestroyed){
            return; //do nothing, tower is gone
        }//end of if
    }//end of Update

    public void attacked(float attackAmount){
        //play sound effect for damage at 50% volume
        if (damageSound != null)
            audioSource.PlayOneShot(damageSound, 0.5f);

        Debug.Log("Defense Tower Hit");
        if (isDestroyed){
            return; //do nothing, tower is gone
        }//end of if
        health -= attackAmount; //health goes down when attacked
        if(health <= 0f) {
            die(); 
        }//end of if
    }//end of attacked

    public void die(){
        //play death sound effect for tower
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound, 0.25f);

        isDestroyed = true; //tower is gone
        Destroy(gameObject); //get rid of tower on screen
        Debug.Log("Destroyed Defense Tower"); //check code works
    }//end of die
}//end of Defense Tower
