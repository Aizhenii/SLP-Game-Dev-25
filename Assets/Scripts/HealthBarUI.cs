using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform healthBar;

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
