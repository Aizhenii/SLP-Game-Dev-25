using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour{
    public float health = 100f; //enemy attacks at 10 per hit
    private bool isDestroyed = false; //check if tower is out of health
    public GameObject attackedTower; //tower currently being attacked

    // Start is called before the first frame update
    void Start() { 
    }//end of Start

    // Update is called once per frame
    void Update(){
        if (isDestroyed){
            return; 
        }//end of if
    }//end of Update

    public void attacked(float attackAmount){
        if (isDestroyed){
            return;
        }//end of if
        health -= attackAmount; //health goes down when attacked
        if(health <= 0) {
            die(); 
        }//end of if
    }//end of attacked

    public void die(){
        isDestroyed = true; //tower is gone
        Destroy(attackedTower);
    }//end of die
}//end of Defense Tower
