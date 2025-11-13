using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour{
    [Header("Defense Tower Stats")]
    public float health = 150f; //enemy attacks at 10 per hit
    private bool isDestroyed = false; //check if tower is out of health
    public AudioSource soundEffect;

    //Start is called before the first frame update
    void Start()
    {
        soundEffect = GetComponent<AudioSource>(); //for tower death
    }//end of Start

    // Update is called once per frame
    void Update(){
        if (isDestroyed){
            return; //do nothing, tower is gone
        }//end of if
    }//end of Update

    public void attacked(float attackAmount){
        if (isDestroyed){
            return; //do nothing, tower is gone
        }//end of if
        health -= attackAmount; //health goes down when attacked
        if(health <= 0f) {
            die(); 
        }//end of if
    }//end of attacked

    public void die(){
        isDestroyed = true; //tower is gone
        Destroy(gameObject); //get rid of tower on screen
        soundEffect.Play(); //play the tower being destroyed sound effect
        Debug.Log("Destroyed"); //check code works
    }//end of die
}//end of Defense Tower
