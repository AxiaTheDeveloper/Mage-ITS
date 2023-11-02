using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance{get; private set;}
    [SerializeField]private int level;
    [SerializeField]private LevelMaxMoveScriptableObject levelMaxMoveSO;
    [SerializeField]private int[] maxMove = new int[3];
    private GameInput gameInput;
    public enum GameState
    {
        WaitingToStart, Start, Pause, Finish, MainMenuMode
    }
    public enum StartState
    {
        Normal, CheckingAnswer, None
    }
    [SerializeField]private GameState stateGame;
    [SerializeField]private StartState startState, saveStartState;
    private bool isPause;
    private bool isGameFinish, isTileMoving, isTIleRotating;

    public event EventHandler OnRotatingTile, OnFinishGame; //
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private StarControlUI starControlUI;

    private void Awake() 
    {
        Instance = this;
        stateGame = GameState.WaitingToStart;
        startState = StartState.None;
        
        playerSaveManager = GetComponent<PlayerSaveManager>();
        maxMove = levelMaxMoveSO.MaxMovePerLevel[level-1].maxMove;
        starControlUI.ChangeTotalMoves(maxMove);
    }
    private void Start() 
    {
        gameInput = GameInput.Instance;
    }
    private void Update() 
    {
        //sementara
        if(gameInput.GetPauseInput() && (stateGame == GameState.Start || stateGame == GameState.Pause))
        {
            Pause();
        }
    }
    public int PuzzleLevel()
    {
        return level;
    }
    public GameState GetStateGame()
    {
        return stateGame;
    }
    public StartState GetStartState()
    {
        return startState;
    }
    public void WaitToStart()
    {
        stateGame = GameState.WaitingToStart;
        startState = StartState.None;
    }
    public void MainMenuMode()
    {
        stateGame = GameState.MainMenuMode;
        startState = StartState.None;
    }
    public void StartGame()
    {
        stateGame = GameState.Start;
        startState = StartState.Normal;
    }
    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            stateGame = GameState.Pause;
            saveStartState = startState;
            startState = StartState.None;
        }
        else
        {
            stateGame = GameState.Start;
            startState = saveStartState;
        }
    }
    public void StartChecking()
    {
        stateGame = GameState.Start;
        startState = StartState.CheckingAnswer;
    }
    public void FinishGame()
    {
        stateGame = GameState.Finish;
        startState = StartState.None;
        int score = CalculatingScore(playerSaveManager.GetPlayerMove());
        starControlUI.ChangeStarsVisual(score);
        //UI diubah lwt sini
        // Debug.Log(score);
        playerSaveManager.SaveScore(score);
        OnFinishGame?.Invoke(this,EventArgs.Empty);
    }

    public bool IsTileMoving()
    {
        return isTileMoving;
    }
    public void ChangeIsTileMoving(bool change)
    {
        isTileMoving = change;
    }
    public bool IsTIleRotating()
    {
        return isTIleRotating;
    }
    public void ChangeIsTileRotating(bool change)
    {
        if(change)
        {
            OnRotatingTile?.Invoke(this, EventArgs.Empty);
        }
        isTIleRotating = change;
    }

    public int CalculatingScore(int totalPlayerMove)
    {
        if(totalPlayerMove <= maxMove[0])
        {
            return 3;
        }
        else if(totalPlayerMove <= maxMove[1])
        {
            return 2;
        }
        else if(totalPlayerMove <= maxMove[2])
        {
            return 1;
        }
        return 0;
    }
    public int[] GetMaxMoves()
    {
        return maxMove;
    }
    



}
