using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Nightmare;

public class LoadSlotButton : MonoBehaviour
{
    public int slot;

    GameData gameData;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    // private DataPersistanceManager dataPersistanceManager;

    string slotTitle;
    Text slotTitleTextComponent;

    // Start is called before the first frame update
    void Start()
    {
        this.gameData = (new FileDataHandler(Application.persistentDataPath, fileName)).Load();

        // this.dataPersistanceManager = GameObject.Find("DataPersistanceManager").GetComponent<DataPersistanceManager>();
        this.slotTitleTextComponent = GetComponentInChildren<Text>();
        this.slotTitle = this.gameData == null ? "" : this.gameData.saveName ;
        this.slotTitleTextComponent.text = this.slotTitle;
    }

    // Update is called once per frame
    void Update()
    {
        this.slotTitleTextComponent.text = this.slotTitle;
    }
    // public void LoadData(GameData data)
    // {
    //     this.slotTitle = data.saveName;
    // }

    // public void SaveData(ref GameData data)
    // {
    //     data.saveName = this.slotTitle;
    //     data.currentDateTicks = DateTime.Now.Ticks;
    // }


    // public void SaveGame()
    // {
    //     this.slotTitle = PlayerPrefs.GetString("PlayerName") + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
    //     this.dataPersistanceManager.fileName = this.fileName;
    //     this.dataPersistanceManager.SaveGame();
    // }

    public void LoadGame()
    {
        DataPersistanceManager.loadedFileName = fileName;
        Debug.Log(DataPersistanceManager.loadedFileName);

        GameManager.startGame();
    }
}
