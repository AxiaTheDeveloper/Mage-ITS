using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
class PlayerData
{
    public int[] levelScore;
}
public class GameSaveManager : MonoBehaviour
{
    public void SaveData(PlayerSaveScriptableObject playerSO)
    {
        string basePath = Application.persistentDataPath;
        
        string playerSavePath = Path.Combine(basePath, "ps.dat");

        PlayerData pd = new PlayerData();
        pd.levelScore = playerSO.levelScore;

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
        playerSO.levelScore = pd.levelScore;
    }
}
