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
    private bool isFirsTimeSFX = true, isFirsTimeBGM= true;
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
        // Debug.Log(BGM.value + "dan" + SFX.value);
    }

    //Set Float so the lean Tween understood wtf we want to change, a weird flex from leanTween, but ok?

    void UpdateAlpha(float alpha) 
    {
        Color tempColor = BG.GetComponent<Image>().color;
        tempColor.a = alpha;
        BG.GetComponent<Image>().color = tempColor;
    }

    public void ShowPause()
    {
        BG.SetActive(true);
        LeanTween.value(BG, UpdateAlpha, 0f, 0.5f, 1f);
        LeanTween.moveLocal(pauseUI, toVector, 0.5f).setEaseSpring();
    }
    public void HidePause()
    {
        sFXManager.PlayButtonCanBeUsed();
        gameManager.Pause();
        LeanTween.value(BG, UpdateAlpha, 0.5f, 0f, 1f).setOnComplete(
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
        if(BGM.value > 0 && BGM.value < 1 && !isFirsTimeBGM)sFXManager.PlayButtonCanBeUsed();
        if(isFirsTimeBGM)isFirsTimeBGM = false;
        float value = Mathf.Round(BGM.value * 100);
        // Debug.Log(value);
        BGMVolText.text = value.ToString();
    }
    public void ChangeSFXVolText()
    {
        // Debug.Log(SFX.value);
        if(SFX.value > 0 && SFX.value < 1 && !isFirsTimeSFX)sFXManager.PlayButtonCanBeUsed();
        if(isFirsTimeSFX)isFirsTimeSFX = false;
        float value = Mathf.Round(SFX.value * 100);
        // Debug.Log(value);
        SFXVolText.text = value.ToString();
    }
}