using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orbs : MonoBehaviour
{
    public abstract void ApplyOrbEffect(GameObject Player);

    void OnTriggerEnter(Collider other)
    {
        // If player takes the orb
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy the orb
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Destroy object after 5 seconds
        Destroy(gameObject, 5f);
    }
}

