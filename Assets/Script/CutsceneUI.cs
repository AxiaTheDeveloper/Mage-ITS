using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField]private CanvasGroup[] textDialogues;
    [SerializeField]private RectTransform chara, BG;
    [SerializeField]private GameObject skipButton;
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private FadeInOutBlackScreen fade;
    [SerializeField]private float showDuration, closeDuration, waitDuration;
    [SerializeField]private int showChara;
    private int cutscenePosition = 0;
    private bool couroStart, isFirstTimeStart = true;
    private void Start() 
    {
        chara.LeanAlpha(0, 0f);
        foreach(CanvasGroup textDialogue in textDialogues)
        {
            textDialogue.LeanAlpha(0, 0f);
        }
        
    }
    public void StartCutscene()
    {
        if(isFirstTimeStart)
        {
            isFirstTimeStart = false;
            StartCoroutine(SkipButtonShow());
        }
        textDialogues[cutscenePosition].gameObject.SetActive(true);
        textDialogues[cutscenePosition].LeanAlpha(1, showDuration).setOnComplete(
            ()=>
            {
                if(gameObject.activeSelf)StartCoroutine(CountDownCutscene());
                
            }
            
        );
        if(cutscenePosition == showChara)
        {
            chara.gameObject.SetActive(true);
            chara.LeanAlpha(1, showDuration);
        }
        
    }
    private IEnumerator SkipButtonShow()
    {
        yield return new WaitForSeconds(showDuration);
        skipButton.gameObject.SetActive(true);
    }
    private IEnumerator CountDownCutscene()
    {
        couroStart = true;
        yield return new WaitForSeconds(waitDuration);
        textDialogues[cutscenePosition].LeanAlpha(0, closeDuration).setOnComplete(
            ()=>PlayNext()
        );
        if(chara.gameObject.activeSelf)chara.LeanAlpha(0, closeDuration);
        couroStart = false;
    }
    public void PlayNext()
    {
        textDialogues[cutscenePosition].gameObject.SetActive(false);
        cutscenePosition++;
        if(cutscenePosition < textDialogues.Length)
        {
            StartCutscene();
        }
        else
        {
            skipButton.gameObject.SetActive(false);
            BG.LeanAlpha(0, 0.5f).setOnComplete(
                ()=>Close()
            );
            
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        fade.PlayAfterCutsceneMainMenu();
    }
    public void Skip()
    {
        // Debug.Log("OIIIIIIII");
        if(couroStart)StopCoroutine(CountDownCutscene());
        gameObject.SetActive(false);
        fade.PlayAfterCutsceneMainMenu();
    }
}
