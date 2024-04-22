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

    private void Awake()
    {
        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        triggerAudio = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        // Play the gun shot audioclip.
        triggerAudio.Play();

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
        }
    }

    public override void DisableEffects()
    {
        return;
    }
}
