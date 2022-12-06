using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static float trashScore, robotCarTime;
    public static string playerName, benchmarkTime;
    private PlayerData playerData;

    public void RegisterPlayer(string submittedName)
    {
        playerName = submittedName;
        benchmarkTime = "0ms";
        trashScore = 0;
        robotCarTime = 0;
        CreatePlayerData();
    }

    private void CreatePlayerData()
    {
        playerData = new PlayerData();
        UpdatePlayerData();
    }

    public void UpdatePlayerData()
    {
        playerData.playerName = playerName;
        playerData.benchmarkTime = benchmarkTime;
        playerData.robotCarTime = robotCarTime;
        playerData.trashScore = trashScore;
    }
}
