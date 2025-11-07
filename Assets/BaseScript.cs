using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float Health, MaxHealth;

    public bool BaseDestroyed = false;
    
    [SerializeField]
    private HealthBarUI healthBar;

    private void Update()
    {
        if (Health == 0)
        {
            BaseDestroyed = true;
        }
    }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // This decreases health by 50 points when the enemy hits the base.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetHealth(-50f);
        }
        
    }
}
