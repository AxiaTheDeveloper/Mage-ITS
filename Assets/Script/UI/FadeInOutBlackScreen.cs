using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutBlackScreen : MonoBehaviour
{
    public static FadeInOutBlackScreen Instance{get;private set;}
    [SerializeField]private RectTransform blackScreen;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private GameObject tilePuzzle;
    [SerializeField]private Vector3 tilePuzzleStartPos, tilePuzzlePlayPos, tilePuzzleFinishPos;
    private bool isFirstUpdate = true, hasFadeOut;
    [SerializeField]private bool isLevelScene = true;
    private string playerPress;
    //buat tutorial
    [SerializeField]private TutorialManager tutorialManager;
    [SerializeField]private TilePuzzleManager tilePuzzleManager;
    [SerializeField]private bool isMainMenu;
    private void Awake() 
    {
        Instance = this;
        if(isLevelScene)
        {
            if(!PlayerPrefs.HasKey("PlayerPress"))
            {
                tilePuzzle.transform.position = tilePuzzleStartPos;
            }
            else
            {
                playerPress = PlayerPrefs.GetString("PlayerPress");
                if(playerPress == "Main Menu" || playerPress == "NextLevel")
                {
                    tilePuzzle.transform.position = tilePuzzleStartPos;
                }
                else if(playerPress == "PrevLevel")
                {
                    tilePuzzle.transform.position = tilePuzzleFinishPos;
                }
                else if(playerPress == "Restart")
                {
                    tilePuzzle.transform.position = tilePuzzlePlayPos;
                }
                
            }
            if(!playerSaveManager.IsPlayerRestartLevel())blackScreen.gameObject.SetActive(true);
        }
        else
        {
            blackScreen.gameObject.SetActive(true);
        }
        
        
    }
    private void Update()
    {
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
        }
        else
        {
            if(!hasFadeOut)
            {
                hasFadeOut = true;
                FadeOutBlackScreen();
            }
        }
    }
    public void FadeOutBlackScreen()
    {
        if(isLevelScene)
        {
            if(!playerSaveManager.IsPlayerRestartLevel())
            {
                MoveTilePuzzle(tilePuzzlePlayPos, 0.5f);
                blackScreen.LeanAlpha(0, 0.8f).setOnComplete(
                    ()=>FadeOutEnded()
                );
            }
            else
            {
                // PuzzleGameManager.Instance.StartGame();
                // playerSaveManager.PlayerRestart(false);
                
                //di atas aja yg dinyalain kalo gamau ada fade in
                blackScreen.LeanAlpha(0, 0.8f).setOnComplete(
                    ()=>FadeOutEnded()
                );
            }
        }
        else
        {
            blackScreen.LeanAlpha(0, 0.8f).setOnComplete(
                ()=>FadeOutEnded()
            );
        }
        
        
    }
    public void FadeOutEnded()
    {
        if(playerSaveManager.IsFirstTimeEnterGame())
        {
            tilePuzzleManager.LockAllMoveAble();
            tilePuzzleManager.UnlockAMoveAble(0);
        }
        else if(!playerSaveManager.IsFinishTutorial() && !isMainMenu)
        {
            tilePuzzleManager.LockAllMoveAble();
            tilePuzzleManager.LockAllRotateAble();
        }
        PuzzleGameManager.Instance.StartGame();
        
        blackScreen.gameObject.SetActive(false);
    }
    public void FadeInBlackScreenOutsideInGame(int level)
    {
        PuzzleGameManager.Instance.WaitToStart();
        blackScreen.gameObject.SetActive(true);
        blackScreen.LeanAlpha(1, 0.8f).setOnComplete(
            ()=>FadeInEndedOutsideInGame(level)
        );
    }
    public void FadeInEndedOutsideInGame(int level)
    {
        
        playerPress = PlayerPrefs.GetString("PlayerPress");
        if(playerPress == "QuitGame")
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        else if(playerPress == "Main Menu")
        {
            Debug.Log("aa");
            string sceneName = "Level " + level;
            SceneManager.LoadScene(sceneName);
        }
        
    }
    public void FadeInBlackScreen()
    {
        PuzzleGameManager.Instance.WaitToStart();
        playerPress = PlayerPrefs.GetString("PlayerPress");
        if(playerPress == "NextLevel")
        {
            MoveTilePuzzle(tilePuzzleFinishPos, 0.5f);
        }
        else if(playerPress == "PrevLevel" || playerPress == "Quit")
        {
            MoveTilePuzzle(tilePuzzleStartPos, 0.5f);
        }
        
        //ini jg kalo quit gamau fade in tinggal ksh kondisi
        blackScreen.gameObject.SetActive(true);
        blackScreen.LeanAlpha(1, 0.8f).setOnComplete(
            ()=>FadeInEnded()
        );
    }
    public void MoveTilePuzzle(Vector3 position, float duration)
    {
        LeanTween.move(tilePuzzle, position, duration).setEaseSpring();
    }
    public void FadeInEnded()
    {
        string sceneName = "Level ";
        if(playerPress == "NextLevel")
        {
            sceneName += (PuzzleGameManager.Instance.PuzzleLevel() + 1);
        }
        else if(playerPress == "PrevLevel")
        {
            sceneName += (PuzzleGameManager.Instance.PuzzleLevel() - 1);
        }
        else if(playerPress == "Quit")
        {
            BGMManager.Instance.DestroyInstance();
            sceneName = "Main Menu";
        }
        SceneManager.LoadScene(sceneName);
    }
    public void FadeInBlackScreenJumpLevelList(int levelNumber)
    {
        PuzzleGameManager.Instance.WaitToStart();
        playerPress = PlayerPrefs.GetString("PlayerPress");
        if(playerPress == "JumpNext")
        {
            PlayerPrefs.SetString("PlayerPress", "NextLevel");
            MoveTilePuzzle(tilePuzzleFinishPos, 0.5f);
        }
        else if(playerPress == "JumpPrev")
        {
            PlayerPrefs.SetString("PlayerPress", "PrevLevel");
            MoveTilePuzzle(tilePuzzleStartPos, 0.5f);
        }
        
        //ini jg kalo quit gamau fade in tinggal ksh kondisi
        blackScreen.gameObject.SetActive(true);
        blackScreen.LeanAlpha(1, 0.8f).setOnComplete(
            ()=>FadeInEndedJumpLevelList(levelNumber)
        );
    }
    public void FadeInEndedJumpLevelList(int levelNumber)
    {
        string sceneName = "Level " + levelNumber;
        SceneManager.LoadScene(sceneName);
    }
}
