using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePuzzleName
{
    Null, NotTile, StraightWireHorizontal_MoveAble, StraightWireHorizontal_UnMoveAble, StraightWireVertical_MoveAble, StraightWireVertical_UnMoveAble, CornerWireLU_MoveAble, CornerWireLU_UnMoveAble, CornerWireLB_MoveAble, CornerWireLB_UnMoveAble, CornerWireRU_MoveAble, CornerWireRU_UnMoveAble, CornerWireRB_MoveAble, CornerWireRB_UnMoveAble,  AND_Gate_MoveAble, AND_Gate_UnMoveAble, OR_Gate_MoveAble, OR_Gate_UnMoveAble, NOT_Gate_MoveAble, NOT_Gate_UnMoveAble, Split_MoveAble, Split_UnMoveAble, Blink_MoveAble, Blink_UnMoveAble, Obstacle_MoveAble, Obstacle_UnMoveAble
}

[CreateAssetMenu]
public class TilePuzzleListScriptableObject : ScriptableObject
{
    public GameObject[] tilePuzzleList;
}
