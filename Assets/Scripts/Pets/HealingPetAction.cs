using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Nightmare
{
    public class HealingPetAction : PausibleObject
    {
        public float timeBetweenHeals = 0.5f;
        public int healHitPoint = 10;

        // Animator anim;
        GameObject player;
        PlayerHealth playerHealth;

        bool isPlayerInRange;

        float timer;

        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter (Collider other)
        {
            if(other.gameObject == player)
            {
                isPlayerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            if(other.gameObject == player)
            {
                isPlayerInRange = false;
            }
        }

        void Update ()
        {
            if (isPaused || !isPlayerInRange)
                return;
            
            timer += Time.deltaTime;
            if(timer >= timeBetweenHeals && playerHealth.currentHealth < playerHealth.startingHealth)
            {
                Heal();
            }

            // // If the player has zero or less health...
            // if(playerHealth.currentHealth <= 0)
            // {
            //     // ... tell the animator the player is dead.
            //     anim.SetTrigger ("PlayerDead");
            // }
        }

        void Heal ()
        {
            timer = 0f;
            healHitPoint = Mathf.Min(healHitPoint, playerHealth.startingHealth - playerHealth.currentHealth);
            playerHealth.TakeDamage(-healHitPoint);
            Debug.Log("Healed");
        }
    }
}