using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private PauseUI pauseUI;
    [SerializeField]private Button RestartButton, PauseButton, LevelListButton, NextLevelButton;
    [SerializeField]private TextMeshProUGUI moveText;
    private void Awake() 
    {
        if(RestartButton != null)
        {
            RestartButton.onClick.AddListener(
            () =>
            {
                Restart();
            }
        );
        }
        if(PauseButton != null)
        {
            PauseButton.onClick.AddListener(
            () =>
            {
                Pause();
            }
        );
        }
        if(LevelListButton != null)
        {
            LevelListButton.onClick.AddListener(
            () =>
            {
                ShowLevelList();
            }
        );
        }
        if(NextLevelButton != null)
        {
            NextLevelButton.onClick.AddListener(
            () =>
            {
                NextLevel();
            }
        );
        }

    }
    private void Start()
    {
        playerSaveManager = PlayerSaveManager.Instance;
        ChangeMoveText();
        playerSaveManager.OnChangeMove += playerSaveManager_OnChangeMove;

        gameManager = PuzzleGameManager.Instance;
        gameManager.OnFinishGame += gameManager_OnFinishGame;
    }
    private void gameManager_OnFinishGame(object sender, EventArgs e)
    {
        ShowNextLevelButton();
        // Debug.Log("finish ui");
    }

    private void playerSaveManager_OnChangeMove(object sender, EventArgs e)
    {
        ChangeMoveText();
    }
    public void ChangeMoveText()
    {
        string moveNumber = playerSaveManager.GetPlayerMove().ToString();
        moveText.text = moveNumber;
    }
    public void Restart()
    {
        // playerSaveManager.PlayerRestart(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        if(gameManager.GetStateGame() == PuzzleGameManager.GameState.Start)
        {
            PuzzleGameManager.Instance.Pause();
            pauseUI.ShowPause();
        }
        
    }
    public void ShowLevelList()
    {
        Debug.Log("level List");
    }
    public void NextLevel()
    {
        Debug.Log("next level");
    }
    public void ShowNextLevelButton()
    {
        NextLevelButton.gameObject.SetActive(true);
    }
}
