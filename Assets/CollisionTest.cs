using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        // This decreases health by 50 points when the triangle hits the hexagon.
        // It throws a NullReferenceException but still works.
        this.GetComponent<PlaceholderScript>().SetHealth(-50f);
        
    }
    
}
