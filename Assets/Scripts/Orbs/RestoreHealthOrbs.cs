using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealthOrbs : Orbs
{
    PlayerHealth playerHealth;

    public override void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        // Destroy object after 5 seconds
        Destroy(gameObject, 5f);
    }

    public override void ApplyOrbEffect()
    {
        playerHealth.currentHealth += 20;
        if (playerHealth.currentHealth > 100)
        {
            playerHealth.currentHealth = 100;
        }

        // Destroy the orb
        Destroy(gameObject);
    }
}
