using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControlUI : MonoBehaviour
{
    public static MainMenuControlUI Instance{get;private set;}
    [SerializeField]private List<TilePuzzleManager> tilePuzzleManagersMainMenu;
    [SerializeField]private List<GameObject> wholeGameObjectPerPartList;//0 itu main menu, 1 itu level list, 2 itu settings, 3 itu credit
    [SerializeField]private List<GameObject> wholeGameObjectPerPartList_Canvas;
    [SerializeField]private Vector3 cameraPos, leftPos, rightPos;
    [SerializeField]private float duration;

    private void Awake() 
    {
        Instance = this;
    }
    public void GoToLevelListUI()
    {
        LeanTween.moveLocal(wholeGameObjectPerPartList[0], leftPos, duration);
        LeanTween.moveLocal(wholeGameObjectPerPartList_Canvas[0], leftPos, duration);

        LeanTween.moveLocal(wholeGameObjectPerPartList[1], cameraPos, duration);
        LeanTween.moveLocal(wholeGameObjectPerPartList_Canvas[1], cameraPos, duration);
    }
}
