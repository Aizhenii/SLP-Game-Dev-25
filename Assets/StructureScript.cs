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
}
