using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision happened with " + other.gameObject.name);

        Debug.Log("Tag: " + other.gameObject.tag);
        Debug.Log("RigidBody: " + other.rigidbody.ToString());
        Debug.Log("Is Trigger: " + other.collider.isTrigger.ToString());

        Debug.Log("Velocity: " + other.relativeVelocity.ToString());

        ContactPoint[] cp = other.contacts;
        foreach (ContactPoint point in cp)
        {
            Debug.Log(point.point.ToString());
        }
    }
}
