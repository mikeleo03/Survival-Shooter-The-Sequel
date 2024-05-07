using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedOrbs : Orbs
{
    public PlayerMovement playerMovement;
    public float originalSpeed;
    public float speedMultiplier = 0.2f;

    private static float endTime = float.PositiveInfinity;
    private bool isDestroying = false;
    private bool isEffectActive = false;

    public override void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        originalSpeed = playerMovement.originalSpeed;

        // Destroy object after 5 seconds
        StartCoroutine(DestroyAfterSec(5f));
    }

    private IEnumerator DestroyAfterSec(float sec)
    {
        yield return new WaitForSeconds(sec);

        // If the orb hasn't been taken after the specified number of seconds, destroy it
        if (!isEffectActive)
        {
            Destroy(gameObject);
        }
    }

    public override void ApplyOrbEffect()
    {
        isEffectActive = true;
        playerMovement.speed = originalSpeed + originalSpeed * speedMultiplier;
        playerMovement.prevSpeed = playerMovement.speed;

        // Disable the child objects of the orb
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Disable the SphereCollider to prevent further interaction
        var collider = GetComponent<SphereCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Stop the ParticleSystem
        var particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // Update the end time only if the effect is newly applied
        if (!isDestroying)
        {
            endTime = Time.time + 15f;
        }
    }

    private void Update()
    {
        // If the current time is past the end time and the game object is not being destroyed, reset the speed
        if (Time.time >= endTime && !isDestroying)
        {
            playerMovement.speed = originalSpeed;
            playerMovement.prevSpeed = originalSpeed;
            isEffectActive = false;  // Set isEffectActive to false when the speed is reset
            isDestroying = true;
            Destroy(gameObject);
        }
    }

}
