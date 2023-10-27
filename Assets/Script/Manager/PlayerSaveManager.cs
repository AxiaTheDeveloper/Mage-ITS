using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get;private set;}
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private GameSaveManager gameSaveManager;
    [SerializeField]private PlayerSaveScriptableObject playerSaveSO;
    [SerializeField]private int playerTotalMove = 0;
    [SerializeField]private int score;
    public event EventHandler OnChangeMove;
    private void Awake() 
    {
        Instance = this;
        gameManager = GetComponent<PuzzleGameManager>();
    }
    public void SaveScore()
    {
        playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelScore = score;
        playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelDone = true;
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
    public void AddPlayerMove()
    {
        playerTotalMove++;
        OnChangeMove?.Invoke(this,EventArgs.Empty);
        // Debug.Log(playerTotalMove);
    }
    public int GetPlayerMove()
    {
        return playerTotalMove;
    }
}
