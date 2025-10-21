using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnCollisionEnter2D()
    {
        // This decreases health by 50 points when the triangle hits the hexagon.
        this.GetComponent<PlaceholderScript>().SetHealth(-50f);
        
    }
    
}
