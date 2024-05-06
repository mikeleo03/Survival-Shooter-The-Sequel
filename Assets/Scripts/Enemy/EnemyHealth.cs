﻿using UnityEngine;

namespace Nightmare
{
    public enum enemyTypes { Keroco, Kepala, Jenderal, Raja };
    public class EnemyHealth : MonoBehaviour
    {
        public enemyTypes type;
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
            if (type == enemyTypes.Keroco)
            {
                QuestManager.kerocoCount++;
            } else if (type == enemyTypes.Kepala)
            {
                QuestManager.kepalaCount++;
            } else if (type == enemyTypes.Jenderal)
            {
                QuestManager.jenderalCount++;
            } else
            {
                QuestManager.rajaCount++;
            }
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }
    }
}