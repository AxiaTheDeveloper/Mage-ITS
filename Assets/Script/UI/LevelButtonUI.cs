using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField]private FadeInOutBlackScreen fade;
    [SerializeField]private int levelNumber;
    [SerializeField]private Button levelButton;
    [SerializeField]private PlayerSaveScriptableObject.LevelIdentity thisLevelIdentity;
    [SerializeField]private SFXManager sFXManager;
    private void Awake() 
    {
        
        if(!levelButton)levelButton = GetComponent<Button>();
        levelButton.onClick.AddListener(
            ()=>
            {
                GoToLevel();
            }
        );
    }
    private void Start() 
    {
        sFXManager = SFXManager.Instance;
        fade = FadeInOutBlackScreen.Instance;
    }
    public void GoToLevel()
    {
        if(thisLevelIdentity.levelUnlocked)
        {
            if(sFXManager)sFXManager.PlayButtonCanBeUsed();
            PlayerPrefs.SetString("PlayerPress", "Main Menu");
            fade.FadeInBlackScreenOutsideInGame(levelNumber);
        }
        else
        {
            if(sFXManager)sFXManager.PlayButtonCantBeUsed();
            //mainin suara kek teken tombol kosong
            Debug.Log("not yet unlocked");
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
        }
        
    }
    public void ChangeVisual()
    {
        //based on byk skor, lock unlock, done itu gaperlu
    }
    public void GetLevelIdentity(PlayerSaveScriptableObject.LevelIdentity lvl, int levelNumbers)
    {
        thisLevelIdentity = lvl;
        levelNumber = levelNumbers;
        ChangeVisual();
    }

}
