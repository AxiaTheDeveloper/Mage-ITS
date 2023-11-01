using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePuzzleName
{
    Null, NotTile, 

    StraightWireHorizontal_MoveAble, StraightWireHorizontal_UnMoveAble, StraightWireVertical_MoveAble, StraightWireVertical_UnMoveAble, 
    CornerWireLU_MoveAble, CornerWireLU_UnMoveAble, CornerWireLB_MoveAble, CornerWireLB_UnMoveAble, CornerWireRU_MoveAble, CornerWireRU_UnMoveAble, CornerWireRB_MoveAble, CornerWireRB_UnMoveAble,  
    
    ANDKanan_Gate_MoveAble, ANDKanan_Gate_UnMoveAble, ANDKiri_Gate_MoveAble, ANDKiri_Gate_UnMoveAble, ANDAtas_Gate_MoveAble, ANDAtas_Gate_UnMoveAble,ANDBawah_Gate_MoveAble, ANDBawah_Gate_UnMoveAble, 
    
    ORKanan_Gate_MoveAble, ORKanan_Gate_UnMoveAble, ORKiri_Gate_MoveAble, ORKiri_Gate_UnMoveAble, ORAtas_Gate_MoveAble, ORAtas_Gate_UnMoveAble, ORBawah_Gate_MoveAble, ORBawah_Gate_UnMoveAble, 
    
    NOTAtas_Gate_MoveAble, NOTAtas_Gate_UnMoveAble, NOTBawah_Gate_MoveAble, NOTBawah_Gate_UnMoveAble, NOTKanan_Gate_MoveAble, NOTKanan_Gate_UnMoveAble, NOTKiri_Gate_MoveAble, NOTKiri_Gate_UnMoveAble,
    
    SplitKanan_MoveAble, SplitKanan_UnMoveAble, SplitKiri_MoveAble, SplitKiri_UnMoveAble, SplitAtas_MoveAble, SplitAtas_UnMoveAble, SplitBawah_MoveAble, SplitBawah_UnMoveAble,
    
    Blink_MoveAble, Blink_UnMoveAble, 
    
    Obstacle_MoveAble, Obstacle_UnMoveAble,

    FinishPuzzle, StartPuzzle
}

[CreateAssetMenu]
public class TilePuzzleListScriptableObject : ScriptableObject
{
    public GameObject[] tilePuzzleList;
}
