using System.Numerics;
using System.Runtime.Serialization;
using System.Security;
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

    //private transform player;

    private Rigidbody2D rigidBody; // rigid body of enemy

    [SerializeField]
    private Transform[] waypoints;

    private int waypointIndex = 0;

    void Start()
    {
        //tower = GameObject.FindGameObjectWithTag("Tower")?.transform;
        rigidBody = GetComponent<Rigidbody2D>(); // refers to rigid body of enemy
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
        health = health - amt;
        Debug.Log("Hit"); //can delete if you want - Elsa
        if (health <= 0f)
        {
            Die();
        }
    }



    private void Die()
    {
        Destroy(gameObject, 0.3f);
        Debug.Log("Died"); //can delete if you want - Elsa
    }

    /*
    public void AttackTower(Tower playerScript)
    {
        if (playerScript != null)
        {
            playerScript.TakeDamage(attackDmg);
        }
    }
    */

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