using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkerComponent;
    public string[] lines;
    public string[] talker;
    public float textSpeed;

    private int index;
    private Controls controls;
    private InputAction click;

    private void Awake()
    {
        controls = new Controls();
        click = controls.UI.Click;

        Time.timeScale = 0;
        textComponent.text = string.Empty;
        talkerComponent.text = string.Empty;
        StartDialogue();
    }

    private void OnEnable()
    {
        click.Enable();

        click.performed += AdvanceDialog;
    }

    private void OnDisable()
    {
        click.performed -= AdvanceDialog;

        click.Disable();
    }

    private void AdvanceDialog(InputAction.CallbackContext ctx)
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
            talkerComponent.text = talker[index];
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        talkerComponent.text = talker[index];
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            talkerComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}