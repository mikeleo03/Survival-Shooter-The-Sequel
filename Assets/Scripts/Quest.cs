using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public double timeLimit;
    public bool winIfTimeLimit;
    public int kerocoReq, kepalaReq, jenderalReq, rajaReq;

    private void Awake()
    {
        int difficultyLvl = PlayerPrefs.GetInt("Difficulty", 0);
        if (difficultyLvl == 1)
        {
            kerocoReq *= 2;
            kepalaReq *= 2;
            jenderalReq *= 2;
        }
        else if (difficultyLvl == 2)
        {
            kerocoReq *= 3;
            kepalaReq *= 3;
            jenderalReq *= 3;
        }
    }
}
