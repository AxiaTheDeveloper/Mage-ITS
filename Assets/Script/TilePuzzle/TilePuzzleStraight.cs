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
    [SerializeField]private int rotationVertical;
    private void Awake() 
    {
        visual = transform.GetChild(0).gameObject;
        if(direction == DirectionStraightPuzzle.Vertical)
        {
            RotateVisual(rotationVertical);
        }
    }
    public void RotateVisual(int rotasi)
    {
        visual.transform.rotation = Quaternion.Euler(0f,0f,rotasi);
    }
}
