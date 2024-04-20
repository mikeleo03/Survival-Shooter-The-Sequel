using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Nightmare
{
    public class EnemyMovement : PausibleObject
    {
        public float visionRange = 10f;
        public float hearingRange = 20f;
        public float wanderDistance = 10f;
        public Vector2 idleTimeRange;
        [Range(0f,1f)]
        public float psychicLevels = 0.2f;

        float currentVision; 
        Transform player;
        PlayerHealth playerHealth;
        EnemyHealth enemyHealth;
        NavMeshAgent nav;
        public float timer = 0f;

        void Awake ()
        {
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent<NavMeshAgent>();

            StartPausible();
        }

        void OnEnable()
        {
            nav.enabled = true;
            ClearPath();
            ScaleVision(1f);
            IsPsychic();
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
                if (enemyHealth.CurrentHealth() > 0 && playerHealth.currentHealth > 0)
                {
                    LookForPlayer();
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

        private void LookForPlayer()
        {
            TestSense(player.position, currentVision);
        }

        private void HearPoint(Vector3 position)
        {
            TestSense(position, hearingRange);
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
            if (!enemyHealth.IsDead())
            {
                SetDestination(position);
            }
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

        private void IsPsychic()
        {
            GoToPlayer();
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

        //void OnDrawGizmos()
        //{
        //    Vector3 position = this.transform.position;
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(position, currentVision);
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(position, hearingRange);
        //}
    }
}