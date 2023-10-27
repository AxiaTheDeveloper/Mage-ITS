using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutBlackScreen : MonoBehaviour
{
    [SerializeField]private RectTransform blackScreen;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    private bool isFirstUpdate = true, hasFadeOut;
    private void Awake() 
    {
        if(!playerSaveManager.IsPlayerRestartLevel())blackScreen.gameObject.SetActive(true);
        
    }
    private void Update()
    {
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
        }
        else
        {
            if(!hasFadeOut)
            {
                hasFadeOut = true;
                FadeOutBlackScreen();
            }
        }
    }
    public void FadeOutBlackScreen()
    {
        if(!playerSaveManager.IsPlayerRestartLevel())
        {
            blackScreen.LeanAlpha(0, 0.8f).setOnComplete(
                ()=>FadeOutEnded()
            );
        }
        else
        {
            PuzzleGameManager.Instance.StartGame();
            playerSaveManager.PlayerRestart(false);
        }
        
    }
    public void FadeOutEnded()
    {
        PuzzleGameManager.Instance.StartGame();
        blackScreen.gameObject.SetActive(false);
    }
}
