using Nightmare;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest[] questList;
    private Quest currQuest;
    private bool loadedNext;
    public static int kerocoCount, kepalaCount, jenderalCount, rajaCount;
    [SerializeField] TimerManager timerManager;
    [SerializeField] LevelManager lm;
    [SerializeField] PlayerHealth ph;
    [SerializeField] Text questText;

    private void Awake()
    {
        currQuest = questList[0];
        loadedNext = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LateUpdate()
    {
        questText.text = "Quest : \n";
        if (currQuest.timeLimit > 0)
        {
            questText.text += "Time limit : " + 
                currQuest.timeLimit.ToString()
                + " seconds\n";
            double currTime = timerManager.GetCurrentTime();
            if (currTime >= currQuest.timeLimit)
            {
                if (currQuest.winIfTimeLimit && !loadedNext)
                {
                    loadedNext = true;
                    timerManager.ResetTimer();
                    lm.AdvanceLevel();
                }
                else
                {
                    // Game over
                    ph.currentHealth = 0;
                }
            }
        }

        if (currQuest.kerocoReq > 0 || currQuest.kepalaReq > 0 || currQuest.jenderalReq > 0 || currQuest.rajaReq > 0)
        {
            questText.text += "Enemies left :\n";
            questText.text += currQuest.kerocoReq > 0 ? 
                "Keroco : " + kerocoCount.ToString() + "/" + currQuest.kerocoReq.ToString() + "\n" : "";
            questText.text += currQuest.kepalaReq > 0 ?
                "Kepala Keroco : " + kepalaCount.ToString() + "/" + currQuest.kepalaReq.ToString() + "\n" : "";
            questText.text += currQuest.jenderalReq > 0 ?
                "Jenderal : " + jenderalCount.ToString() + "/" + currQuest.jenderalReq.ToString() + "\n" : "";
            questText.text += currQuest.rajaReq > 0 ?
                "Raja : " + rajaCount.ToString() + "/" + currQuest.rajaReq.ToString() + "\n" : "";
            if (kerocoCount >= currQuest.kerocoReq && kepalaCount >= currQuest.kepalaReq &&
                jenderalCount >= currQuest.jenderalReq && rajaCount >= currQuest.rajaReq
                && !loadedNext)
            {
                loadedNext = true;
                timerManager.ResetTimer();
                lm.AdvanceLevel();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode != LoadSceneMode.Additive)
            return;
        loadedNext = false;
        currQuest = questList[lm.GetCurrLevel() % questList.Length];
        kerocoCount = 0;
        kepalaCount = 0;
        jenderalCount = 0;
        rajaCount = 0;
    }
}
