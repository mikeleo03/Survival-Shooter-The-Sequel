using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatisticsData
{
    void LoadStatistics(StatisticsData data);

    void SaveStatistics(ref StatisticsData data);
}
