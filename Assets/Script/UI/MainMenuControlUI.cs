using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControlUI : MonoBehaviour
{
    public static MainMenuControlUI Instance{get;private set;}
    [SerializeField]private PlayerSaveManager playerSave;
    [SerializeField]private List<TilePuzzleManager> tilePuzzleManagersMainMenu;
    [SerializeField]private List<GameObject> wholeGameObjectPerPartList;//0 itu main menu, 1 itu level list, 2 itu settings, 3 itu credit
    [SerializeField]private Vector3 cameraPos, leftPos, rightPos;
    [SerializeField]private float duration;
    [SerializeField]private FadeInOutBlackScreen fade;
    private int canvasOnCamera = 0;
    [SerializeField]private LevelButtonUIManager levelButtonManager;

    private void Awake() 
    {
        Instance = this;
        if(playerSave.IsFirstTime())
        {
            playerSave.ChangeIsFirstTime();
            playerSave.LoadData();
        }
        if(levelButtonManager)levelButtonManager.SpawnLevelButton();
    }
    public void GoToMainMenu()
    {
        MovingTheCanvas(2);
    }
    public void GoToLevelListUI()
    {
        MovingTheCanvas(1);
    }
    public void GoToSettings()
    {
        MovingTheCanvas(2);
    }
    public void GoToCredits()
    {
        MovingTheCanvas(3);
    }
    public void Quit()
    {
        PlayerSaveManager.Instance.SaveData();
        PlayerPrefs.SetString("PlayerPress", "QuitGame");
        LeanTween.moveLocal(wholeGameObjectPerPartList[canvasOnCamera], leftPos, duration);
        fade.FadeInBlackScreenOutsideInGame(0);
    }
    public void MovingTheCanvas(int wantToGoCanvas)
    {
        wholeGameObjectPerPartList[wantToGoCanvas].transform.localPosition = new Vector3(rightPos.x, wholeGameObjectPerPartList[wantToGoCanvas].transform.localPosition.y, wholeGameObjectPerPartList[wantToGoCanvas].transform.localPosition.z);
        LeanTween.moveLocalX(wholeGameObjectPerPartList[canvasOnCamera], leftPos.x, duration);

        LeanTween.moveLocalX(wholeGameObjectPerPartList[wantToGoCanvas], cameraPos.x, duration).setOnComplete(
            ()=>
            {
                PuzzleGameManager.Instance.StartGame();
                canvasOnCamera = wantToGoCanvas;
            }
            
        );
    }
}
