using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
//using System.Threading.Tasks.Dataflow;

//using System.Threading.Tasks.Dataflow;
using UnityEngine;

/// <summary> 
/// Enemy class for the construction workers
/// establishes health, speed, and attack dmg
/// also handles taking damage
/// </summary>

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;

    public float speed = 200f;

    public float attackDmg = 20f;

    private DefenseTower attackedTower;

    private Rigidbody2D rigidBody; // rigid body of enemy

    private bool isDying = false; //track if the enemy is dead

    [SerializeField]
    private Transform[] waypoints;

    private int waypointIndex = 0;

    [SerializeField] private AudioClip damageSound; //sound for damaged enemy
    [SerializeField] private AudioClip deathSound; //sound for dead enemy
    private AudioSource audioSource; //to play sound effects

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // refers to rigid body of enemy
        audioSource = GetComponent<AudioSource>(); //initialize
        //tower = GameObject.FindGameObjectWithTag("Tower")?.transform;

    }

    void Update()
    {
        EnemyPathing();
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // This deletes the enemy once it hits the base.
        if (collision.gameObject.CompareTag("Base"))
        {
            Die();
        }
        

    }


    public float Damage
    {
        get { return attackDmg; } // used to damage structures
    }

    public void TakeDamage(float amt)
    {
        //play enemy damage sound at 50% volume
        if(damageSound != null)
            audioSource.PlayOneShot(damageSound, 0.5f);

        health = health - amt;
        Debug.Log("Enemy Hit"); //can delete if you want - Elsa
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDying) return;  //prevents two deaths with audio calls, adjustments to script made for audio
        isDying = true;

        //play enemy death sound at 50% volume
        if (deathSound!=null && audioSource!=null)
            audioSource.PlayOneShot(deathSound, 0.5f);

        Debug.Log("Enemy Died"); //can delete if you want - Elsa
        Destroy(gameObject, 0.3f); //take game object off screen
    }

    //attack defense towers
    public void AttackTower(DefenseTower t)
    {
        if (t != null)
        {
            t.attacked(attackDmg);
        }
    }

    public void EnemyPathing()
    {
        if (waypointIndex < waypoints.Length) // stops enemy at last waypoint
        {
            // how the enemy moves from one point to another
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

            // enemy changes direction
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

}