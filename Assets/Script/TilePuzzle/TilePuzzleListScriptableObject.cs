using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TilePuzzleName
{
    Null, NotTile, AllPipe, HorizontalPipe, VerticalPipe, ObstaclePipe
}

[CreateAssetMenu]
public class TilePuzzleListScriptableObject : ScriptableObject
{
    public GameObject[] tilePuzzleList;
}
