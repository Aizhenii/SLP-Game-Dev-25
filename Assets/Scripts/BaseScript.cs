using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float Health, MaxHealth;
    public GameObject enemy;

    [SerializeField]
    private HealthBarUI healthBar;
    
    private EnemyScript enemyDamage;

    [SerializeField] private AudioClip damageSound; //sound for damaged base
    private AudioSource audioSource; //to play sound effects

    // This sets the healthbar to the maximum health when starting.
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
        enemyDamage = enemy.GetComponent<EnemyScript>();
        enemyDamage.attackDmg = -20f;
        audioSource = GetComponent<AudioSource>(); //initialize
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
        // This decreases health by damage points when the enemy hits the base.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("base attacked");
            SetHealth(enemyDamage.attackDmg);
            //play sound effect for damage at 50% volume
            if (damageSound != null)
                audioSource.PlayOneShot(damageSound, 0.5f);
        }
        
    }
}
