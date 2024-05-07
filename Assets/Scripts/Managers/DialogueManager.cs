using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkerComponent;
    public string[] lines;
    public string[] talker;
    public float textSpeed;
    [SerializeField] private Canvas QuestCanvas;
    [SerializeField] private Canvas DialogueCanvas;

    private int index;
    private bool isDialogueFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        QuestCanvas.enabled = false;
        textComponent.text = string.Empty;
        talkerComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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