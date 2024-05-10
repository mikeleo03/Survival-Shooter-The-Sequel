using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoreHealthOrbs : Orbs
{
    PlayerHealth playerHealth;
    Slider healthSlider;

    public override void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        // Destroy object after 5 seconds
        Destroy(gameObject, 5f);
    }

    public override void ApplyOrbEffect()
    {
        playerHealth.TakeDamage(-20); // Restore 20 HP

        // Handle HP overflow
        if (playerHealth.currentHealth > 100)
        {
            playerHealth.currentHealth = 100;
        }

        // Set Health Slider
        healthSlider.value = playerHealth.currentHealth;

        // Destroy the orb
        Destroy(gameObject);
    }
}
