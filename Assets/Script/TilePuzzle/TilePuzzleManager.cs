using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class FinishWant
{
    public FinishModeWhenOn mode;
    public int totalInputNeeded;
}
public class TilePuzzleManager : MonoBehaviour
{
    
    public static TilePuzzleManager Instance {get; private set;}
    [SerializeField]private TilePuzzleListScriptableObject tilePuzzleListSO;
    [SerializeField]private int totalRow, totalColumn, jarakAntarTile;//kolom ke kiri, row ke bawah
    [SerializeField]private Vector2 startPositionTile;
    [SerializeField]private Vector2 minPuzzleSize, maxPuzzleSize;
    private GameObject chosenTileToInstantiate;
    
    [SerializeField]private List<TilePuzzleName> tilePuzzleList_ForThisPuzzle; 

    // public event EventHandler OnFinishSpawnPuzzle;
    [SerializeField]private List<MoveTile> tileListForStart = new List<MoveTile>();
    [SerializeField]private List<FinishBlock> finishBlockList = new List<FinishBlock>();
    [SerializeField]private List<FinishWant> finishWants;
    [Header("Untuk Main Menu")]
    [SerializeField]private bool isMainMenu;
    [SerializeField]private List<MoveTile> NOTTilePuzzleList;

    
    private void Awake() 
    {
        Instance = this;

        minPuzzleSize = startPositionTile;
        maxPuzzleSize.x = startPositionTile.x + jarakAntarTile * (totalColumn-1);
        maxPuzzleSize.y = (startPositionTile.y + jarakAntarTile * (totalRow-1)) * -1;
        // int positionTilePuzzleList_Now = 0;
        int counterFinish = 0;
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
                        tileListForStart.Add(null);
                        break;
                    }
                    if(tilePuzzleList_ForThisPuzzle[listNumber] == tilePuzzleListSO.tilePuzzleList[p].GetComponent<TilePuzzle>().TileName())
                    {
                        chosenTileToInstantiate = tilePuzzleListSO.tilePuzzleList[p];

                        Transform tileInstantiate = Instantiate(chosenTileToInstantiate.transform, this.gameObject.transform);
                        
                        if(tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.FinishPuzzle)
                        {
                            FinishBlock finishTile = tileInstantiate.GetComponent<FinishBlock>();
                            finishTile.ChangeTotalInputNeeded(finishWants[counterFinish].totalInputNeeded);
                            finishTile.ChangeFinishMode(finishWants[counterFinish].mode);
                            finishTile.ChangeNOTTileNumber(counterFinish);
                            counterFinish++;
                            finishTile.OnFinishOn += finishTile_OnFinishOn;
                            finishBlockList.Add(finishTile);
                            
                        }
                        if(isMainMenu)
                        {
                            if(tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.NOTAtas_Gate_MoveAble || tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.NOTBawah_Gate_MoveAble || tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.NOTKanan_Gate_MoveAble || tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.NOTKiri_Gate_MoveAble)
                            {
                                NOTTilePuzzleList.Add(tileInstantiate.GetComponent<MoveTile>());
                            }
                        }

                        tileInstantiate.transform.localPosition = new Vector3((startPositionTile.x + jarakAntarTile * j), (startPositionTile.y + jarakAntarTile * i)*-1, 0f);

                        tileInstantiate.transform.rotation = Quaternion.Euler(0f,0f,0f);
                        MoveTile tileNow = tileInstantiate.GetComponent<MoveTile>();
                        tileNow.GetTilePuzzleManager(this);
                        tileListForStart.Add(tileNow);
                        //GANTI LEFT
                        if(j==0)
                        {
                            tileNow.ChangeLeftMax(MinPuzzleSize().x);
                        }
                        else
                        {
                            int listNumberLeft = 0;
                            if(i == 0)
                            {
                                listNumberLeft = (j-1);
                            }
                            else
                            {
                                listNumberLeft = (j-1) + i*totalColumn;
                            }
                            if(tilePuzzleList_ForThisPuzzle[listNumberLeft] == TilePuzzleName.Null)
                            {
                                tileNow.ChangeLeftMax(tileInstantiate.transform.localPosition.x - jarakAntarTile);
                            }
                            else
                            {
                                tileNow.ChangeLeftMax(tileInstantiate.transform.localPosition.x);
                                // Debug.Log(tileInstantiate.transform.localPosition.x + " " + tileInstantiate.gameObject);
                            }
                        }
                        //GANTI RIGHT
                        if(j == totalColumn - 1)
                        {
                            tileNow.ChangeRightMax(MaxPuzzleSize().x);
                        }
                        else
                        {
                            int listNumberRight = 0;
                            if(i == 0)
                            {
                                listNumberRight = (j+1);
                            }
                            else
                            {
                                listNumberRight = (j+1) + i*totalColumn;
                            }
                            if(tilePuzzleList_ForThisPuzzle[listNumberRight] == TilePuzzleName.Null)
                            {
                                tileNow.ChangeRightMax(tileInstantiate.transform.localPosition.x + jarakAntarTile);
                            }
                            else
                            {
                                tileNow.ChangeRightMax(tileInstantiate.transform.localPosition.x);
                            }
                        }
                        //GANTI TOP
                        if(i == 0)
                        {
                            tileNow.ChangeTopMax(MinPuzzleSize().y);
                        }
                        else
                        {
                            int listNumberTop = 0;
                            listNumberTop = j + (i-1)*totalColumn;
                            if(tilePuzzleList_ForThisPuzzle[listNumberTop] == TilePuzzleName.Null)
                            {
                                tileNow.ChangeTopMax(tileInstantiate.transform.localPosition.y + jarakAntarTile);
                            }
                            else
                            {
                                tileNow.ChangeTopMax(tileInstantiate.transform.localPosition.y);
                            }
                        }
                        //GANTI DOWN
                        if(i == totalRow-1)
                        {
                            tileNow.ChangeDownMax(MaxPuzzleSize().y);
                        }
                        else
                        {
                            int listNumberTop = 0;
                            listNumberTop = j + (i+1)*totalColumn;
                            if(tilePuzzleList_ForThisPuzzle[listNumberTop] == TilePuzzleName.Null)
                            {
                                tileNow.ChangeDownMax(tileInstantiate.transform.localPosition.y - jarakAntarTile);
                            }
                            else
                            {
                                tileNow.ChangeDownMax(tileInstantiate.transform.localPosition.y);
                            }
                        }
                        
                        break;
                    }
                }
                
                
            }
        }
        // OnFinishSpawnPuzzle?.Invoke(this, EventArgs.Empty);
    }

    private void finishTile_OnFinishOn(object sender, EventArgs e)
    {
        bool isFinish = true;
        foreach(FinishBlock block in finishBlockList)
        {
            if(!block.IsOn())
            {
                isFinish = false;
                break;
            }
        }
        if(isFinish)PuzzleGameManager.Instance.FinishGame();
    }

    // 0 1 2
    // 3 4 5

    // 4/(total column) = 2 -> ada d row berapa
    // <=(total row) = ada d kolom berapa
    // > total row - totalcolumn = ada d kolom berapa
    // private int x = 0;
    private void Start() 
    {
        // if(x == 1)
        // {
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
                    if(tilePuzzleList_ForThisPuzzle[listNumber] == TilePuzzleName.Null)
                    {
                        continue;
                    }
                    else
                    {
                        //CEK LEFT
                        if(j!=0)
                        {
                            int listNumberLeft = 0;
                            if(i == 0)
                            {
                                listNumberLeft = (j-1);
                            }
                            else
                            {
                                listNumberLeft = (j-1) + i*totalColumn;
                            }
                            if(tilePuzzleList_ForThisPuzzle[listNumberLeft] != TilePuzzleName.Null)
                            {
                                // Debug.Log(tileListForStart[listNumber].gameObject + " Ada d kiri");
                                tileListForStart[listNumber].AddLeft(tileListForStart[listNumberLeft].gameObject.GetComponent<Collider2D>());
                            }
                        }
                        if(j != totalColumn - 1)
                        {
                            int listNumberRight = 0;
                            if(i == 0)
                            {
                                listNumberRight = (j+1);
                            }
                            else
                            {
                                listNumberRight = (j+1) + i*totalColumn;
                            }
                            if(tilePuzzleList_ForThisPuzzle[listNumberRight] != TilePuzzleName.Null)
                            {
                                // Debug.Log(tileListForStart[listNumber].gameObject + " Ada d kanan");
                                tileListForStart[listNumber].AddRight(tileListForStart[listNumberRight].gameObject.GetComponent<Collider2D>());
                            }
                        }
                        //GANTI TOP
                        if(i != 0)
                        {
                            int listNumberTop = 0;
                            listNumberTop = j + (i-1)*totalColumn;
                            if(tilePuzzleList_ForThisPuzzle[listNumberTop] != TilePuzzleName.Null)
                            {
                                // Debug.Log(tileListForStart[listNumber].gameObject + " Ada d atas");
                                tileListForStart[listNumber].AddTop(tileListForStart[listNumberTop].gameObject.GetComponent<Collider2D>());
                            }
                        }
                        //GANTI DOWN
                        if(i != totalRow-1)
                        {
                            int listNumberTop = 0;
                            listNumberTop = j + (i+1)*totalColumn;
                            if(tilePuzzleList_ForThisPuzzle[listNumberTop] != TilePuzzleName.Null)
                            {
                                // Debug.Log(tileListForStart[listNumber].gameObject + " Ada d bwh");
                                tileListForStart[listNumber].AddDown(tileListForStart[listNumberTop].gameObject.GetComponent<Collider2D>());
                            }
                        }
                    }    
                }
            }
            // ++x;
        // }
        // else if(x<1)
        // {
            // ++x;
        // }


    }
    public int JarakAntarTile()
    {
        return jarakAntarTile;
    }
    public Vector2 MinPuzzleSize()
    {
        return minPuzzleSize;
    }
    public Vector2 MaxPuzzleSize()
    {
        return maxPuzzleSize;
    }
    
    //buat main menu
    public void ResetNOTTiletoStart(int NOTTIlePosition)
    {
        //0 start 1 options 2 credit 3 quit - lsg mati sih
        StartCoroutine(Reset(NOTTIlePosition));
    }
    public IEnumerator Reset(int NOTTIlePosition)
    {
        while(NOTTilePuzzleList[NOTTIlePosition].WasBeingClicked())
        {
            yield return null;
        }
        NOTTilePuzzleList[NOTTIlePosition].transform.localPosition = new Vector2(NOTTilePuzzleList[NOTTIlePosition].transform.localPosition.x - jarakAntarTile, NOTTilePuzzleList[NOTTIlePosition].transform.localPosition.y);
        NOTTilePuzzleList[NOTTIlePosition].IsResetPlaceMainMenu(true);
        // NOTTilePuzzleList[NOTTIlePosition].ChangeWasBeingClicked(true);
        for(int i=0;i<NOTTilePuzzleList.Count;i++)
        {
            NOTTilePuzzleList[i].ChangeTopMax(NOTTilePuzzleList[i].transform.localPosition.y);
            NOTTilePuzzleList[i].ChangeDownMax(NOTTilePuzzleList[i].transform.localPosition.y);
        }
        
    }
}