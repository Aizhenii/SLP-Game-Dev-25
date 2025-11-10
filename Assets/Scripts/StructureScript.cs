using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureScript : MonoBehaviour
{
    public float StructureHealth, StructureMaxHealth;


    public GameObject enemy;

    [SerializeField]
    private HealthBarUI healthBar;
    
    private EnemyScript enemyDamage;
    

    // This sets the healthbar to the maximum health when starting.
    void Start()
    {
        healthBar.SetMaxHealth(StructureMaxHealth);
        enemyDamage = enemy.GetComponent<EnemyScript>();
        enemyDamage.attackDmg = -20f;
    }

    // This is how the healthbar updates.
    public void SetHealth(float healthChange)
    {
        StructureHealth += healthChange;
        StructureHealth = Mathf.Clamp(StructureHealth, 0, StructureMaxHealth);

        healthBar.SetHealth(StructureHealth);
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        int delay = 0; // This variable is to prevent the health from reaching 0 immediately.

        // This block of code decreases health when the enemy hits the structure.
        if (collision.gameObject.CompareTag("Enemy") && StructureHealth > 0 && delay % 100 == 0)
        {
            
            SetHealth(enemyDamage.attackDmg);
        }
        delay++;

        // This block of code removes the structure collider box when the structure health is 0.
        if (StructureHealth == 0)
        {
            Destroy(GetComponent<BoxCollider2D>());
        }

    }
    
    

    
    
}
