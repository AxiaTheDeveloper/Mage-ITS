using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    [SerializeField]private TilePuzzle tilePuzzle;
    [SerializeField]private GameInput gameInput;
    private bool isMoveAble;
    private bool isBeingClicked = false;
    private Vector3 mousePos;
    private void Awake() 
    {
        tilePuzzle = GetComponent<TilePuzzle>();
        isMoveAble = tilePuzzle.IsMoveAble();

        gameInput = GameInput.Instance;
    }
    private void Update() 
    {
        
    }
    private void OnMouseDown() 
    {
        mousePos = gameInput.GetMousePosition();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
        
        isBeingClicked = true;

    }

}
