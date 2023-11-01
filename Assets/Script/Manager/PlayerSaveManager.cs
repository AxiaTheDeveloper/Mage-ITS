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
    public void SaveScore(int score)
    {
        
        if(!playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelDone)
        {
            playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelScore = score;
            playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelDone = true;
            if(gameManager.PuzzleLevel() != playerSaveSO.levelIdentities.Length)
            {
                playerSaveSO.levelIdentities[gameManager.PuzzleLevel()].levelUnlocked = true;
            }
        }
        else
        {
            if(score > playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelScore)playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelScore = score;

        }
        
        
        // gameSaveManager.SaveData(playerSaveSO);
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
    public int GetTotalLevel()
    {
        return playerSaveSO.levelIdentities.Length;
    }
    public bool IsLevelDone()
    {
        return playerSaveSO.levelIdentities[gameManager.PuzzleLevel() - 1].levelDone;
    }

    
}