using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        ContactPoint[] cp = other.contacts;
    }
}
