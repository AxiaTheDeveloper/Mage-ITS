using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    [SerializeField]private Button levelListButton, RestartButton, NextLevelButton;
    [SerializeField]private RectTransform group;
    [SerializeField]private Vector3 movePlace;
    [SerializeField]private PuzzleGameManager gameManager;
    private void Awake() 
    {
        RestartButton.onClick.AddListener(
            ()=>
            {
                Restart();
            }
        );
    }
    private void Start() 
    {
        gameManager = PuzzleGameManager.Instance;
        gameManager.OnFinishGame += gameManager_OnFinishGame;
        Debug.Log(transform.position);
    }

    private void gameManager_OnFinishGame(object sender, EventArgs e)
    {
        ShowFinishUI();
        Debug.Log("finish ui");
    }

    public void Restart()
    {
        PlayerSaveManager.Instance.PlayerRestart(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void ShowFinishUI()
    {
        LeanTween.moveLocal(gameObject, movePlace, 1f).setEaseSpring();
    }
}
