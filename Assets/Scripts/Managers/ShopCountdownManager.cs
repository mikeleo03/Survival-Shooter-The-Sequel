using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Nightmare;

public class ShopCountdownManager : MonoBehaviour {

    public int countdownTime;
    public Text countdownShop;
    private Coroutine countdownCoroutine;
    LevelManager lm;

    // Start the countdown coroutine if it's set to start automatically
    void Start() {
        lm = FindObjectOfType<LevelManager>();
        countdownCoroutine = StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart() {
        while (countdownTime > 0)
        {
            countdownShop.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownShop.text = "0";
        StopCountdown();
        NextLevel();
        ResetCountdown();
    }

    private void NextLevel()
    {
        lm.AdvanceLevel();
    }

    public void ResetCountdown() {
        countdownTime = 30;
    }

    public void StopCountdown() {
        if (countdownCoroutine != null) {
            StopCoroutine(countdownCoroutine);
        }
    }
}
