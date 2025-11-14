using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using System;

public class HealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    
    public GameObject healthbar;
    private HealthBarUI health;

    // Start is called before the first frame update
    void Start()
    {
        health = healthbar.GetComponent<HealthBarUI>();
        healthText.text = health.Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.Health.ToString() + " / " + health.MaxHealth.ToString();
    }
}
