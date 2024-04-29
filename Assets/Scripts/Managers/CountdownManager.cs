using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownManager : MonoBehaviour {

    public int countdownTime;
    public Text countdownDisplay;
    private bool isRunning = false;
    private Coroutine countdownCoroutine;

    // Start the countdown coroutine if it's set to start automatically
    void Start() {
        if (isRunning) {
            StartCountdown();
        }
    }

    private IEnumerator CountdownToStart() {
        while (countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "0";
        StopCountdown();
        LoadMenuScene();
        ResetCountdown();
    }

    private void LoadMenuScene() {
        SceneManager.LoadScene("Menu");
    }

    public void ResetCountdown() {
        countdownTime = 10;
    }

    public void StartCountdown() {
        if (!isRunning) {
            isRunning = true;
            countdownCoroutine = StartCoroutine(CountdownToStart());
        }
    }

    public void StopCountdown() {
        if (isRunning) {
            isRunning = false;
            if (countdownCoroutine != null) {
                StopCoroutine(countdownCoroutine);
            }
        }
    }
}
