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
    [SerializeField]private SFXManager sFXManager;
    private void Awake() 
    {
        button = GetComponent<Button>();
        if(button)button.onClick.AddListener(ToTheLevel);
    }
    private void Start() 
    {
        sFXManager = SFXManager.Instance;
    }
    public void ToTheLevel()
    {
        int thisStageLevelNumber = PuzzleGameManager.Instance.PuzzleLevel();
        if(thisLevelIdentity.levelUnlocked && levelNumber != thisStageLevelNumber)
        {
            if(sFXManager)sFXManager.PlayButtonCanBeUsed();
            if(levelNumber > thisStageLevelNumber)PlayerPrefs.SetString("PlayerPress", "JumpNext");
            else PlayerPrefs.SetString("PlayerPress", "JumpPrev");
            
            fade.FadeInBlackScreenJumpLevelList(levelNumber);
        }
        else
        {
            if(sFXManager)sFXManager.PlayButtonCantBeUsed();
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
