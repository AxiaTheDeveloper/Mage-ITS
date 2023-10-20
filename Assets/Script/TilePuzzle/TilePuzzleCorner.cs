using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleCorner : TilePuzzle
{
    public enum DirectionCornerPuzzle
    {
        LB, LU, RU, RB
    }
    [SerializeField]private DirectionCornerPuzzle direction;
    [SerializeField]private int rotationLU, rotationRU, rotationRB;
    private void Awake() 
    {
        visual = transform.GetChild(0).gameObject;
        if(direction == DirectionCornerPuzzle.LU)
        {
            RotateVisual(rotationLU);
        }
        else if(direction == DirectionCornerPuzzle.RU)
        {
            RotateVisual(rotationRU);
        }
        else if(direction == DirectionCornerPuzzle.RB)
        {
            RotateVisual(rotationRB);
        }
    }
    public void RotateVisual(int rotasi)
    {
        visual.transform.rotation = Quaternion.Euler(0f,0f,rotasi);
    }
}
