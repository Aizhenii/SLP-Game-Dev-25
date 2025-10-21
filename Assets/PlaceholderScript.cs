using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderScript : MonoBehaviour
{
    public float Health, MaxHealth;
    
    [SerializeField]
    private HealthBarUI healthBar;
    

    // This sets the healthbar to the maximum health when starting.
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    // This updates healthbar in response to keyboard input.
    void Update()
    {
        /*
        if (Input.GetKeyDown("q"))
        {
            SetHealth(-20f);
        }

        if (Input.GetKeyDown("w"))
        {
            SetHealth(20f);
        }
        */


    }
    
    


    // This is how the healthbar updates.
    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
}
