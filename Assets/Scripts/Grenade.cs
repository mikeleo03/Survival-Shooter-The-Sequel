using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class Grenade : PausibleObject
    {
        public float explosiveForce = 500f;
        public int explosiveDamage = 50;
        public float explosiveRadius = 2f;
        public float timeOut = 3f;

        bool isPickup;
        Rigidbody rb;
        ParticleSystem ps;
        MeshRenderer mr;
        TrailRenderer tr;
        float timer = 0f;
        float destroyWait;
        UnityAction<Vector3> listener;

        void Awake()
        {
            rb = this.GetComponent<Rigidbody>();
            mr = this.GetComponent<MeshRenderer>();
            tr = this.GetComponentInChildren<TrailRenderer>();
            ps = this.GetComponentInChildren<ParticleSystem>();
            
            ParticleSystem.MainModule pMain = ps.main;
            destroyWait = Mathf.Max(pMain.startLifetime.constantMin, pMain.startLifetime.constantMax);

            listener = new UnityAction<Vector3>(Shoot);
            EventManager.StartListening("ShootGrenade", Shoot);

            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
            EventManager.StopListening("ShootGrenade", Shoot);
        }

        void OnEnable()
        {
            timer = 0f;
            mr.enabled = true;
            tr.enabled = false;
            ps.Stop();
            isPickup = true;
        }

        void Update()
        {
            if (isPaused)
                return;
            
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    Explode();
                }
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (isPickup)
            {
                if (coll.tag == "Player")
                {
                    EventManager.TriggerEvent("GrenadePickup");
                    Disable();
                }
            }
            else
            {
                if (coll.tag == "Enemy")
                {
                    Explode();
                }
            }
        }

        public void Shoot(Vector3 force)
        {
            if (timer > 0f)
                return;

            isPickup = false;
            mr.enabled = true;
            tr.enabled = true;
            timer = timeOut;
            rb.AddForce(force);
        }

        private void Explode()
        {
            timer = -1;
            ps.Play();
            tr.enabled = false;
            mr.enabled = false;
            EventManager.TriggerEvent("Sound", this.transform.position);

            Collider[] colls = Physics.OverlapSphere(this.transform.position, explosiveRadius);
            for (int i = 0; i < colls.Length; i++)
            {                
                if (colls[i].tag == "Enemy" && !colls[i].isTrigger)
                {
                    EnemyHealth victim = colls[i].GetComponent<EnemyHealth>();
                    if (victim != null)
                    {
                        victim.TakeDamage(explosiveDamage, colls[i].ClosestPoint(this.transform.position));
                    }
                }
            }

            StartCoroutine("TimedDisable");
        }

        private IEnumerator TimedDisable()
        {
            yield return new WaitForSeconds(destroyWait);
            Disable();
        }

        private void Disable()
        {
            timer = -1;
            isPickup = false;
            this.gameObject.SetActive(false);
        }
    }
}

