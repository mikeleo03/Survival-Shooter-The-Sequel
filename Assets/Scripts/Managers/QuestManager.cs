using Nightmare;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] Canvas CompletedCanvas;

    PlayerCurrency playerCurr;
    InputAction click;

    private void Awake()
    {
        click = ControlRef.control.UI.Click;
        currQuest = questList[0];
        CompletedCanvas.enabled = false;
        loadedNext = false;
        playerCurr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public Quest getCurrentQuest()
    {
        return currQuest;
    }

    void Update()
    {
        if (click.IsPressed()) 
        {
            if (CompletedCanvas.enabled == true)
            {
                rewardPlayer();
                CompletedCanvas.enabled = false;
                Time.timeScale = 1;
                if (lm.GetCurrLevel() < 3)
                {
                    SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
                }
                else
                {
                    SceneManager.LoadScene("Cutscene02", LoadSceneMode.Single);
                }
            }
        }
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

    void rewardPlayer()
    {
        int reward = 200 * (lm.GetCurrLevel() +  1); 
        playerCurr.add(reward);
    }
}
