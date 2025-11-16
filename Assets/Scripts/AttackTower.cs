using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour{
    [Header("Attack Tower Stats")]
    public float attackRange = 1000f; //range of attack
    public float attackDamage = 50F; //amount of damage tower does
    public float attackInterval = 1f; //wait this amount of seconds before attacking again
    public float searchInterval = .25f; //time between searches for an enemy
    private EnemyScript enemy; //get enemy
    private float attackTimer;
    private float searchTimer;

    //Projectile settings
    public GameObject towerProjectilePrefab;
    public Transform fire;
    public float projectileSpeed = 300f;
    [SerializeField] private AudioClip attackSound; //sound for firing/attacking
    private AudioSource audioSource; //to play sound effects

    //Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>(); //initialize   
    }//end of Start

    void Update(){
        searchTimer += Time.deltaTime;
        if(searchTimer >= searchInterval){
            //find the enemy if timer is over the alloted interval
            findEnemy(); //find enemy in range to attack
            searchTimer = 0f; //reset timer
        }//end of if

        if(enemy != null){
            //get location
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance <= attackRange){ //check in range
                attackEnemy();
            }//end of if
            else {
                enemy = null; //no enemy in range to attack
                attackTimer = 0f; //reset timer
            }//end of else
        }//end of if
    }//end of Update

    
    private void attackEnemy(){
        attackTimer += Time.deltaTime; //timing between attacks

        //check if a tower can attack based on time passed
        if (attackTimer >= attackInterval){
            attackTimer = 0f; //resets timer after attack
            if (enemy != null){
                enemy.TakeDamage(attackDamage);
                fireProjectile();
                if (attackSound != null) //play sound effect when enemy located and attacked
                    audioSource.PlayOneShot(attackSound, 0.25f);
                Debug.Log($"fAttacked enemy {enemy.health}"); //check code working
            }//end of if
        }//end of if
    }//end of attackPlayer

    //new method to fire projectile
    private void fireProjectile()
    {
        if(towerProjectilePrefab == null)
        {
            Debug.LogWarning("no projectile prefab");
            return;
        }

        Vector3 spawnPosition = fire != null ? fire.position : transform.position;

        GameObject projectileObject = Instantiate(towerProjectilePrefab, spawnPosition, Quaternion.identity);
        TowerProjectileScript proj = projectileObject.GetComponent<TowerProjectileScript>();
        if(proj != null)
        {
            proj.initialize(enemy.transform, attackDamage, projectileSpeed);
        }
        else
        {
            Debug.LogWarning("projectile has no script!");
        }

            
    }

    private void findEnemy(){
        GameObject[] incomingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null; //set closest enemy as target later
        float minDistance = Mathf.Infinity;

        foreach(GameObject enemyObject in incomingEnemies){
            //get distance from enemy
            float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
            if(distance < minDistance){ //find closest enemy
                minDistance = distance; //set as new minDistance for later comparison
                closest = enemyObject; //current closest enemy
            }//end of if
        }//end of foreach

        //check enemy exists and is in attackRange before attacking
        if(closest!=null && minDistance<=attackRange){
            enemy = closest.GetComponent<EnemyScript>(); //set as target
        }//end of if
        else{
            enemy = null; //no enemies close enough
        }//end of else
    }//end of findEnemy
}//end of AttackTower
