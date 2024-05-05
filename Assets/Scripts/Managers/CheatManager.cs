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
    EnemyHealth enemyHealth;
    string textInput;
    public InputField inputField;
    bool cheatOpened;

    Controls control;
    InputAction cheatInput;

    // Cheats
    bool[] cheats = new bool[4];

    private void Awake()
    {
        control = new Controls();
        cheatInput = control.Player.Cheat;
        cheatOpened = false;

        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        enemyHealth = GameObject.Find("GameManager").GetComponent<EnemyHealth>();
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
            ActivateNoDamage();
            // Reset the input field text
            inputField.text = "";

            // Close the input field
            hud.CloseInput();
            return;
        }
        if (textInput == "ONEHITKILL")
        {
            ActivateOneHitKill();
            // Reset the input field text
            inputField.text = "";

            // Close the input field
            hud.CloseInput();
            return;
        }
        if (textInput == "XTWOSPEED")
        {
            ActivateXTwoSpeed();
            // Reset the input field text
            inputField.text = "";

            // Close the input field
            hud.CloseInput();
            return;
        }
        if (textInput == "RESETCHEATS")
        {
            ActivateReset();
            // Reset the input field text
            inputField.text = "";

            // Close the input field
            hud.CloseInput();
            return;
        }
    }

    private void ActivateNoDamage()
    {
        playerHealth.SetCheatNoDamage(true);
        hud.OpenPanel("No Damage Cheat Activated!");
        cheats[(int)CheatsType.NODAMAGE] = true;
    }

    private void ActivateOneHitKill()
    {
        enemyHealth.SetCheatOneHitKill(true);
        hud.OpenPanel("One Hit Kill Cheat Activated!");
        cheats[(int)CheatsType.ONEHITKILL] = true;
    }

    private void ActivateXTwoSpeed()
    {
        playerMovement.ActivateCheatXTwoSpeed();
        hud.OpenPanel("Two Times Speed Cheat Activated!");
        cheats[(int)CheatsType.XTWOSPEED] = true;
    }

    private void ActivateReset()
    {
        playerHealth.SetCheatNoDamage(false);
        playerMovement.ResetSpeed();
        enemyHealth.SetCheatOneHitKill(false);
        hud.OpenPanel("Successfully Reset Cheat(s)!");
    }

    public void LoadCheat(bool[] gatheredCheats)
    {
        if (gatheredCheats[(int) CheatsType.NODAMAGE])
        {
            ActivateNoDamage();
        }
        if (gatheredCheats[(int) CheatsType.ONEHITKILL])
        {
            ActivateOneHitKill();
        }
        if (gatheredCheats[(int) CheatsType.XTWOSPEED])
        {
            ActivateXTwoSpeed();
        }
    }

}
