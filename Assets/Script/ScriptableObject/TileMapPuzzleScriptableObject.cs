using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileMapPuzzle
{
    public List<TilePuzzleName> tilePuzzleListForMap; 
}
[CreateAssetMenu]
public class TileMapPuzzleScriptableObject : ScriptableObject
{
    
    public List<TileMapPuzzle> tileMapPuzzles;
}
