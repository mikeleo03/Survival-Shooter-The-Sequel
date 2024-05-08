using Nightmare;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
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
    [SerializeField] Canvas CompletedCanvas;

    private void Awake()
    {
        currQuest = questList[0];
        CompletedCanvas.enabled = false;
        loadedNext = false;
    }

    public Quest getCurrentQuest()
    {
        return currQuest;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (CompletedCanvas.enabled == true)
            {
                CompletedCanvas.enabled = false;
                Time.timeScale = 1;
                if (lm.GetCurrLevel() < 3)
                {
                    lm.AdvanceLevel();
                }
                else
                {
                    SceneManager.LoadScene("Cutscene02", LoadSceneMode.Single);
                }
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void LateUpdate()
    {
        questText.text = "Quest\n";
        if (currQuest.timeLimit > 0)
        {
            questText.text += "Batas waktu : " + 
                currQuest.timeLimit.ToString()
                + " detik\n";
            double currTime = timerManager.GetCurrentTime();
            if (currTime >= currQuest.timeLimit)
            {
                if (currQuest.winIfTimeLimit && !loadedNext)
                {
                    loadedNext = true;
                    timerManager.ResetTimer();
                    if (CompletedCanvas.enabled == false)
                    {
                        Time.timeScale = 0;
                        CompletedCanvas.enabled = true;
                    }
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
            questText.text += "Musuh tersisa\n";
            questText.text += currQuest.kerocoReq > 0 ? 
                "- Keroco : " + kerocoCount.ToString() + "/" + currQuest.kerocoReq.ToString() + "\n" : "";
            questText.text += currQuest.kepalaReq > 0 ?
                "- Kepala Keroco : " + kepalaCount.ToString() + "/" + currQuest.kepalaReq.ToString() + "\n" : "";
            questText.text += currQuest.jenderalReq > 0 ?
                "- Jenderal : " + jenderalCount.ToString() + "/" + currQuest.jenderalReq.ToString() + "\n" : "";
            questText.text += currQuest.rajaReq > 0 ?
                "- Raja : " + rajaCount.ToString() + "/" + currQuest.rajaReq.ToString() + "\n" : "";
            if (kerocoCount >= currQuest.kerocoReq && kepalaCount >= currQuest.kepalaReq &&
                jenderalCount >= currQuest.jenderalReq && rajaCount >= currQuest.rajaReq
                && !loadedNext)
            {
                loadedNext = true;
                timerManager.ResetTimer();
                if (CompletedCanvas.enabled == false)
                {
                    Time.timeScale = 0;
                    CompletedCanvas.enabled = true;
                }
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
