using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListInGameController : MonoBehaviour
{
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private PlayerSaveManager playerSave;
    
    [SerializeField]private PlayerSaveScriptableObject playerSaveSO;
    [SerializeField]private Sprite[] spriteLevelList;//0 normal, 1 yg done
    [SerializeField]private Image[] levelList;
    [SerializeField]private LevelListInGameButton[] levelListButTheLevelListInGameButton;
    [SerializeField]private GameObject levelSelect;
    [SerializeField]private Color[] colorList;//0 normal, 1 yg locked
    
    private void Awake() 
    {
        if(playerSave)playerSaveSO = playerSave.GetPlayerSaveSO();
        for(int i=0;i<playerSaveSO.levelIdentities.Length;i++)
        {
            if(playerSaveSO.levelIdentities[i].levelUnlocked)
            {
                levelList[i].color = colorList[0];
                if(playerSaveSO.levelIdentities[i].levelDone)levelList[i].sprite = spriteLevelList[1];
            }
            else
            {
                levelList[i].color = colorList[1];
            }
            levelListButTheLevelListInGameButton[i].GetLevelIdentity(playerSaveSO.levelIdentities[i], i+1);
        }
        // Debug.Log(levelList[gameManager.PuzzleLevel() - 1].GetComponent<RectTransform>().anchoredPosition);
        levelSelect.transform.SetParent(levelList[gameManager.PuzzleLevel() - 1].gameObject.transform);
        
    }
    private void Start() 
    {
        gameManager.OnFinishGame += gameManager_OnFinishGame;
    }

    private void gameManager_OnFinishGame(object sender, EventArgs e)
    {
        ChangeVisualWhenWin();
    }

    

    public void ChangeVisualWhenWin()
    {
        // Debug.Log("Change");
        levelList[gameManager.PuzzleLevel() - 1].sprite = spriteLevelList[1];
        // Debug.Log(levelList[gameManager.PuzzleLevel() - 1].sprite);
        if(gameManager.PuzzleLevel() != playerSaveSO.levelIdentities.Length)levelList[gameManager.PuzzleLevel()].color = colorList[0];
        // Debug.Log(levelList[gameManager.PuzzleLevel()].gameObject);
    }
}
