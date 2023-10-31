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
    private void Awake() 
    {
        buttonBack.onClick.AddListener(
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
    public void BackToMainMenu()
    {
        Debug.Log("Kembali ke main menu");
    }



}
