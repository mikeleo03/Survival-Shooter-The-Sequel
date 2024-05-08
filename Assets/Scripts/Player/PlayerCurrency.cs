using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public int balance = 0;
    
    // Start is called before the first frame update
    public void add(int nominal) 
    {
        balance += nominal;
    }

    public void subtract(int nominal) 
    {
        if (balance < nominal) 
        {
            throw new System.Exception("Insufficient balance");
        }
        balance -= nominal;

        Debug.Log("Balance: " + balance);
    }

    public int Balance() 
    {
        return balance;
    }
}
