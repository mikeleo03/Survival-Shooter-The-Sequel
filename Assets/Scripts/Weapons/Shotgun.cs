using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    RaycastHit[] shootHit;
    ParticleSystem gunParticles;
    Light gunLight;
    Light faceLight;

    private void Awake()
    {
        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        triggerAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        faceLight = GetComponentInChildren<Light>();
    }

    public override void Shoot()
    {
        // Play the gun shot audioclip.
        triggerAudio.Play();

        // Enable the lights.
        gunLight.enabled = true;
        faceLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        TextStatistics.shotsFired++;
        InGameTextStatistics.shotsFired++;


        if (!isEnemyWeapon)
        {
            shootHit = Physics.SphereCastAll(transform.position, range, transform.forward, range, shootableMask, QueryTriggerInteraction.Ignore);

            if (shootHit.Length > 0)
            {
                TextStatistics.shotsHit++;
                InGameTextStatistics.shotsHit++;
            }

            foreach (RaycastHit hit in shootHit)
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    // Damage is lower the farther the enemy is.
                    int finalDamage = Mathf.Max(10, Mathf.RoundToInt(damagePerShot - hit.distance / range * damagePerShot));
                    enemyHealth.TakeDamage(finalDamage, hit.point);
                }

                EnemyPetHealth enemyPetHealth = hit.collider.GetComponent<EnemyPetHealth>();
                // If the EnemyPetHealth component exist...
                if (enemyPetHealth != null)
                {
                    // ... the enemy pet should take damage.
                    // Damage is lower the farther the enemy pet is.
                    int finalDamage = Mathf.Max(10, Mathf.RoundToInt(damagePerShot - hit.distance / range * damagePerShot));
                    enemyPetHealth.TakeDamage(finalDamage, hit.point);
                }
            }
        }
    }

    public override void DisableEffects()
    {
        // Disable the light.
        faceLight.enabled = false;
        gunLight.enabled = false;
    }
}
