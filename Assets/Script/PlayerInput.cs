using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
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
    }
    void Update()
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
                    chosenTile.ChangeIsBeingClicked(true);
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
                    if(tilePuzzle.TileName() == TilePuzzleName.StraightWireHorizontal_MoveAble || tilePuzzle.TileName() == TilePuzzleName.StraightWireHorizontal_UnMoveAble)
                    {
                        hit.collider.GetComponent<TilePuzzleStraight>().RotateVisual(90);
                    }
                    else if(tilePuzzle.TileName() == TilePuzzleName.CornerWireLB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLB_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLU_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireLU_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRB_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRB_UnMoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRU_MoveAble || tilePuzzle.TileName() == TilePuzzleName.CornerWireRU_UnMoveAble)
                    {
                        hit.collider.GetComponent<TilePuzzleCorner>().RotateVisual(90);
                    }
                }
                
                
            }
        }
    }

}
