using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]private TilePuzzleManager tilePuzzleManager;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private RectTransform tutorialUI;
    [SerializeField]private bool isMainMenu;
    private void Awake() 
    {
        
        if(playerSaveManager.IsFirstTimeEnterGame())
        {
            tutorialUI.gameObject.SetActive(true);
        }
        
    }
    public void PlayTutorial()
    {
        if(isMainMenu)
        {
            //gameobject setactive
            //nyalakan tutorial itu

        }
        else
        {

        }
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
}
