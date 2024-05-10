using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveSlotButton : MonoBehaviour, IDataPersistance
{
    public int slot;

    [Header("File Storage Config")]
    public string fileName;

    private DataPersistanceManager dataPersistanceManager;

    string slotTitle;
    Text slotTitleTextComponent;

    // Start is called before the first frame update
    void Start()
    {
        // this.dataHandler = (new FileDataHandler(Application.persistentDataPath, fileName));

        this.dataPersistanceManager = GameObject.Find("DataPersistanceManager").GetComponent<DataPersistanceManager>();
        this.slotTitleTextComponent = GetComponentInChildren<Text>();
        // this.slotTitle = (new FileDataHandler(Application.persistentDataPath, this.fileName)).Load().saveName;
        this.slotTitle = (new FileDataHandler(Application.persistentDataPath, this.fileName)).Load() == null ? "" : (new FileDataHandler(Application.persistentDataPath, this.fileName)).Load().saveName;
        this.slotTitleTextComponent.text = this.slotTitle;
    }

    // Update is called once per frame
    void Update()
    {
        this.slotTitleTextComponent.text = this.slotTitle;
    }
    public void LoadData(GameData data)
    {
        this.slotTitle = data.saveName;
    }

    public void SaveData(ref GameData data)
    {
        data.saveName = this.slotTitle;
        data.currentDateTicks = DateTime.Now.Ticks;
    }


    public void SaveGame()
    {
        this.slotTitle = PlayerPrefs.GetString("PlayerName") + "_" + DateTime.Now.ToString("MM-dd HH-mm-ss");
        this.dataPersistanceManager.fileName = this.fileName;
        this.dataPersistanceManager.SaveGame();
    }
}
