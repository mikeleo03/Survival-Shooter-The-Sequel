using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Nightmare
{
    public class AllyPetHealth : MonoBehaviour
    {
        public int startingHealth = 50;
        public float sinkSpeed = 2.5f;

        Animator anim;
        CapsuleCollider capsuleCollider;
        int currentHealth;
        

        void Awake ()
        {
            anim = GetComponent <Animator> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
        }

        void OnEnable()
        {
            currentHealth = startingHealth;
            SetKinematics(false);
        }

        void SetKinematics(bool isKinematic)
        {
            capsuleCollider.isTrigger = isKinematic;
            capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
        }
        
        bool IsDead()
        {
            return currentHealth <= 0f;
        }

        void Update ()
        {
            if (IsDead())
            {
                //TODO: Add a death animation, set rigidbody to kinematic, and sink the pet into the ground
                Destroy(gameObject);

                // transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                // if (transform.position.y < -10f)
                // {
                //     Destroy(gameObject);
                // }
            }
        }

        public void TakeDamage (int amount)
        {
            if (!IsDead())
            {
                Debug.Log("AllyPetHealth: " + currentHealth);
                currentHealth -= amount;
            }
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }   
        
    }
}