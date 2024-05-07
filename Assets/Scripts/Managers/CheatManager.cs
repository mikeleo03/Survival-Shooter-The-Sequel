using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

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
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    // Orbs
    public GameObject increaseDamageOrbPrefab; // Increase Damage Orb
    public GameObject restoreHealthOrbPrefab; // Restore Health Orb
    public GameObject increaseSpeedOrbPrefab; // Increase Speed Orb

    string textInput;
    public InputField inputField;

    bool[] cheats = new bool[4];

    private void Start()
    {
        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerShooting = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
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
            ActivateNoDamage();
            ResetInputField();
            return;
        }
        if (textInput == "ONEHITKILL")
        {
            ActivateOneHitKill();
            ResetInputField();
            return;
        }
        if (textInput == "XTWOSPEED")
        {
            ActivateXTwoSpeed();
            ResetInputField();
            return;
        }
        if (textInput == "GETORB")
        {
            ActivateGetRandomOrb();
            ResetInputField();
            return;
        }
        if (textInput == "SKIPLEVEL")
        {
            ActivateSkipLevel();
            // Reset the input field text
            ResetInputField();
            return;
        }
        if (textInput == "RESETCHEATS")
        {
            ActivateReset();
            ResetInputField();
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

    private void ActivateXTwoSpeed()
    {
        playerMovement.ActivateCheatXTwoSpeed();
        hud.OpenPanel("Two Times Speed Cheat Activated!");
        cheats[(int)CheatsType.XTWOSPEED] = true;
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
        Debug.Log("SKIP");
    }

    private void ActivateReset()
    {
        playerHealth.SetCheatNoDamage(false);
        playerMovement.ResetSpeed();
        playerShooting.ResetPlayerDamage();
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
