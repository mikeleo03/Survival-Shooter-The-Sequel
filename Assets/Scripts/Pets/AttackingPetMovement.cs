using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class AttackingPetMovement : PausibleObject
    {
        public float visionRange = 10f;
        public float wanderDistance = 10f;
        public Vector2 idleTimeRange;
        [Range(0f,1f)]

        float currentVision; 
        Transform player;
        PlayerHealth playerHealth;
        
        // EnemyHealth enemyHealth;
        NavMeshAgent nav;
        public float timer = 0f;

        float attackTimer = 0f;

        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            nav = GetComponent<NavMeshAgent>();
            StartPausible();
        }

        void OnEnable()
        {
            nav.enabled = true;
            ClearPath();
            ScaleVision(1f);
            GoToPlayer();
            timer = 0f;
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
                // If both the enemy and the player have health left...
                // TODO: Verify pet health
                if (playerHealth.currentHealth > 0)
                {
                    LookForEnemy();
                    WanderOrIdle();
                }
                else
                {
                    nav.enabled = false;
                }
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

        private GameObject getNearestEnemy() 
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float nearestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }

        private void LookForEnemy()
        {
            GameObject nearestEnemy = getNearestEnemy();
            if (nearestEnemy != null)
            {
                TestSense(nearestEnemy.transform.position, currentVision);
            }
        }

        private void TestSense(Vector3 position, float senseRange)
        {
            if (Vector3.Distance(this.transform.position, position) <= senseRange)
            {
                GoToPosition(position);
            }
        }

        public void GoToPlayer()
        {
            GoToPosition(player.position);
        }

        private void GoToPosition(Vector3 position)
        {
            timer = -1f;
            // if (!enemyHealth.IsDead()) TODO: verify pet health
            // {
                SetDestination(position);
            // }
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

        public void ScaleVision(float scale)
        {
            currentVision = visionRange * scale;
        }

        private int GetCurrentNavArea()
        {
            NavMeshHit navHit;
            nav.SamplePathPosition(-1, 0.0f, out navHit);

            return navHit.mask;
        }

    }
}