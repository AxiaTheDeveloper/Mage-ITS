using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUIManager : MonoBehaviour
{
    [SerializeField]private Transform prefabLevelButtonUI;
    [SerializeField]private Transform parent;
    [SerializeField]private PlayerSaveScriptableObject playerSaveSO;
    [SerializeField]private Button buttonBack, settingButton;
    private int totalLevel;
    [SerializeField]private SFXManager sFXManager;
    private void Awake() 
    {
        
        if(buttonBack)buttonBack.onClick.AddListener(
            ()=>
            {
                BackToMainMenu();
            }
        );
        totalLevel = playerSaveSO.levelIdentities.Length;
        for(int i=0;i<totalLevel;i++)
        {
            Transform instatiateButton = Instantiate(prefabLevelButtonUI.transform, parent);
            LevelButtonUI levelButton = instatiateButton.GetComponent<LevelButtonUI>();
            levelButton.GetLevelIdentity(playerSaveSO.levelIdentities[i],i+1);
        }
    } 
    private void Start() 
    {
        sFXManager = SFXManager.Instance;
    }
    public void BackToMainMenu()
    {
        if(sFXManager)sFXManager.PlayButtonCanBeUsed();
        Debug.Log("Kembali ke main menu");
    }



}
