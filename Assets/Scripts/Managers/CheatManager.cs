using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/* CHEATS DOCUMENTATION */
/*
 * NODAMAGE     : Player becomes invicible (HP do not decrease)
 * ONEHITKILL   : Player deals one hit kill attack on enemies
 * MOTHERLODE   : Player gets infinite money
 * XTWOSPEED    : Player's movement becomes twice faster
 * FULLHPPET    : Player's pet becomes invicible (HP do not increase)
 * KILLPET      : Player's pet gets killed instantly
 * GETORB       : Player gets random orb
 * SKIPLEVEL    : Player skips 1 level
 */

public enum CheatsType
{
    NODAMAGE,
    ONEHITKILL,
    MOTHERLODE,
    XTWOSPEED,
    FULLHPPET,
    KILLPET,
    GETORB,
    SKIPLEVEL
}

public class CheatManager : MonoBehaviour
{
    HUDisplay hud;
    PlayerHealth playerHealth;
    PlayerCurrency playerCurrency;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    LevelManager levelManager;

    // Orbs
    public GameObject increaseDamageOrbPrefab; // Increase Damage Orb
    public GameObject restoreHealthOrbPrefab; // Restore Health Orb
    public GameObject increaseSpeedOrbPrefab; // Increase Speed Orb

    string textInput;
    public InputField inputField;
    int prevBalance;

    bool[] cheats = new bool[8];

    private void Start()
    {
        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerCurrency = GameObject.Find("Player").GetComponent<PlayerCurrency>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>(); 
        playerShooting = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        // Reason: the read string conditionals do not contain Y or Z character
        // Open input field by pressing Y key
        if (UnityEngine.Input.GetKeyDown(KeyCode.Y))
        {
            hud.OpenInput();
        }

        // Close input field by pressing Z key
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z))
        {
            hud.CloseInput();
        }
    }

    public void ReadStringInput(string text)
    {
        textInput = text;

        // Activate cheats based on text input
        if (textInput == "NODAMAGE")
        {
            ResetInputField();
            ActivateNoDamage();
            return;
        }
        if (textInput == "ONEHITKILL")
        {
            ResetInputField();
            ActivateOneHitKill();
            return;
        }
        if (textInput == "MOTHERLODE")
        {
            ResetInputField();
            ActivateMotherlode();
            return;
        }
        if (textInput == "XTWOSPEED")
        {
            ResetInputField();
            ActivateXTwoSpeed();
            return;
        }
        if (textInput == "FULLHPPET")
        {
            ResetInputField();
            ActivateFullHPPet();
            return;
        }
        if (textInput == "KILLPET")
        {
            ResetInputField();
            ActivateKillPet();
            return;
        }
        if (textInput == "GETORB")
        {
            ResetInputField();
            ActivateGetRandomOrb();
            return;
        }
        if (textInput == "SKIPLEVEL")
        {
            ResetInputField();
            ActivateSkipLevel();
            return;
        }
        if (textInput == "RESETCHEATS")
        {
            ResetInputField();
            ActivateReset();
            return;
        }
        return;
    }

    private void ResetInputField()
    {
        // Reset the input field text
        inputField.text = "";

        // Close the input field
        hud.CloseInput();
    }

    private void ActivateNoDamage()
    {
        playerHealth.SetCheatNoDamage(true);
        hud.OpenPanel("No Damage Cheat Activated!");
        cheats[(int)CheatsType.NODAMAGE] = true;
    }

    private void ActivateOneHitKill()
    {
        playerShooting.ActivateCheatOneHitKill();
        hud.OpenPanel("One Hit Kill Cheat Activated!");
        cheats[(int)CheatsType.ONEHITKILL] = true;
    }

    private void ActivateMotherlode()
    {
        prevBalance = playerCurrency.balance;
        playerCurrency.balance = 100000;
        hud.OpenPanel("Motherlode Cheat Activated!");
    }

    private void ActivateXTwoSpeed()
    {
        playerMovement.ActivateCheatXTwoSpeed();
        hud.OpenPanel("Two Times Speed Cheat Activated!");
        cheats[(int)CheatsType.XTWOSPEED] = true;
    }

    private void ActivateFullHPPet()
    {
        GameObject[] allyPets = GameObject.FindGameObjectsWithTag("AllyPet");
        foreach (GameObject allyPet in allyPets)
        {
            AllyPetHealth allyPetHealth = allyPet.GetComponent<AllyPetHealth>();
            allyPetHealth.SetCheatFullHPPet(true);
        }
        
        hud.OpenPanel("Full HP Pet Cheat Activated!");
        cheats[(int)CheatsType.FULLHPPET] = true;
    }

    private void ActivateKillPet()
    {
        GameObject[] enemyPets = GameObject.FindGameObjectsWithTag("EnemyPet");
        Debug.Log(enemyPets.Length);
        foreach (GameObject enemyPet in enemyPets)
        {
            EnemyPetHealth enemyPetHealth = enemyPet.GetComponent<EnemyPetHealth>();
            enemyPetHealth.ActivateCheatKillPet();
        }

        hud.OpenPanel("Kill Pet Cheat Activated!");
    }

    private void ActivateGetRandomOrb()
    {
        int orbType = Random.Range(0, 3);
        GameObject orbPrefab;

        switch (orbType)
        {
            case 0:
                orbPrefab = increaseDamageOrbPrefab;
                break;
            case 1:
                orbPrefab = restoreHealthOrbPrefab;
                break;
            case 2:
                orbPrefab = increaseSpeedOrbPrefab;
                break;
            default:
                Debug.LogError("Invalid orb type");
                return;
        }

        if (orbPrefab != null)
        {
            GameObject orbInstance = Instantiate(orbPrefab, playerMovement.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Orb prefab is null");
        }

        hud.OpenPanel("Get Random Orb Cheat Activated!");
        cheats[(int)CheatsType.GETORB] = true;
    }

    private void ActivateSkipLevel()
    {
        levelManager.AdvanceLevel();
        hud.OpenPanel("Skip Level Cheat Activated!");
        cheats[(int)CheatsType.SKIPLEVEL] = true;
    }

    private void ActivateReset()
    {
        playerHealth.SetCheatNoDamage(false);
        playerCurrency.balance = prevBalance;
        playerMovement.ResetSpeed();
        playerShooting.ResetPlayerDamage();

        GameObject[] allyPets = GameObject.FindGameObjectsWithTag("AllyPet");
        foreach (GameObject allyPet in allyPets)
        {
            AllyPetHealth allyPetHealth = allyPet.GetComponent<AllyPetHealth>();
            allyPetHealth.SetCheatFullHPPet(false);
        }

        hud.OpenPanel("Successfully Reset Cheat(s)!");
    }

    public void LoadCheat(bool[] gatheredCheats)
    {
        if (gatheredCheats[(int)CheatsType.NODAMAGE])
        {
            ActivateNoDamage();
        }
        if (gatheredCheats[(int)CheatsType.ONEHITKILL])
        {
            ActivateOneHitKill();
        }
        if (gatheredCheats[(int)CheatsType.XTWOSPEED])
        {
            ActivateXTwoSpeed();
        }
    }
}
