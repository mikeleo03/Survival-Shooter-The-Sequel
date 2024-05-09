using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : Weapons
{
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    ParticleSystem gunParticles;
    Coroutine swordStop;
    Animator swordAnim;

    private void Awake()
    {
        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        triggerAudio = GetComponent<AudioSource>();
        swordAnim = GetComponentInChildren<Animator>();
    }

    public override void Shoot()
    {
        // Play the gun shot audioclip.
        triggerAudio.Play();

        if (swordAnim != null)
        {
            // Play sword animation
            swordAnim.SetBool("IsAttacking", true);
        }

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        if (!isEnemyWeapon)
        {
            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask, QueryTriggerInteraction.Ignore))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }

                // Try and find an EnemyPetHealth script on the gameobject hit.
                EnemyPetHealth enemyPetHealth = shootHit.collider.GetComponent<EnemyPetHealth>();

                // If the EnemyPetHealth component exist...
                if (enemyPetHealth != null)
                {
                    // ... the enemy pet should take damage.
                    Debug.Log("SWORDD");
                    enemyPetHealth.TakeDamage(damagePerShot, shootHit.point);
                }
            }
        }
    }

    public override void DisableEffects()
    {
        swordAnim.SetBool("IsAttacking", false);
        return;
    }
}
