using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Nightmare;


public class DataPersistanceManager : MonoBehaviour
{
    [Header("Pet Prefabs")]

    [SerializeField] public GameObject healingPet;
    [SerializeField] public GameObject attackingPet;

    public string fileName;

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;


    public static DataPersistanceManager instance { get; private set; }


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        // this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        // load any saved data form a file using the data handler
        this.gameData = dataHandler.Load();


        // if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            NewGame();
        }
        else
        {
            if (this.gameData.healingPetHealths.Count != 0)
            {
                foreach (int healingPetHealth in this.gameData.healingPetHealths)
                {
                    GameObject pet = Instantiate(healingPet, gameData.playerPosition, Quaternion.identity);
                    AllyPetHealth petHealthScript = pet.GetComponent<AllyPetHealth>();
                    if (petHealthScript != null)
                    {
                        petHealthScript.currentHealth = healingPetHealth;
                    }
                }
                this.gameData.healingPetHealths.Clear();
            }
            if (this.gameData.attackingPetHealths.Count != 0)
            {
                foreach (int attackingPetHealth in this.gameData.attackingPetHealths)
                {
                    GameObject pet = Instantiate(attackingPet, gameData.playerPosition, Quaternion.identity);
                    AllyPetHealth petHealthScript = pet.GetComponent<AllyPetHealth>();
                    if (petHealthScript != null)
                    {
                        petHealthScript.currentHealth = attackingPetHealth;
                    }
                }
                this.gameData.attackingPetHealths.Clear();
            }
        }

        this.dataPersistanceObjects = FindAllDataPersistanceObjects();

        // push the loaded data to all other scipts that need it
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }

    }
    public void SaveGame()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        this.dataPersistanceObjects = FindAllDataPersistanceObjects();

        // pass the data to other scripts so they can update it 
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    // private void OnApplicationQuit()
    // {
    //     SaveGame();
    // }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

}
