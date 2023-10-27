using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleStraight : TilePuzzle
{
    public enum DirectionStraightPuzzle
    {
        Horizontal, Vertical
    }
    [SerializeField]private DirectionStraightPuzzle direction;
    [SerializeField]private float rotationVertical;
    private bool isRotating = false;
    private void Awake() 
    {
        visual = transform.GetChild(0).gameObject;
        if(direction == DirectionStraightPuzzle.Vertical)
        {
            RotateVisual(rotationVertical);
        }
        
    }
    public void RotateVisual(float rotasi)
    {
        // Debug.Log("test");
        PuzzleGameManager.Instance.ChangeIsTileRotating(true);
        ChangeIsBeingRotateed(true);
        Quaternion rotasi_visual = visual.transform.localRotation;
        float rotasiNew = rotasi_visual.eulerAngles.z + rotasi;
        if(rotasiNew == 360)
        {
            rotasiNew = 0;
        }
        // Debug.Log(rotasiNew);
        isRotating = true;
        visual.transform.rotation = Quaternion.Euler(0f,0f,rotasiNew);
        Berotasi();
        
        
    }

    private void Berotasi()
    {
        playerSave.AddPlayerMove();   
        PuzzleGameManager.Instance.ChangeIsTileRotating(false);
        ChangeIsBeingRotateed(false);
        isRotating = false;
        if(visual.transform.rotation.eulerAngles.z == 90 || visual.transform.rotation.eulerAngles.z == -90 || visual.transform.rotation.eulerAngles.z == 270 )
        {
            direction = DirectionStraightPuzzle.Vertical;
        }
        else
        {
            direction = DirectionStraightPuzzle.Horizontal;
        }
        ChangeVisual();
    }
    public bool IsRotating()
    {
        return isRotating;
    }
    public int GetDirection()
    {
        return (int)direction;
    }

    
}
