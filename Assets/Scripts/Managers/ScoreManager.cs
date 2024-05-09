﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        private int levelThreshhold;
        const int LEVEL_INCREASE = 300;

        Text sText;

        void Awake()
        {
            sText = GetComponent<Text>();

            score = 0;
            levelThreshhold = LEVEL_INCREASE;
        }


        void Update()
        {
            ScoreManager.score = InGameTextStatistics.score;
            sText.text = "Score: " + score;
        }

        private void AdvanceLevel()
        {
            levelThreshhold = score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }
    }
}