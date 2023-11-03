using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapPuzzle
{
    public int totalRow, totalColumn, jarakAntarTile;
    public Vector2 startPositionTile;
    public List<TilePuzzleName> tilePuzzleListForMap; 
    public List<FinishWant> finishWantsPuzzle;
}
[CreateAssetMenu]
public class TileMapPuzzleScriptableObject : ScriptableObject
{
    
    public List<TileMapPuzzle> tileMapPuzzles;
}
