﻿using UnityEngine;

namespace Nightmare
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;
        public AudioClip deathClip;

        int currentHealth;
        Animator anim;
        AudioSource enemyAudio;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;

        // Cheat One Hit Kill
        public bool isCheatOneHitKill = false;
        public static bool IsActiveCheatOneHitKill = false; // For indicator to change isCheatOneHitKill

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
            enemyMovement = this.GetComponent<EnemyMovement>();
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        private void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }

        void Update ()
        {
            SetCheatOneHitKill(IsActiveCheatOneHitKill);

            if (IsDead())
            {
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return (currentHealth <= 0f);
        }

        // Activate or deactivate cheat one hit kill
        public void SetCheatOneHitKill(bool isActive)
        {
            // Get an array of all EnemyHealth scripts
            EnemyHealth[] allEnemies = FindObjectsOfType<EnemyHealth>();

            // Loop and set isCheatOneHitKill to true on each enemy
            foreach (EnemyHealth eHealth in allEnemies)
            {
                eHealth.isCheatOneHitKill = isActive;
            }

            // Change indicator
            IsActiveCheatOneHitKill = isActive;
        }

        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            if (!IsDead())
            {  
                // Cheat one hit kill
                if (isCheatOneHitKill)
                {
                    currentHealth = 0;
                }

                enemyAudio.Play();
                currentHealth -= amount;

                if (currentHealth <= 0)
                {
                    Death();
                }
                else
                {
                    enemyMovement.GoToPlayer();
                }
            }
                
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        void Death ()
        {
            EventManager.TriggerEvent("Sound", this.transform.position);
            anim.SetTrigger ("Dead");

            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }

        public void StartSinking ()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);

            ScoreManager.score += scoreValue;
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }
    }
}