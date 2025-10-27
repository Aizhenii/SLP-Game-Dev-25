using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float Health, MaxHealth;
    
    [SerializeField]
    private HealthBarUI healthBar;
    

    // This sets the healthbar to the maximum health when starting.
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    // This is how the healthbar updates.
    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}
