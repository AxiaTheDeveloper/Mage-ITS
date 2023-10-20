using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzle : MonoBehaviour
{
    [SerializeField]private TilePuzzleName tileName;
    [SerializeField]protected GameObject visual;
    [SerializeField]private bool isTilePuzzle;
    [SerializeField]private bool isMoveAble; // tilepuzzle bisa ga gerak
    [SerializeField]private bool isPuzzleAnswer; // maksudnya trmasuk pipa ato ga
    [SerializeField]protected bool isRotateAble;

    

    
    public TilePuzzleName TileName()
    {
        return tileName;
    }
    public bool IsMoveAble()
    {
        return isMoveAble;
    }
    public bool IsTilePuzzle()
    {
        return isTilePuzzle;
    }
    public bool IsRotateAble()
    {
        return isRotateAble;
    }


}
