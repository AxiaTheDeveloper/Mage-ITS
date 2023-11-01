using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance{get; private set;}
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private FadeInOutBlackScreen fade;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private PauseUI pauseUI;
    [SerializeField]private Button RestartButton, PauseButton, PrevLevelButton, NextLevelButton, QuitButton;
    [SerializeField]private TextMeshProUGUI moveText, nameTileText;
    
    private void Awake() 
    {
        Instance = this;
        nameTileText.text = "";
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
        if(PrevLevelButton != null)
        {
            PrevLevelButton.onClick.AddListener(
                () =>
                {
                    
                    PrevLevel();
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
        if(QuitButton != null)
        {
            QuitButton.onClick.AddListener(
                () =>
                {
                    Quit();
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
        // gameManager.OnFinishGame += gameManager_OnFinishGame;
    }
    // private void gameManager_OnFinishGame(object sender, EventArgs e)
    // {
    //     ShowNextLevelButton();
    //     // Debug.Log("finish ui");
    // }

    private void playerSaveManager_OnChangeMove(object sender, EventArgs e)
    {
        ChangeMoveText();
    }
    public void ChangeMoveText()
    {
        string moveNumber = playerSaveManager.GetPlayerMove().ToString();
        moveText.text = moveNumber;
    }
    public void ChangeNameText(string name)
    {
        nameTileText.text = name;
    }
    public void Restart()
    {
        // PlayerSaveManager.Instance.PlayerRestart(true);
        PlayerPrefs.SetString("PlayerPress", "Restart");
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
    public void PrevLevel()
    {
        if(gameManager.PuzzleLevel()==1)Debug.Log("Ga ada level sblmnya");//mainin suara kek teken tombol kosong?
        else
        {
            PlayerPrefs.SetString("PlayerPress", "PrevLevel");
            fade.FadeInBlackScreen();
        }
        
    }
    public void NextLevel()
    {
        
        if(gameManager.PuzzleLevel()== playerSaveManager.GetTotalLevel() || !playerSaveManager.IsLevelDone())
        {
            Debug.Log("Ga ada level setelahnya");
            // NextLevelButton.spriteState = normal
            // EventSystem.current.SetSelectedGameObject(null);
        }
        //mainin suara kek teken tombol kosong?
        else
        {
            PlayerPrefs.SetString("PlayerPress", "NextLevel");
            fade.FadeInBlackScreen();
        }
    }
    // public void ShowNextLevelButton()
    // {
    //     NextLevelButton.gameObject.SetActive(true);
    // }
    public void Quit()
    {
        PlayerPrefs.SetString("PlayerPress", "Quit");
        fade.FadeInBlackScreen();
    }
}
