using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

namespace Nightmare
{
    public class EnemyAttack : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        Weapons heldWeapon;
        bool playerInRange;
        float timer;
        int buff = 0;
        float buffRate = 0.2f;

        void Awake ()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
            heldWeapon = GetComponentInChildren<Weapons>();

            int difficultyLvl = PlayerPrefs.GetInt("Difficulty", 0);
            if (difficultyLvl == 1)
            {
                attackDamage = Mathf.RoundToInt(attackDamage * 1.5f);
            }
            else if (difficultyLvl == 2)
            {
                attackDamage = Mathf.RoundToInt(attackDamage * 2f);
            }

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter (Collider other)
        {
            // If the entering collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is in range.
                playerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            // If the exiting collider is the player...
            if(other.gameObject == player)
            {
                // ... the player is no longer in range.
                playerInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused)
                return;
            
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            if (timer >= 0.2f)
            {
                heldWeapon.DisableEffects();
            }

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth() > 0)
            {
                // ... attack.
                Attack ();
            }

            // If the player has zero or less health...
            if(playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the player is dead.
                anim.SetTrigger ("PlayerDead");
            }
        }

        void Attack ()
        {
            // Reset the timer.
            timer = 0f;
        
            int totalDamage = (int) Math.Round(attackDamage  + attackDamage * buff * buffRate);
            // If the player has health to lose...
            if(playerHealth.currentHealth > 0)
            {
                // ... damage the player.
                heldWeapon.Shoot();
                playerHealth.TakeDamage (totalDamage);
            }
        }

        public void Buff()
        {
            buff++;
        }

        public void Debuff()
        {
            buff++;
        }
    }

    
}