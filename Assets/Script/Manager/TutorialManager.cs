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
    [SerializeField]private bool isMainMenu;
    [SerializeField]private bool canInteractTutorial;
    //di level 1
    // [SerializeField]private RectTransform
    [SerializeField]private GameObject tutorialAllTile, tutorialTileNamePlace, tutorialMoveTile;
    [SerializeField]private GameObject blocker_TutorialTileNamePlace, blocker_TutorialMoveTile;
    private int tutorialPosition = 0;
    private void Awake() 
    {
        Instance = this;
        
        if(playerSaveManager.IsFirstTimeEnterGame())
        {
            tutorialUI.gameObject.SetActive(true);
        }
        else if(playerSaveManager.IsFirstTimeTutorial())
        {
            tutorialAllTile.gameObject.SetActive(true);
        }
        
    }
    private void Update() 
    {
        if(gameManager.GetStartState() == PuzzleGameManager.StartState.Normal)
        {
            if(!canInteractTutorial)
            {
                ChangeCanInteractTutorial(true);
            }
        }
        else
        {
            ChangeCanInteractTutorial(false);
        }
    }
    public void PlayTutorial_OpenNamePlace()
    {
        tutorialPosition = 1;

        gameManager.TutorialMode();
        tutorialAllTile.gameObject.SetActive(false);
        tutorialTileNamePlace.gameObject.SetActive(true);
        blocker_TutorialTileNamePlace.gameObject.SetActive(true);
        gameManager.StartGame();
    }
    public void PlayTutorial_MovingTile()
    {
        if(!canInteractTutorial)return;
        tutorialPosition = 2;
        
        gameManager.TutorialMode();
        tutorialTileNamePlace.gameObject.SetActive(false);
        blocker_TutorialTileNamePlace.gameObject.SetActive(false);
        tutorialMoveTile.gameObject.SetActive(true);
        blocker_TutorialMoveTile.gameObject.SetActive(true);
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
