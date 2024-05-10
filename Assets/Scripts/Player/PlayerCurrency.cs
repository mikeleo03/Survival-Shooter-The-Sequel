using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour, IDataPersistance
{
    public int balance = 0;
    int prevBalance;
    
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

    public void ActivateMotherlode()
    {
        prevBalance = balance;
        balance = 100000;
    }

    public void ResetMotherlode()
    {
        balance = prevBalance;
    }

    public void LoadData(GameData data)
    {
        this.balance = data.balance;
    }

    public void SaveData(ref GameData data)
    {
        data.balance = this.balance;
    }
}
