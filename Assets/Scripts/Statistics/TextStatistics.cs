using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.UI;


public class TextStatistics : MonoBehaviour, IStatisticsData
{
    public static int shotsFired;
    public static int shotsHit;
    public static double shotAccuracy;
    public static double distanceTraveled;
    public static double playTime;
    public static int highScore;
    public static int allTimeScore;
    public static int enemiesKilled;

    private Text statistics;

    void Awake()
    {
        TextStatistics.shotsFired = 0;
        TextStatistics.shotsHit = 0;
        TextStatistics.shotAccuracy = 0;
        TextStatistics.distanceTraveled = 0;
        TextStatistics.playTime = 0;
        TextStatistics.highScore = 0;
        TextStatistics.allTimeScore = 0;
        TextStatistics.enemiesKilled = 0;

        this.statistics = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string timeText = System.TimeSpan.FromSeconds(TextStatistics.playTime).ToString("hh':'mm':'ss");
        if (TextStatistics.shotsFired != 0)
        {
            TextStatistics.shotAccuracy = ((double)TextStatistics.shotsHit / TextStatistics.shotsFired) * 100;
        }

        statistics.text = "Shots Fired: " + TextStatistics.shotsFired + "\n"
                           + "Shots Hit: " + TextStatistics.shotsHit + "\n"
                           + "Shot Accuracy: " + TextStatistics.shotAccuracy.ToString("F2") + "%\n"
                           + "Distance Traveled: " + (TextStatistics.distanceTraveled / 1000).ToString("F3") + "km \n"
                           + "Play Time: " + timeText + "\n"
                           + "High Score: " + TextStatistics.highScore + "\n"
                           + "All Time Score: " + TextStatistics.allTimeScore + "\n"
                           + "Enemies Killed: " + TextStatistics.enemiesKilled + "\n";
    }


    public void LoadStatistics(StatisticsData data)
    {
        TextStatistics.shotsFired = data.shotsFired;
        TextStatistics.shotsHit = data.shotsHit;
        TextStatistics.shotAccuracy = data.shotAccuracy;
        TextStatistics.distanceTraveled = data.distanceTraveled;
        TextStatistics.playTime = data.playTime;
        TextStatistics.highScore = data.highScore;
        TextStatistics.allTimeScore = data.allTimeScore;
        TextStatistics.enemiesKilled = data.enemiesKilled;
    }

    public void SaveStatistics(ref StatisticsData data)
    {
        data.shotsFired = TextStatistics.shotsFired;
        data.shotsHit = TextStatistics.shotsHit;
        data.shotAccuracy = TextStatistics.shotAccuracy;
        data.distanceTraveled = TextStatistics.distanceTraveled;
        data.playTime = TextStatistics.playTime;
        data.highScore = InGameTextStatistics.score > TextStatistics.highScore ? InGameTextStatistics.score : TextStatistics.highScore;
        data.allTimeScore = TextStatistics.allTimeScore;
        data.enemiesKilled = TextStatistics.enemiesKilled;
    }

}
