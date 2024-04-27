using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : PausibleObject
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] int spawnTimer;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        InvokeRepeating("Spawn", spawnTimer, spawnTimer);
        StartPausible();
    }

    private void OnDestroy()
    {
        StopPausible();
    }

    void Spawn()
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f || enemyHealth.CurrentHealth() <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemyToSpawn, transform.position + transform.right * 2, transform.rotation);
    }
}
