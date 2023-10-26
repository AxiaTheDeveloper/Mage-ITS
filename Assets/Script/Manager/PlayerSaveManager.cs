using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get;private set;}
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private GameSaveManager gameSaveManager;
    [SerializeField]private PlayerSaveScriptableObject playerSaveSO;
    [SerializeField]private int playerTotalMove;
    [SerializeField]private int score;
    private void Awake() 
    {
        Instance = this;
        gameManager = GetComponent<PuzzleGameManager>();
    }
    public void SaveScore()
    {
        playerSaveSO.levelScore[gameManager.PuzzleLevel() - 1] = score;
        // gameSaveManager.SaveData(playerSaveSO);
    }
    public int Score()
    {
        return score;
    }
    public void CalculateScore()
    {
        //if move segini lalalalala
        score = 1;
    }
    public PlayerSaveScriptableObject GetPlayerSaveSO()
    {
        return playerSaveSO;
    }
    public void PlayerRestart(bool change)
    {
        playerSaveSO.isPlayerRestartLevel = change;
    }
    public bool IsPlayerRestartLevel()
    {
        return playerSaveSO.isPlayerRestartLevel;
    }
}
