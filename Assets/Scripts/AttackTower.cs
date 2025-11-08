using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour{
    public float attackRange = 5f; //range of attack
    public float attackDamage = 15f; //amount of damage tower does
    public GameObject enemyObject;
    public float attackInterval = 1f; //wait this amount of seconds before attacking again
    private Transform enemy; //track enemy position
    //access enemy class to get their health stats, enemyStats
    private float timer;
    private float attackTimer;

    private void Awake(){
        //find the enemy
        if(enemyObject != null){
            enemy = enemyObject.transform;
        }//end of if
        else{
            var go = GameObject.FindGameObjectWithTag("Enemy");
            if(go != null){
                enemy = go.transform;
            }//end of if
        }//end of else

        //cache enemy stats
        if(enemy != null){
          //enemyStats = enemy.GetComponent<EnemyStats>();
        }//end of if
    }//end of Awake

    // Update is called once per frame
    void Update(){
        if(enemy!=null && enemyInRange()){
            //attack enemy
            //enemy is 
        }//end of if
    }//end of Update

    private void attackEnemy(){
        /*
        if(enemyStats == null){
            return;
        }//end of if
        */
        //get distance from tower to enemy
        float distance = Vector3.Distance(transform.position, enemy.position);
        if(distance <= attackRange){ //attack if in range
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackInterval){
                attackTimer = 0f;
                //enemyStats.TakeDamage(attackDamage);
            }//end of if
        }//end of if
        else{
            attackTimer = 0f; //reset time if out of range
        }//end of else
    }//end of attackPlayer

    private bool enemyInRange(){
        Vector3 distance = transform.position; //get position of the enemy
        if(distance.magnitude <= attackRange){
            return true; //enemy in range -> can attack
        }//end of if
        return false; //enemy not in range -> cannot attack
    }//end of canSeePlayer
}//end of AttackTower
