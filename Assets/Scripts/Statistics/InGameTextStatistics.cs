using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.UI;


public class InGameTextStatistics : MonoBehaviour, IDataPersistance
{
    public static int score;
    public static int shotsFired;
    public static int shotsHit;
    public static double shotAccuracy;
    public static double distanceTraveled;
    public static double playTime;
    public static int enemiesKilled;

    public Text statistics;

    void Awake()
    {
        InGameTextStatistics.score = 0;
        InGameTextStatistics.shotsFired = 0;
        InGameTextStatistics.shotsHit = 0;
        InGameTextStatistics.shotAccuracy = 0;
        InGameTextStatistics.distanceTraveled = 0;
        InGameTextStatistics.playTime = 0;
        InGameTextStatistics.enemiesKilled = 0;

        this.statistics = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string timeText = System.TimeSpan.FromSeconds(InGameTextStatistics.playTime).ToString("mm':'ss");
        if (InGameTextStatistics.shotsFired != 0)
        {
            InGameTextStatistics.shotAccuracy = ((double)InGameTextStatistics.shotsHit / InGameTextStatistics.shotsFired) * 100;
        }

        statistics.text = "Score: " + InGameTextStatistics.score + "\n"
                           + "Shots Fired: " + InGameTextStatistics.shotsFired + "\n"
                           + "Shots Hit: " + InGameTextStatistics.shotsHit + "\n"
                           + "Shot Accuracy: " + InGameTextStatistics.shotAccuracy.ToString("F2") + "%\n"
                           + "Distance Traveled: " + (InGameTextStatistics.distanceTraveled / 1000).ToString("F3") + "km \n"
                           + "Play Time: " + timeText + "\n"
                           + "Enemies Killed: " + InGameTextStatistics.enemiesKilled + "\n";
    }


    public void LoadData(GameData data)
    {
        InGameTextStatistics.score = data.score;
        InGameTextStatistics.shotsFired = data.shotsFired;
        InGameTextStatistics.shotsHit = data.shotsHit;
        InGameTextStatistics.shotAccuracy = data.shotAccuracy;
        InGameTextStatistics.distanceTraveled = data.distanceTraveled;
        InGameTextStatistics.playTime = data.playTime;
        InGameTextStatistics.enemiesKilled = data.enemiesKilled;
    }

    public void SaveData(ref GameData data)
    {
        data.score = InGameTextStatistics.score;
        data.shotsFired = InGameTextStatistics.shotsFired;
        data.shotsHit = InGameTextStatistics.shotsHit;
        data.shotAccuracy = InGameTextStatistics.shotAccuracy;
        data.distanceTraveled = InGameTextStatistics.distanceTraveled;
        data.playTime = InGameTextStatistics.playTime;
        data.enemiesKilled = InGameTextStatistics.enemiesKilled;
    }

}
