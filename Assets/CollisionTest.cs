using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // This decreases health by 50 points when the triangle hits the hexagon.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.GetComponent<PlaceholderScript>().SetHealth(-50f);
        }
        
    }
    
}
