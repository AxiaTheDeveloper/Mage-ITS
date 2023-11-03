using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
class PlayerData
{
    public PlayerSaveScriptableObject.LevelIdentity[] levelIdentities;
    public bool isFirstTimeEnterGame;
    public bool isFinishTutorial;
}
public class GameSaveManager : MonoBehaviour
{
    public void SaveData(PlayerSaveScriptableObject playerSO)
    {
        string basePath = Application.persistentDataPath;
        
        string playerSavePath = Path.Combine(basePath, "ps.dat");

        PlayerData pd = new PlayerData();
        pd.levelIdentities = playerSO.levelIdentities;
        pd.isFirstTimeEnterGame = playerSO.isFirstTimeEnterGame;
        pd.isFinishTutorial = playerSO.isFinishTutorial;

        File.WriteAllText(playerSavePath, JsonUtility.ToJson(pd));
    }
    public void LoadData(PlayerSaveScriptableObject playerSO)
    {
        string basePath = Application.persistentDataPath;
        
        string playerSavePath = Path.Combine(basePath, "ps.dat");

        if(!File.Exists(playerSavePath)){
            return;
        }
        PlayerData pd = JsonUtility.FromJson<PlayerData>(File.ReadAllText(playerSavePath));
        playerSO.levelIdentities = pd.levelIdentities;
        playerSO.isFirstTimeEnterGame = pd.isFirstTimeEnterGame;
        playerSO.isFinishTutorial = pd.isFinishTutorial;
    }
}
