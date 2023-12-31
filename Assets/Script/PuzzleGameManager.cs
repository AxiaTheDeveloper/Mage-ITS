using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance{get; private set;}
    private GameInput gameInput;
    public enum GameState
    {
        WaitingToStart, Start, Pause, Finish
    }
    public enum StartState
    {
        Normal, CheckingAnswer, None
    }
    [SerializeField]private GameState stateGame;
    [SerializeField]private StartState startState, saveStartState;
    private bool isPause;
    private bool isGameFinish, isTileMoving, isTIleRotating;

    private void Awake() 
    {
        Instance = this;
        stateGame = GameState.Start;
        startState = StartState.Normal;
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
    public GameState GetStateGame()
    {
        return stateGame;
    }
    public StartState GetStartState()
    {
        return startState;
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
        isTIleRotating = change;
    }




}
