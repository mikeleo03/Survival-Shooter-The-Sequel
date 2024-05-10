using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;
using Unity.VisualScripting;

namespace Nightmare
{
    public class EnemyPetMovement : PausibleObject
    {
   
        public float wanderDistance = 10f;
        public Vector2 idleTimeRange;
        [Range(0f,1f)]
    
        float currentVision; 
        Transform player;
        PlayerHealth playerHealth;
        Animator anim; 

        bool isPlayerInRange;

        NavMeshAgent nav;
        public float timer = 0f;
        void Awake ()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            nav = GetComponent<NavMeshAgent>();
            StartPausible();
        }

        void OnEnable()
        {
            nav.enabled = true;
            ClearPath();
            AvoidPlayer();
            timer = 0f;
        }

        void OnTriggerEnter (Collider other)
        {
            if(other.CompareTag("Player"))
            {
                isPlayerInRange = true;
            }
        }

        void OnTriggerExit (Collider other)
        {
            if(other.CompareTag("Player"))
            {
                isPlayerInRange = false;
            }
        }

        void ClearPath()
        {
            if (nav.hasPath)
                nav.ResetPath();
        }

        void Update ()
        {
            if (!isPaused)
            {
                if (playerHealth.currentHealth > 0)
                {
                    if (isPlayerInRange) AvoidPlayer();
                    WanderOrIdle();
                    Animate();
                }
                else
                {
                    nav.enabled = false;
                }
            }
        }

        void AvoidPlayer()
        {
            GoToPosition(new Vector3(-player.position.x, -player.position.y, -player.position.z));
        }

        void Animate()
        {
            if (nav.velocity.magnitude < 0.1f)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }
        }

        void OnDestroy()
        {
            nav.enabled = false;
            StopPausible();
        }

        public override void OnPause()
        {
            if (nav.hasPath)
                nav.isStopped = true;
        }

        public override void OnUnPause()
        {
            if (nav.hasPath)
                nav.isStopped = false;
        }


        private void GoToPosition(Vector3 position)
        {
            timer = -1f;
            SetDestination(position);
        }

        private void SetDestination(Vector3 position)
        {
            if (nav.isOnNavMesh)
            {
                nav.SetDestination(position);
            }
        }

        private void WanderOrIdle()
        {
            if (!nav.hasPath)
            {
                if (timer <= 0f)
                {
                    SetDestination(GetRandomPoint(wanderDistance, 5));
                    if (nav.pathStatus == NavMeshPathStatus.PathInvalid)
                    {
                        ClearPath();
                    }
                    timer = Random.Range(idleTimeRange.x, idleTimeRange.y);
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }

        private Vector3 GetRandomPoint(float distance, int layermask)
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * distance + this.transform.position;;

            NavMeshHit navHit;
            NavMesh.SamplePosition(randomPoint, out navHit, distance, layermask);

            return navHit.position;
        }

        private int GetCurrentNavArea()
        {
            NavMeshHit navHit;
            nav.SamplePathPosition(-1, 0.0f, out navHit);

            return navHit.mask;
        }

    }
}