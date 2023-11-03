using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInput : MonoBehaviour
{
    [SerializeField]private PuzzleGameManager puzzleGameManager;
    public static PlayerInput Instance{get; private set;}
    [SerializeField]private LayerMask layerClickAble;
    [SerializeField]private GameInput gameInput;
    private MoveTile chosenTile;
    public event EventHandler OnRotate;
    private void Awake() 
    {
        Instance = this;
    }
    private void Start() 
    {
        gameInput = GameInput.Instance;
        puzzleGameManager = PuzzleGameManager.Instance;
    }
    public bool IsThereChosenTile()
    {
        if(chosenTile)
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        if(puzzleGameManager.GetStartState() == PuzzleGameManager.StartState.Normal)
        {
            if(gameInput.GetMouse0InputDown() && !chosenTile)
            {
                Ray ray = Camera.main.ScreenPointToRay(gameInput.GetMousePosition());

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerClickAble);
                if(hit)
                {
                    // Debug.Log(hit.collider.gameObject.name);
                    TilePuzzle tilePuzzle = hit.collider.GetComponent<TilePuzzle>();
                    if(tilePuzzle.IsTilePuzzle() && tilePuzzle.IsMoveAble())
                    {
                        chosenTile = hit.collider.GetComponent<MoveTile>();
                        if(!chosenTile.IsFirstTime())chosenTile.ChangeIsBeingClicked(true);
                        
                    }
                    
                }
            }
            if(gameInput.GetMouse0InputUp())
            {
                if(chosenTile)
                {
                    if(chosenTile.IsBeingClicked())
                    {
                        chosenTile.ChangeWasBeingClicked(true);
                    }
                    chosenTile.ChangeIsBeingClicked(false);
                    
                }
                
                chosenTile = null;
            }
            if(gameInput.GetMouse1InputDown())
            {
                Ray ray = Camera.main.ScreenPointToRay(gameInput.GetMousePosition());

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerClickAble);
                if(hit)
                {
                    // Debug.Log(hit.transform.name);
                    TilePuzzle tilePuzzle = hit.collider.GetComponent<TilePuzzle>();
                    if(tilePuzzle.IsTilePuzzle() && tilePuzzle.IsRotateAble())
                    {
                        if(tilePuzzle.TileName() == TilePuzzleName.StraightWireHorizontal_MoveAble ||  tilePuzzle.TileName() == TilePuzzleName.StraightWireVertical_MoveAble)
                        {
                            
                            TilePuzzleStraight straight = hit.collider.GetComponent<TilePuzzleStraight>();
                            if(!straight.IsRotating())
                            {
                                OnRotate?.Invoke(this,EventArgs.Empty);
                                SFXManager.Instance.PlayRotate();
                                straight.RotateVisual(90);
                            }
                            
                        }
                        else if(tilePuzzle.TileName() == TilePuzzleName.CornerWireLB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLU_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRU_MoveAble  || tilePuzzle.TileName() == TilePuzzleName.SplitKanan_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitAtas_MoveAble ||  tilePuzzle.TileName() == TilePuzzleName.SplitKiri_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitBawah_MoveAble )
                        {
                            TilePuzzleCorner straight = hit.collider.GetComponent<TilePuzzleCorner>();
                            if(!straight.IsRotating())
                            {
                                OnRotate?.Invoke(this,EventArgs.Empty);
                                SFXManager.Instance.PlayRotate();
                                straight.RotateVisual(90);
                            }
                        }
                    }
                    
                    
                }
            }
        }
        else
        {
            if(chosenTile)chosenTile = null;
        }  
        
        
        
    }


}
