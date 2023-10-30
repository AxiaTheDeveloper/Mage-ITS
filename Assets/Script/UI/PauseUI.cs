using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject BG;
    [SerializeField]private GameObject pauseUI;
    [SerializeField]private Vector3 startVector, toVector;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private Button UnPauseButton;
    private void Awake() 
    {
        UnPauseButton.onClick.AddListener(
            ()=>
            {
                if(gameManager.GetStateGame() == PuzzleGameManager.GameState.Pause)HidePause();
            }
        );    
    }
    private void Start()
    {
        LeanTween.alpha(BG, 0, 0);
    }

    // Update is called once per frame
    public void ShowPause()
    {
        BG.SetActive(true);
        LeanTween.alpha(BG, 0.5f, 0.2f);
        LeanTween.moveLocal(pauseUI, toVector, 0.5f).setEaseSpring();
    }
    public void HidePause()
    {
        gameManager.Pause();
        LeanTween.alpha(BG, 0, 0.2f).setOnComplete(
            ()=>BG.SetActive(false)
        );
        LeanTween.moveLocal(pauseUI, startVector, 0.2f);
    }
}
