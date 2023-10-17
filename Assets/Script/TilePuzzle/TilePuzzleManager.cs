using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleManager : MonoBehaviour
{
    [SerializeField]private TilePuzzleListScriptableObject tilePuzzleListSO;
    [SerializeField]private int totalRow, totalColumn, jarakAntarTile;//kolom ke kiri, row ke bawah
    [SerializeField]private Vector2 startPositionTile;
    private GameObject chosenTileToInstantiate;
    
    [SerializeField]private List<TilePuzzleName> tilePuzzleList_ForThisPuzzle; 
    
    private void Awake() 
    {
        // int positionTilePuzzleList_Now = 0;
        for(int i=0;i<totalRow;i++)
        {
            for(int j=0;j<totalColumn;j++)
            {
                int listNumber = 0;
                if(i == 0)
                {
                    listNumber = j;
                }
                else
                {
                    listNumber = j + i*totalColumn;
                }
                for(int p=0;p<tilePuzzleListSO.tilePuzzleList.Length;p++)
                {
                    if(tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.Null)
                    {
                        break;
                    }
                    if(tilePuzzleList_ForThisPuzzle[listNumber] == tilePuzzleListSO.tilePuzzleList[p].GetComponent<TilePuzzle>().TileName())
                    {
                        chosenTileToInstantiate = tilePuzzleListSO.tilePuzzleList[p];

                        Transform tileInstantiate = Instantiate(chosenTileToInstantiate.transform, this.gameObject.transform);

                        tileInstantiate.transform.localPosition = new Vector3((startPositionTile.x + jarakAntarTile * j), (startPositionTile.y + jarakAntarTile * i)*-1, 0f);

                        tileInstantiate.transform.rotation = Quaternion.Euler(0f,0f,0f);
                        break;
                    }
                }
                
                
            }
        }
    }

    // 0 1 2
    // 3 4 5

    // 4/(total column) = 2 -> ada d row berapa
    // <=(total row) = ada d kolom berapa
    // > total row - totalcolumn = ada d kolom berapa
}
