using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListInGameButton : MonoBehaviour
{
    [SerializeField]private Button button;
    private int levelNumber;
    private PlayerSaveScriptableObject.LevelIdentity thisLevelIdentity;
    [SerializeField]private FadeInOutBlackScreen fade;
    private void Awake() 
    {
        button = GetComponent<Button>();
        if(button)button.onClick.AddListener(ToTheLevel);
    }
    public void ToTheLevel()
    {
        int thisStageLevelNumber = PuzzleGameManager.Instance.PuzzleLevel();
        if(thisLevelIdentity.levelUnlocked && levelNumber != thisStageLevelNumber)
        {
            if(levelNumber > thisStageLevelNumber)PlayerPrefs.SetString("PlayerPress", "JumpNext");
            else PlayerPrefs.SetString("PlayerPress", "JumpPrev");
            
            fade.FadeInBlackScreenJumpLevelList(levelNumber);
        }
        else
        {
            //bunyi kek teken tombol kosong
            Debug.Log("tidak bisa");
        }
        
    }
    public void GetLevelIdentity(PlayerSaveScriptableObject.LevelIdentity level, int lvlnmbr)
    {
        thisLevelIdentity = level;
        levelNumber = lvlnmbr;
    }
}
