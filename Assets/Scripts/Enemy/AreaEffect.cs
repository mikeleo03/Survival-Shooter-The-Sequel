using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : PausibleObject
{
    GameObject player;
    bool playerInRange;
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;
    PlayerShooting playerShooting;
    PlayerMovement playerMovement;
    float timer;

    public int damageOverTime;
    public float playerDmgDebuff; // Damage debuff value in percent
    public float playerSpeedDebuff; // Speed debuff value (not percent)
    public float dotInterval;
    public bool isRaja;
    private void Awake()
    {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
        StartPausible();
    }

    void OnDestroy()
    {
        StopPausible();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... player is in range
            playerInRange = true;
            // Apply debuff is enemy is Raja
            if (isRaja)
            {
                playerMovement.speed -= playerSpeedDebuff;
                playerShooting.damagePercent -= playerDmgDebuff;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... player is out of range
            playerInRange = false;
            // Deapply debuff to player
            if (isRaja)
            {
                playerMovement.speed += playerSpeedDebuff;
                playerShooting.damagePercent += playerDmgDebuff;
            }
        }
    }

    private void Update()
    {
        if (isPaused)
            return;

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the damage over time interval, the player is in range and this enemy is alive...
        if (timer >= dotInterval && playerInRange && enemyHealth.CurrentHealth() > 0)
        {
            // ... apply DoT
            DamageOverTime();
        }
    }

    void DamageOverTime()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(damageOverTime);
        }
    }
}
