using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum FinishModeWhenOn
{
    finishGame, finishStart, finishSettings, finishCredit, finishQuit,

    finishMainMenu
}
public class FinishBlock : MonoBehaviour
{
    [SerializeField]private Collider2D[] collInput;
    [SerializeField]private GameObject[] inputVisual; 
    [SerializeField]private int totalInputNeeded;
    private int totalInput;
    [SerializeField]private GameObject visualOff, visualOn;
    [SerializeField]private bool isOn;
    public event EventHandler OnFinishOn;
    [SerializeField]private FinishModeWhenOn finishMode;
    //buat main menu
    [SerializeField]private TilePuzzleManager tilepuzzleParent;
    private int NOTTile_Position;

    private void Start() 
    {
        tilepuzzleParent = GetComponentInParent<TilePuzzleManager>();
    }
    private void Update() 
    {
        if(PuzzleGameManager.Instance.GetStateGame() == PuzzleGameManager.GameState.Start)
        {
            totalInput = 0;
           
            for(int i=0;i<collInput.Length;i++)
            {
                Collider2D[] collidersInside = new Collider2D[5];
                collInput[i].OverlapCollider(new ContactFilter2D(), collidersInside);
                foreach(Collider2D collider in collidersInside)
                {
                    if(collider && collider.gameObject.CompareTag("Output"))
                    {
                        
                        Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                        TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                        if(tilePuzzleColliderInside && tilePuzzleColliderInside.HasElectricity())
                        {
                            // Debug.Log("Game WIN");
                            totalInput++;
                            inputVisual[i].SetActive(true);
                        }
                        else
                        {
                            inputVisual[i].SetActive(false);
                        }
                        break;
                    }
                            
                }

                if(totalInput == totalInputNeeded && !isOn)
                {
                    isOn = true;
                    ChangeVisual(true);
                    if(finishMode == FinishModeWhenOn.finishGame)FinishGame();
                    if(finishMode == FinishModeWhenOn.finishStart)FinishStart();
                    if(finishMode == FinishModeWhenOn.finishSettings)FinishSettings();
                    if(finishMode == FinishModeWhenOn.finishCredit)FinishCredit();
                    if(finishMode == FinishModeWhenOn.finishQuit)FinishQuit();
                    if(finishMode == FinishModeWhenOn.finishMainMenu)FinishMainMenu();
                    break;
                }
                else if(totalInput == totalInputNeeded && isOn)break;
                else if(totalInput != totalInputNeeded && i == collInput.Length - 1 && isOn)
                {
                    isOn = false;
                    ChangeVisual(false);
                }
            }
            
        }
        
    }
    // finishGame, finishStart, finishSettings, finishCredit, finishQuit
    public void FinishGame()
    {
        // Debug.Log("Finish Game");
        OnFinishOn?.Invoke(this,EventArgs.Empty);
    }
    public void FinishMainMenu()
    {
        Debug.Log("Main Menu");
        PuzzleGameManager.Instance.MainMenuMode();
        tilepuzzleParent.ResetNOTTiletoStart(NOTTile_Position);
        MainMenuControlUI.Instance.GoToMainMenu();
        
        //terus geser ke level list sambil di reset semua terus balik ke start mode;
    }
    public void FinishStart()
    {
        Debug.Log("Start");
        PuzzleGameManager.Instance.MainMenuMode();
        tilepuzzleParent.ResetNOTTiletoStart(NOTTile_Position);
        MainMenuControlUI.Instance.GoToLevelListUI();
        
        //terus geser ke level list sambil di reset semua terus balik ke start mode;
    }
    public void FinishSettings()
    {
        Debug.Log("Settings");
        PuzzleGameManager.Instance.MainMenuMode();
        tilepuzzleParent.ResetNOTTiletoStart(NOTTile_Position);
        MainMenuControlUI.Instance.GoToSettings();
        
        //terus geser ke level list sambil di reset semua terus balik ke start mode;
    }
    public void FinishCredit()
    {
        Debug.Log("Credit");
        PuzzleGameManager.Instance.MainMenuMode();
        tilepuzzleParent.ResetNOTTiletoStart(NOTTile_Position);
        MainMenuControlUI.Instance.GoToCredits();
        
        //terus geser ke level list sambil di reset semua terus balik ke start mode;
    }
    public void FinishQuit()
    {
        Debug.Log("Quit");
        PuzzleGameManager.Instance.MainMenuMode();
        tilepuzzleParent.ResetNOTTiletoStart(NOTTile_Position);
        MainMenuControlUI.Instance.Quit();
        
    }
    public void ChangeTotalInputNeeded(int change)
    {
        totalInputNeeded = change;
    }
    public void ChangeFinishMode(FinishModeWhenOn mode)
    {
        finishMode = mode;
    }
    public void ChangeVisual(bool finish)
    {
        visualOn.SetActive(finish);
        visualOff.SetActive(!finish);
    }
    public bool IsOn()
    {
        return isOn;
    }
    public void ChangeNOTTileNumber(int number)
    {
        NOTTile_Position = number;
    }



}
