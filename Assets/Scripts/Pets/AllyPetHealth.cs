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
        public int currentHealth;

        // Cheat Full HP Pet
        public bool isCheatFullHPPet = false;


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
                StartSinking();
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
                if (transform.position.y < -10f)
                {
                    Destroy(gameObject);
                }
            }
        }

        void StartSinking ()
        {
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
            SetKinematics(true);
        }

        public void TakeDamage (int amount)
        {
            if (!IsDead())
            {
                // Cheat Full HP Pet (Invicible)
                if (isCheatFullHPPet)
                    return;

                Debug.Log("AllyPetHealth: " + currentHealth);
                currentHealth -= amount;
            }
        }

        public int CurrentHealth()
        {
            return currentHealth;
        }        
        
        // Activate or deactivate cheat full hp pet
        public void SetCheatFullHPPet(bool isActive)
        {
            isCheatFullHPPet = isActive;
        }
        
    }

    
}
