using UnityEngine;
using UnityEngine.UIElements;

namespace Nightmare
{
    public class EnemyPetHealth : MonoBehaviour
    {
        public int startingHealth = 50;
        public float sinkSpeed = 2.5f;
        public int scoreValue = 10;

        int currentHealth;
        Animator anim;
        ParticleSystem hitParticles;
        CapsuleCollider capsuleCollider;
        EnemyMovement enemyMovement;


        void Awake ()
        {
            anim = GetComponent <Animator> ();
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
                StartSinking();
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -1f)
                {
                    Destroy(gameObject);
                }
            }
        }

        public bool IsDead()
        {
            return currentHealth <= 0f;
        }

        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            Debug.Log("Enemy Pet: " + currentHealth);
            if (!IsDead())
            {
                // enemyAudio.Play();
                currentHealth -= amount;

                if (currentHealth <= 0)
                {
                    Death();
                }
            }
                
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
        }

        void Death ()
        {        
            // EventManager.TriggerEvent("Sound", transform.position);
            anim.SetBool ("IsWalking", false);

            // enemyAudio.clip = deathClip;
            // enemyAudio.Play ();
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

        public void ActivateCheatKillPet()
        {
            currentHealth = 0;
        }
    }
}