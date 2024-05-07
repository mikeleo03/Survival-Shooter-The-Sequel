using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Nightmare
{
    public class AttackingPetAction : PausibleObject
    {
        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        // Animator anim;
        GameObject player;
        PlayerHealth playerHealth;
        List<GameObject> nearbyEnemies;
        float timer;

        void Awake ()
        {
            // anim = GetComponent <Animator> ();
            nearbyEnemies = new List<GameObject>();
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void OnTriggerEnter (Collider other)
        {
            if(other.CompareTag("Enemy"))
            {
                nearbyEnemies.Add(other.gameObject);
            }
            Debug.Log("SKEEBS");
        }

        void OnTriggerExit (Collider other)
        {
            if(nearbyEnemies.Contains(other.gameObject))
            {
                nearbyEnemies.Remove(other.gameObject);
            }
        }

        void Update ()
        {
            if (isPaused || nearbyEnemies.Count == 0)
                return;
            
            GameObject targetedEnemy = nearbyEnemies[0]; 
            while (targetedEnemy.GetComponent<EnemyHealth>().CurrentHealth() < 0) {
                nearbyEnemies.Remove(targetedEnemy);
                if (nearbyEnemies.Count == 0) {
                    return;
                }
                targetedEnemy = nearbyEnemies[0];
            }

            timer += Time.deltaTime;
            if(timer >= timeBetweenAttacks)
            {
                Attack (targetedEnemy);
            }

            // // If the player has zero or less health...
            // if(playerHealth.currentHealth <= 0)
            // {
            //     // ... tell the animator the player is dead.
            //     anim.SetTrigger ("PlayerDead");
            // }
        }

        void Attack (GameObject targetedEnemy)
        {
            timer = 0f;
            targetedEnemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage, new Vector3());
            Debug.Log("Attacked");
        }
    }
}