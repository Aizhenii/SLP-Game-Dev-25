using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureScript : MonoBehaviour
{
    public float StructureHealth, StructureMaxHealth;
    
    [SerializeField]
    private HealthBarUI healthBar;
    

    // This sets the healthbar to the maximum health when starting.
    void Start()
    {
        healthBar.SetMaxHealth(StructureMaxHealth);
    }

    // This is how the healthbar updates.
    public void SetHealth(float healthChange)
    {
        StructureHealth += healthChange;
        StructureHealth = Mathf.Clamp(StructureHealth, 0, StructureMaxHealth);

        healthBar.SetHealth(StructureHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // This block of code decreases health when the enemy hits the structure.
        while (collision.gameObject.CompareTag("Enemy") && StructureHealth > 0)
        {
            SetHealth(-5f);
        }

        // This block of code removes the structure collider box when the structure health is 0.
        if (StructureHealth == 0)
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
        
    }
}
