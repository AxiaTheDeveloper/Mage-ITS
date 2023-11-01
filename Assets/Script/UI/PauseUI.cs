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
        ChangeBGMVolText();
        ChangeSFXVolText();
    }

    //Set Float so the lean Tween understood wtf we want to change, a weird flex from leanTween, but ok?

    void UpdateAlpha(float alpha) {
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
        float value = Mathf.Round(BGM.value * 100);
        // Debug.Log(value);
        BGMVolText.text = value.ToString();
    }
    public void ChangeSFXVolText()
    {
        // Debug.Log(SFX.value);
        float value = Mathf.Round(SFX.value * 100);
        // Debug.Log(value);
        SFXVolText.text = value.ToString();
    }
}