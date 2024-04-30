using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI; // For Text

public class HUDisplay : MonoBehaviour
{
    public GameObject messagePanel;
    private Text textComponent;
    [SerializeField] GameObject textInput;

    private void Start()
    {
        // Find Text element on screen
        textComponent = messagePanel.transform.Find("Text").GetComponent<Text>();

        // False as default
        messagePanel.SetActive(false);
        textInput.SetActive(false);
    }

    public void OpenPanel(string text)
    {
        textComponent.text = text;
        messagePanel.SetActive(true);
        StartCoroutine(delayExec(2));
    }

    public void ClosePanel()
    {
        messagePanel.SetActive(false);
    }

    public void OpenInput()
    {
        textInput.SetActive(true);
    }

    public void CloseInput()
    {
        textInput.SetActive(false);
    }

    private IEnumerator delayExec(int sec)
    {
        yield return new WaitForSeconds(sec);
        ClosePanel();
    }
}
