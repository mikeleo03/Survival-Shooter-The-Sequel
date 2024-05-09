using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPlayerNearby;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNearby = true;
            Debug.Log("Player is nearby");
        }
    }

    void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNearby = false;
            Debug.Log("Player is not nearby");
        }
    }
}
