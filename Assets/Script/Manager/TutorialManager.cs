using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance{get;private set;}
    [SerializeField]private TilePuzzleManager tilePuzzleManager;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private RectTransform tutorialUI;
    [SerializeField]private GameObject tutorialMainMenu;
    [SerializeField]private bool isMainMenu;
    [SerializeField]private bool canInteractTutorial;
    //di level 1
    // [SerializeField]private RectTransform
    [SerializeField]private GameObject tutorialAllTile, tutorialTileNamePlace, tutorialMoveTile, tutorialShowMove, tutorialRotateTile, tutorialShowScore, tutorialShowToMoveOrRotate, tutorialHighlightScoreDestroyed, tutorialShowRestartButton, tutorialShowLevelList, tutorialHaveFun;
    [SerializeField]private GameObject blockOption;
    [SerializeField]private PlayerInput playerInput;
    private MoveTile muv;
    private int tutorialPosition = 0;
    private void Awake() 
    {
        Instance = this;
        
        
        
    }
    private void Start() 
    {
        if(playerSaveManager.IsFirstTimeEnterGame())
        {
            tutorialUI.gameObject.SetActive(true);
        }
        else if(!playerSaveManager.IsFinishTutorial() && !isMainMenu)
        {
            tutorialAllTile.gameObject.SetActive(true);
        }
        if(isMainMenu && playerSaveManager.IsFinishTutorial())
        {
            tutorialMainMenu.SetActive(true);
        }
        if(playerInput)playerInput.OnRotate+=playerInput_OnRotate;
    }

    private void playerInput_OnRotate(object sender, EventArgs e)
    {
        if(tutorialPosition == 4)
        {
            PlayTutorial_ShowScore();
        }
    }

    private void Update() 
    {
        if(!isMainMenu)
        {
            if(gameManager.GetStartState() == PuzzleGameManager.StartState.Normal)
            {
                if(!canInteractTutorial)
                {
                    ChangeCanInteractTutorial(true);
                }
                if(playerSaveManager.GetPlayerMove() == 0 && tutorialPosition == 2 && !playerInput.IsThereChosenTile())
                {
                    muv.ChangeLeftMax(muv.transform.localPosition.x);
                    muv.ChangeDownMax(muv.transform.localPosition.y);
                }
                if(playerSaveManager.GetPlayerMove() == 1 && tutorialPosition == 2)
                {
                    PlayTutorial_ShowTotalMoves();
                }
                
                // if(gameManager.IsTIleRotating() && tutorialPosition == 4)
                // {
                //     Debug.Log("Oi");
                //     PlayTutorial_ShowScore();
                // }
                if(playerSaveManager.GetPlayerMove() == 3 && tutorialPosition == 6)
                {
                    PlayTutorial_ShowHighlightScoreDestroyed();
                }
            }
            else
            {
                ChangeCanInteractTutorial(false);
            }
        }
        
    }
    public void PlayTutorial_OpenNamePlace()
    {
        tutorialPosition = 1;

        gameManager.TutorialMode();
        tutorialAllTile.gameObject.SetActive(false);

        tutorialTileNamePlace.gameObject.SetActive(true);
        gameManager.StartGame();
    }
    public void PlayTutorial_MovingTile()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 2;
        
        gameManager.TutorialMode();
        tutorialTileNamePlace.gameObject.SetActive(false);

        tutorialMoveTile.gameObject.SetActive(true);
        tilePuzzleManager.UnlockAMoveAble(1);
        muv = tilePuzzleManager.GetTheLockMoveTile(1);
        muv.ChangeLeftMax(muv.transform.localPosition.x);
        muv.ChangeDownMax(muv.transform.localPosition.y);
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowTotalMoves()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 3;
        
        gameManager.TutorialMode();
        tutorialMoveTile.gameObject.SetActive(false);
        tilePuzzleManager.LockAMoveAble(1);

        tutorialShowMove.gameObject.SetActive(true);


        gameManager.StartGame();
    }
    public void PlayTutorial_RotateTile()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 4;
        
        gameManager.TutorialMode();
        tutorialShowMove.gameObject.SetActive(false);
        

        tutorialRotateTile.gameObject.SetActive(true);
        tilePuzzleManager.UnlockARotateAble(1);


        gameManager.StartGame();
    }
    public void PlayTutorial_ShowScore()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 5;
        
        gameManager.TutorialMode();
        tutorialRotateTile.gameObject.SetActive(false);
        tilePuzzleManager.LockARotateAble(1);
        

        tutorialShowScore.gameObject.SetActive(true);
        
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowToMoveOrRotate()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 6;
        
        gameManager.TutorialMode();
        tutorialShowScore.gameObject.SetActive(false);
        

        tutorialShowToMoveOrRotate.gameObject.SetActive(true);
        tilePuzzleManager.UnlockAllMoveAble();
        tilePuzzleManager.UnlockAllRotateAble();
        
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowHighlightScoreDestroyed()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 7;
        
        gameManager.TutorialMode();
        tutorialShowToMoveOrRotate.gameObject.SetActive(false);
        tilePuzzleManager.LockAllMoveAble();
        tilePuzzleManager.LockAllRotateAble();

        tutorialHighlightScoreDestroyed.gameObject.SetActive(true);
        
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowTutorialRestart()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 8;
        
        gameManager.TutorialMode();
        tutorialHighlightScoreDestroyed.gameObject.SetActive(false);

        tutorialShowRestartButton.gameObject.SetActive(true);
        blockOption.SetActive(true);
        
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowtutorialShowLevelList()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 9;
        
        gameManager.TutorialMode();
        tutorialShowRestartButton.gameObject.SetActive(false);
        blockOption.SetActive(false);

        tutorialShowLevelList.gameObject.SetActive(true);
        
        gameManager.StartGame();
    }
    public void PlayTutorial_ShowtutorialHaveFun()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 10;
        
        gameManager.TutorialMode();
        tutorialShowLevelList.gameObject.SetActive(false);

        tutorialHaveFun.gameObject.SetActive(true);
        
        
        gameManager.StartGame();
    }
    public void EndTutorial()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 11;
        
        gameManager.TutorialMode();
        tutorialHaveFun.gameObject.SetActive(false);
        playerSaveManager.ChangeIsFinishTutorial();
        tilePuzzleManager.UnlockAllMoveAble();
        tilePuzzleManager.UnlockAllRotateAble();

        
        
        gameManager.StartGame();
        
    }
    public bool CanInteractTutorial()
    {
        return canInteractTutorial;
    }
    public void ChangeCanInteractTutorial(bool change)
    {
        if(change)
        {
            StartCoroutine(CanInteractTutorialCounter());
        }
        else
        {
            canInteractTutorial = change;
        }
    }
    private IEnumerator CanInteractTutorialCounter()
    {
        yield return new WaitForSeconds(0.5f);
        canInteractTutorial = true;

    }
    public void FadeTutorialMainMenu()
    {
        LeanTween.alpha(tutorialUI,0, 0.8f).setOnComplete(
            ()=>FadeTutorialMainMenuEnded()
        );
    }
    public void FadeTutorialMainMenuEnded()
    {
        tutorialUI.gameObject.SetActive(false);
        playerSaveManager.ChangeIsFirstTimeEnterGame();
        tilePuzzleManager.UnlockAllMoveAble();
        PuzzleGameManager.Instance.StartGame();
    }
    public int GetTutorialPosition()
    {
        return tutorialPosition;
    }
}
