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
    string textInput;
    public InputField inputField;

    // Cheats
    bool[] cheats = new bool[4];

    private void Start()
    {
        hud = GameObject.Find("HUDCanvas").GetComponent<HUDisplay>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Y)) // Open
        {
            hud.OpenInput();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.Z)) // Close
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
        // TODO
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
