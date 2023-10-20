using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePuzzleName
{
    Null, NotTile, StraightWire_MoveAble, StraightWire_UnMoveAble,CornerWire_MoveAble, CornerWire_UnMoveAble, AND_Gate_MoveAble, AND_Gate_UnMoveAble, OR_Gate_MoveAble, OR_Gate_UnMoveAble, NOT_Gate_MoveAble, NOT_Gate_UnMoveAble, Split_MoveAble, Split_UnMoveAble, Blink_MoveAble, Blink_UnMoveAble, Obstacle_MoveAble, Obstacle_UnMoveAble
}

[CreateAssetMenu]
public class TilePuzzleListScriptableObject : ScriptableObject
{
    public GameObject[] tilePuzzleList;
}
