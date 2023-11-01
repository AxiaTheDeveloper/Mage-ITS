using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject BG;
    [SerializeField]private GameObject pauseUI;
    [SerializeField]private Vector3 startVector, toVector;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private Button UnPauseButton;
    [SerializeField]private Slider BGM, SFX;
    [SerializeField]private TextMeshProUGUI BGMVolText, SFXVolText;
    [SerializeField]private SFXManager sFXManager;
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
        sFXManager = SFXManager.Instance;
        LeanTween.alpha(BG, 0, 0);
        ChangeBGMVolText();
        ChangeSFXVolText();
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
        sFXManager.PlayButtonCanBeUsed();
        gameManager.Pause();
        LeanTween.alpha(BG, 0, 0.2f).setOnComplete(
            ()=>BG.SetActive(false)
        );
        LeanTween.moveLocal(pauseUI, startVector, 0.2f);
    }
    public Slider GetBGMSlider()
    {
        return BGM;
    }
    public Slider GetSFXSlider()
    {
        return SFX;
    }
    public void ChangeBGMVolText()
    {
        if(BGM.value > 0 && BGM.value < 1)sFXManager.PlayButtonCanBeUsed();
        float value = Mathf.Round(BGM.value * 100);
        // Debug.Log(value);
        BGMVolText.text = value.ToString();
    }
    public void ChangeSFXVolText()
    {
        // Debug.Log(SFX.value);
        if(SFX.value > 0 && SFX.value < 1)sFXManager.PlayButtonCanBeUsed();
        float value = Mathf.Round(SFX.value * 100);
        // Debug.Log(value);
        SFXVolText.text = value.ToString();
    }
}