using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField]private FadeInOutBlackScreen fade;
    [SerializeField]private int levelNumber;
    [SerializeField]private Button levelButton;
    [SerializeField]private PlayerSaveScriptableObject.LevelIdentity thisLevelIdentity;
    [SerializeField]private SFXManager sFXManager;
    [SerializeField]private TextMeshProUGUI levelText;
    [SerializeField]private GameObject[] stars;
    [SerializeField]private Color[] colorOnOff; //0 - On, 1 - off
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
        levelText.text = levelNumber.ToString();
        if(thisLevelIdentity.levelUnlocked)
        {
            GetComponent<Image>().color = colorOnOff[0];
            levelText.color = colorOnOff[0];
            if(thisLevelIdentity.levelDone)
            {
                for(int i=0;i<thisLevelIdentity.levelScore;i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
        else
        {
            GetComponent<Image>().color = colorOnOff[1];
            levelText.color = colorOnOff[1];
        }
    }
    public void GetLevelIdentity(PlayerSaveScriptableObject.LevelIdentity lvl, int levelNumbers)
    {
        thisLevelIdentity = lvl;
        levelNumber = levelNumbers;
        ChangeVisual();
    }

}
