using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Nightmare;

public class CountdownManager : MonoBehaviour {

    public int countdownTime;
    private int countdown;
    public Text countdownDisplay;
    private bool isRunning = false;
    private Coroutine countdownCoroutine;
    LevelManager lm;

    // Start the countdown coroutine if it's set to start automatically
    void Start() {
        countdown = countdownTime;
        lm = FindObjectOfType<LevelManager>();
        if (isRunning) {
            StartCountdown();
        }
    }

    private IEnumerator CountdownToStart() {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "0";
        StopCountdown();

        if (countdown == 10)
        {
            LoadMenuScene();
        } 
        else
        {
            lm.AdvanceLevel();
        }

        ResetCountdown();
    }

    private void LoadMenuScene() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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
