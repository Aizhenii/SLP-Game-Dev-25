using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // These points reference where the enemy goes.
    // Each point represents a turn for the enemy.
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;

    private Rigidbody2D rigidBody; // rigid body of enemy
    private Transform currentPoint;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // refers to rigid body of enemy
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position; // gives direction of enemy travel

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
