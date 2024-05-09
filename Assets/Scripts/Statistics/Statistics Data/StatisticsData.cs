using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StatisticsData
{
    public int shotsFired;
    public int shotsHit;
    public double shotAccuracy;
    public double distanceTraveled;
    public double playTime;
    public int highScore;
    public int allTimeScore;
    public int enemiesKilled;

    public StatisticsData()
    {
        this.shotsFired = 0;
        this.shotsHit = 0;
        this.shotAccuracy = 0;
        this.distanceTraveled = 0;
        this.playTime = 0;
        this.highScore = 0;
        this.allTimeScore = 0;
        this.enemiesKilled = 0;
    }
}
