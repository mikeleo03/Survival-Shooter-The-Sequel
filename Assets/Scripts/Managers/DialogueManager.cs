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
    [SerializeField] private Canvas QuestCanvas;
    [SerializeField] private Canvas DialogueCanvas;
    [SerializeField] private GameObject TalkerBox;

    private int index;
    private bool isDialogueFinished = false;
    private Controls controls;
    private InputAction click;

    private void Awake()
    {
        controls = new Controls();
        click = controls.UI.Click;

        Time.timeScale = 0;
        QuestCanvas.enabled = false;
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
        if (isDialogueFinished == false)
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
                // TalkerBox.SetActive(!string.IsNullOrEmpty(talker[index]));
            }
        }
        else
        {
            if (QuestCanvas.enabled == false)
            {
                QuestCanvas.enabled = true;
                DialogueCanvas.enabled = false;
            } 
            else
            {
                QuestCanvas.enabled = false;
                gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        // Check if talkerComponent is empty and disable the TalkerBox
        if (TalkerBox != null)
        {
            if (string.IsNullOrEmpty(talkerComponent.text))
            {
                TalkerBox.SetActive(false);
            }
            else
            {
                TalkerBox.SetActive(true);
            }
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

            // Check is it end?
            if (index == lines.Length - 1)
            {
                isDialogueFinished = true;
            }
        }
    }
}