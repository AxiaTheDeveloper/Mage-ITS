using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]private PuzzleGameManager puzzleGameManager;
    public static PlayerInput Instance{get; private set;}
    [SerializeField]private LayerMask layerClickAble;
    [SerializeField]private GameInput gameInput;
    private MoveTile chosenTile;
    private void Awake() 
    {
        Instance = this;
    }
    private void Start() 
    {
        gameInput = GameInput.Instance;
        puzzleGameManager = PuzzleGameManager.Instance;
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
                    TilePuzzle tilePuzzle = hit.collider.GetComponent<TilePuzzle>();
                    if(tilePuzzle.IsTilePuzzle() && tilePuzzle.IsRotateAble())
                    {
                        if(tilePuzzle.TileName() == TilePuzzleName.StraightWireHorizontal_MoveAble || tilePuzzle.TileName() == TilePuzzleName.StraightWireHorizontal_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.StraightWireVertical_MoveAble || tilePuzzle.TileName() == TilePuzzleName.StraightWireVertical_UnMoveAble)
                        {
                            
                            TilePuzzleStraight straight = hit.collider.GetComponent<TilePuzzleStraight>();
                            if(!straight.IsRotating())
                            {
                                straight.RotateVisual(90);
                            }
                            
                        }
                        else if(tilePuzzle.TileName() == TilePuzzleName.CornerWireLB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLB_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLU_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLU_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRB_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRU_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRU_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitKanan_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitKanan_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitAtas_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitAtas_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitKiri_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitKiri_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitBawah_MoveAble || tilePuzzle.TileName() == TilePuzzleName.SplitBawah_UnMoveAble)
                        {
                            TilePuzzleCorner straight = hit.collider.GetComponent<TilePuzzleCorner>();
                            if(!straight.IsRotating())
                            {
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
