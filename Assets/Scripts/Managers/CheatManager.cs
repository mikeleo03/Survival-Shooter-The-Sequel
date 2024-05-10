using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    bool cheatOpened;
    InputAction cheatInput;

    bool[] cheats = new bool[3];

    private void Awake()
    {
        cheatInput = ControlRef.control.Player.Cheat;
        cheatOpened = false;

        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerCurrency = GameObject.Find("Player").GetComponent<PlayerCurrency>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>(); 
        playerShooting = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        cheatInput.performed += ToggleCheat;
    }

    private void OnDisable()
    {
        cheatInput.performed -= ToggleCheat;
    }

    public void ToggleCheat(InputAction.CallbackContext ctx)
    {
        if (cheatOpened)
        {
            hud.CloseInput();
        } else
        {
            hud.OpenInput();
        }
        cheatOpened = !cheatOpened;
    }

    public void ReadStringInput(string text)
    {
        textInput = text;
        cheatOpened = false;
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
    }

    private void ActivateOneHitKill()
    {
        playerShooting.ActivateCheatOneHitKill();
        hud.OpenPanel("One Hit Kill Cheat Activated!");
    }

    private void ActivateMotherlode()
    {
        playerCurrency.ActivateMotherlode();
        hud.OpenPanel("Motherlode Cheat Activated!");
    }

    private void ActivateXTwoSpeed()
    {
        playerMovement.ActivateCheatXTwoSpeed();
        hud.OpenPanel("Two Times Speed Cheat Activated!");
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
    }

    private void ActivateKillPet()
    {
        GameObject[] enemyPets = GameObject.FindGameObjectsWithTag("EnemyPet");
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
    }

    private void ActivateSkipLevel()
    {
        levelManager.AdvanceLevel();
        hud.OpenPanel("Skip Level Cheat Activated!");
    }

    /* RESET CHEATS DOCUMENTATION */
    /*
     * Makes player can take damage again
     * Reset player's balance to previous balance
     * Reset player's speed
     * Reset player's attack damage
     * Makes player's pets can take damage again
     */

    private void ActivateReset()
    {
        playerHealth.SetCheatNoDamage(false);
        playerCurrency.ResetMotherlode();
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
}
