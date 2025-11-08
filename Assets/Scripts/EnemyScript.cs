using System.Security;
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

    public float attackDmg = 10f;

    //private transform player;

    private Rigidbody2D rigidBody; // rigid body of enemy
    private Transform currentPoint; // target point of enemy

    [SerializeField]
    private GameObject pointA;
    [SerializeField]
    private GameObject pointB;
    [SerializeField]
    private GameObject pointC;

    void Start()
    {
        //tower = GameObject.FindGameObjectWithTag("Tower")?.transform;
        rigidBody = GetComponent<Rigidbody2D>(); // refers to rigid body of enemy
        currentPoint = pointB.transform;
    }

    void Update()
    {
        EnemyPathing();
    }

    public void TakeDamage(float amt)
    {
        health = health - amt;
        Debug.Log("Hit"); //can delete if you want - Elsa
        if(health <= 0f)
        {
            Die();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
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
        Vector2 point = currentPoint.position - transform.position; // gives direction of enemy travel

        //Pathing(turningPoints);
        
        // Enemy travels in a straight line
        if (currentPoint == pointB.transform)
        {
            rigidBody.velocity = new Vector2(speed, 0);
        }

        // Enemy changes direction at a point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointC.transform;
        }

        if (currentPoint == pointC.transform)
        {
            rigidBody.velocity = new Vector2(0, -speed);
        }
    }


}