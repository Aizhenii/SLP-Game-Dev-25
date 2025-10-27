using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStructureHealthOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // This decreases health by 50 points when the enemy hits the base.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.GetComponent<StructureScript>().SetHealth(-50f);
        }
        
    }
}
