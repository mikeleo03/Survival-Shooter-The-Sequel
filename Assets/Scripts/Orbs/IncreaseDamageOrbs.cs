using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageOrbs : Orbs
{
    public PlayerShooting playerShooting;

    public static int damageOrbCount = 0;

    public override void Start()
    {
        playerShooting = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();

        // Destroy object after 5 seconds
        Destroy(gameObject, 5f);
    }

    public override void ApplyOrbEffect()
    {
        if (damageOrbCount < 15) // Maximum orb effects a player can get
        {
            playerShooting.damagePercent += 0.1f; // Add 10% from base damage
            damageOrbCount += 1;
        }

        // Destroy the orb
        Destroy(gameObject);
    }
}
