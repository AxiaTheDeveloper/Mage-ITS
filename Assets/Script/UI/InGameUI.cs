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
    [SerializeField]private SFXManager sFXManager;
    
    
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
        else
            Debug.LogWarning("Missing a Button, If available please create one : " + RestartButton);
        if(PauseButton != null)
        {
            PauseButton.onClick.AddListener(
                () =>
                {
                    Pause();
                }
            );
        }
        else
            Debug.LogWarning("Missing a Button, If available please create one : " + PauseButton);
        if(PrevLevelButton != null)
        {
            PrevLevelButton.onClick.AddListener(
                () =>
                {
                    
                    PrevLevel();
                }
            );    
        }
        else
            Debug.LogWarning("Missing a Button, If available please create one : " + PrevLevelButton);
        if(NextLevelButton != null)
        {
            NextLevelButton.onClick.AddListener(
                () =>
                {
                    
                    NextLevel();
                }
            );
        }
        else
            Debug.LogWarning("Missing a Button, If available please create one : " + NextLevelButton);
        if (QuitButton != null)
        {
            QuitButton.onClick.AddListener(
                () =>
                {
                    Quit();
                }
            );
        }
        else
            Debug.LogWarning("Missing a Button, If available please create one : " + NextLevelButton);
    }
    private void Start()
    {
        playerSaveManager = PlayerSaveManager.Instance;
        ChangeMoveText();
        playerSaveManager.OnChangeMove += playerSaveManager_OnChangeMove;

        gameManager = PuzzleGameManager.Instance;
        sFXManager = SFXManager.Instance;
        gameManager.OnFinishGame += gameManager_OnFinishGame;
    }
    private void gameManager_OnFinishGame(object sender, EventArgs e)
    {
        StartCoroutine(CountDownNextLevel());
    }
    private IEnumerator CountDownNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        NextLevel();
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
    public void ChangeNameText(string name)
    {
        nameTileText.text = name;
    }
    public void Restart()
    {
        // PlayerSaveManager.Instance.PlayerRestart(true);
        if(sFXManager)sFXManager.PlayButtonCanBeUsed();
        PlayerPrefs.SetString("PlayerPress", "Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        if(gameManager.GetStateGame() == PuzzleGameManager.GameState.Start || gameManager.GetStateGame() == PuzzleGameManager.GameState.Finish)
        {
            if(sFXManager)sFXManager.PlayButtonCanBeUsed();
            PuzzleGameManager.Instance.Pause();
            pauseUI.ShowPause();
        }
    }
    public void PrevLevel()
    {
        if(gameManager.PuzzleLevel()==1)
        {
            if(sFXManager)sFXManager.PlayButtonCantBeUsed();
            Debug.Log("Ga ada level sblmnya");
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
        }
        
        else
        {
            if(sFXManager)sFXManager.PlayButtonCanBeUsed();
            PlayerPrefs.SetString("PlayerPress", "PrevLevel");
            fade.FadeInBlackScreen();
        }
        
    }
    public void NextLevel()
    {
        
        if(gameManager.PuzzleLevel()== playerSaveManager.GetTotalLevel() || !playerSaveManager.IsLevelDone())
        {
            if(sFXManager)sFXManager.PlayButtonCantBeUsed();
            Debug.Log("Ga ada level setelahnya");
            // NextLevelButton.spriteState = normal
            // EventSystem.current.SetSelectedGameObject(null);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
        }
        //mainin suara kek teken tombol kosong?
        else
        {
            if(sFXManager)sFXManager.PlayButtonCanBeUsed();
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
        if(sFXManager)sFXManager.PlayButtonCanBeUsed();
        playerSaveManager.SaveData();
        PlayerPrefs.SetString("PlayerPress", "Quit");
        fade.FadeInBlackScreen();
    }
}
