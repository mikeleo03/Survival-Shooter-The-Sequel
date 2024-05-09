using Nightmare;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTextManager : MonoBehaviour
{
    [SerializeField] int level;
    private QuestManager questManager;
    private Quest currQuest;
    public TextMeshProUGUI questText;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        currQuest = questManager.getCurrentQuest();
        questText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        currQuest = questManager.getCurrentQuest();

        // For quest task 2
        if (level == 0)
        {
            questText.text = "Rangga harus bertahan terhadap serangan keroco selama " + currQuest.timeLimit.ToString() + " detik";
        }
        else if (level == 1)
        {
            questText.text = "Rangga harus melawan semua keroco dan kepala keroco dalam hutan (" + currQuest.kerocoReq + " keroco dan " + currQuest.kepalaReq + " kepala keroco).";
        }
        else if (level == 2)
        {
            questText.text = "Rangga harus mengalahkan " + currQuest.jenderalReq + " jendral keroco dalam " + currQuest.timeLimit.ToString() + " detik.";
        }
        else
        {
            questText.text = "Rangga harus mengalahkan raja keroco termasuk berbagai keroco yang ia keluarkan";
        }
    }
}
