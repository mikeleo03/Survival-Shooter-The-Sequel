using Nightmare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    LevelManager levelManager;

    // Orbs
    public GameObject increaseDamageOrbPrefab; // Increase Damage Orb
    public GameObject restoreHealthOrbPrefab; // Restore Health Orb
    public GameObject increaseSpeedOrbPrefab; // Increase Speed Orb

    string textInput;
    public InputField inputField;
    bool cheatOpened;

    Controls control;
    InputAction cheatInput;

    bool[] cheats = new bool[4];

    private void Awake()
    {
        control = new Controls();
        cheatInput = control.Player.Cheat;
        cheatOpened = false;

        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerShooting = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnEnable()
    {
        cheatInput.Enable();

        cheatInput.performed += ToggleCheat;
    }

    private void OnDisable()
    {
        cheatInput.Disable();

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
        if (textInput == "XTWOSPEED")
        {
            ResetInputField();
            ActivateXTwoSpeed();
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
