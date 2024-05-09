using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatisticsManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileNameStatistics;

    private StatisticsData statisticsData;

    private List<IStatisticsData> statisticsDataObjects;
    private FileStatisticsHandler dataHandler;


    public static StatisticsManager instance { get; private set; }


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Statistics Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileStatisticsHandler(Application.persistentDataPath, fileNameStatistics);
        this.statisticsDataObjects = FindAllStatisticsObjects();
        LoadStatistics();
    }

    public void NewStatistics()
    {
        this.statisticsData = new StatisticsData();
    }

    public void LoadStatistics()
    {
        // load any saved data form a file using the data handler
        this.statisticsData = dataHandler.Load();


        // if no data can be loaded, initialize to a new game
        if (this.statisticsData == null)
        {
            NewStatistics();
        }

        // push the loaded data to all other scipts that need it
        foreach (IStatisticsData statisticsDataObj in statisticsDataObjects)
        {
            statisticsDataObj.LoadStatistics(statisticsData);
        }

    }
    public void SaveStatistics()
    {
        // pass the data to other scripts so they can update it 
        foreach (IStatisticsData statisticsDataObj in statisticsDataObjects)
        {
            statisticsDataObj.SaveStatistics(ref statisticsData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(statisticsData);
    }

    private void OnApplicationQuit()
    {
        SaveStatistics();
    }

    private List<IStatisticsData> FindAllStatisticsObjects()
    {
        IEnumerable<IStatisticsData> statisticsDataObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IStatisticsData>();

        return new List<IStatisticsData>(statisticsDataObjects);
    }

}
