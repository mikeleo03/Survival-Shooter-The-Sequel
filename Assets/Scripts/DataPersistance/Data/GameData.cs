using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    // public int timer;
    public int playerHealth;
    public float damagePercent;

    public Vector3 playerPosition;
    public Vector3 cameraPosition;

    public int score;
    public int shotsFired;
    public int shotsHit;
    public double shotAccuracy;
    public double distanceTraveled;
    public double playTime;
    public int highScore;
    public int allTimeScore;
    public int enemiesKilled;


    public GameData()
    {
        this.playerHealth = 100;
        this.damagePercent = 1;
        this.playerPosition = Vector3.zero;
        this.cameraPosition = Vector3.zero;

        // Statistics
        this.score = 0;
        this.shotsFired = 0;
        this.shotsHit = 0;
        this.shotAccuracy = 0;
        this.distanceTraveled = 0;
        this.playTime = 0;
        this.enemiesKilled = 0;
    }
}