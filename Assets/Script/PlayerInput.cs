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
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            
            Debug.Log("One");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerClickAble);
            if(hit)
            {
                // Debug.Log(hit.collider.gameObject.name);
                chosenTile = hit.collider.GetComponent<MoveTile>();
                chosenTile.ChangeIsBeingClicked(true);
                
            }
        }
        if(gameInput.GetMouse0InputUp())
        {
            if(chosenTile)
            {
                chosenTile.ChangeIsBeingClicked(false);
                chosenTile.ChangeWasBeingClicked(true);
            }
            
            
            chosenTile = null;
        }
    }
    // public void DeleteChosenTile()
    // {
    //     chosenTile = null;
    // }
}
