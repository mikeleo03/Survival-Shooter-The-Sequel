using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy object after 5 seconds
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        // If player takes the orb
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy the orb
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
