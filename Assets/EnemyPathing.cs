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
    //GameObject[] turningPoints = {pointA, pointB, pointC};

    private Rigidbody2D rigidBody; // rigid body of enemy
    private Transform currentPoint; // target point of enemy

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

        // Pathing(turningPoints);

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

    // In theory, this method should allow pathing.
    // In practice, it did not work at all.
    /*
    public static void Pathing(GameObject pointList)
    {
        int i = 0;

        if (currentPoint == pointList[i].transform)
        {
            rigidBody.velocity = new Vector2(speed, speed);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointList[i].transform)
        {
            i++;
        }
    }
    */
}
