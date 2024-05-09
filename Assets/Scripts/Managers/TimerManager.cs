using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    public Text Timer;
    private double time = 0;
    private bool isRunning = false;

    private void Awake() {
        StartTimer();
    }

    // Update is called once per frame
    void Update() {
        if (isRunning) {
            time = time + Time.deltaTime;
            TextStatistics.playTime += Time.deltaTime;
            InGameTextStatistics.playTime += Time.deltaTime;
        }
        string timeText = System.TimeSpan.FromSeconds(time).ToString("mm':'ss");
        Timer.text = timeText;
    }

    public void ResetTimer() {
        time = 0;
    }

    public double TakeTime() {
        StopTimer();
        var r = time;
        ResetTimer();
        return r;
    }

    public double GetCurrentTime()
    {
        return time;
    }

    public void StartTimer() {
        isRunning = true;
    }

    public void StopTimer() {
        isRunning = false;
    }
}
