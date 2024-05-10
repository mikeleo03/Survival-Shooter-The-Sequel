using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    ShopManager shopManager;
    void Start()
    {
        shopManager = FindFirstObjectByType<ShopManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopManager.setAccessible(true);
        }
    }   

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shopManager.setAccessible(false);
        }
    }
}
