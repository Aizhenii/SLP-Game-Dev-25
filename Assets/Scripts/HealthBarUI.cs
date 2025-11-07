using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    public bool BaseDestroyed = false;

    [SerializeField]
    private RectTransform healthBar;

    private void Update()
    {
        if (Health == 0)
        {
            BaseDestroyed = true;
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    // This changes the health bar width depending on current health.
    public void SetHealth(float health)
    {
        Health = health;
        float newWidth = (Health / MaxHealth) * Width;
        healthBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
