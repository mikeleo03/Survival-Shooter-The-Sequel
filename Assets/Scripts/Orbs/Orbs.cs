using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orbs : MonoBehaviour
{
    public abstract void Start();

    public abstract void ApplyOrbEffect();

    void OnTriggerEnter(Collider other)
    {
        // If player takes the orb
        if (other.gameObject.CompareTag("Player"))
        {
            // Apply orb logic
            ApplyOrbEffect();
        }
    }
}

