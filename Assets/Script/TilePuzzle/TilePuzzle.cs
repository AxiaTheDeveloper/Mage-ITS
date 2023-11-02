using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputType
{
    AllDirection, AND, OR, NOT, NoOutput
}
public class TilePuzzle : MonoBehaviour
{
    [SerializeField]private TilePuzzleName tileName;
    // [SerializeField]private StartBlock startBlock;
    [SerializeField]protected PuzzleGameManager gameManager;
    [SerializeField]protected PlayerSaveManager playerSave;
    [SerializeField]protected InGameUI inGameUI;
    [SerializeField]private MoveTile moveTile;
    [Header("Visuaaaaaaaaaal")]
    [SerializeField]private string nameTILE;
    [SerializeField]protected GameObject visual;
    [SerializeField]protected SpriteRenderer visualSprite;
    [SerializeField]private Sprite[] onVisual;
    [SerializeField]private Sprite[] offVisual;

    [Header("Segala macam Bool Penting")]
    [SerializeField]private bool isTilePuzzle;
    [SerializeField]private bool isMoveAble; // tilepuzzle bisa ga gerak
    [SerializeField]private bool isPuzzleAnswer; // maksudnya trmasuk pipa ato ga
    [SerializeField]protected bool isRotateAble;
    [SerializeField]private bool hasElectricity;

    [Header("Collider Input & output")]
    [SerializeField]private InputType inputType;
    [SerializeField]private List<Collider2D> inputColliderList;
    private List<bool> inputOnOff_Checker;
    [SerializeField]private List<Collider2D> outputColliderList;
    private List<Collider2D> saveInputAlreadyGotInputed;

    // [Header("Counter untuk AND ato siapapun yg nantinya butuh byk input gitu di syarat")]
    // private int inputCounter = 0;
    [SerializeField]protected bool isBeingRotateed, wasRotating;
    private bool isCourotineRunning = false;
    private void Awake() 
    {
        // Debug.Log(transform.GetChild(0).GetComponent<Transform>() + " " + gameObject);
        
        if(!visualSprite)visualSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        // if(visualSprite)Debug.Log("Ada" + gameObject);
        
        moveTile = GetComponent<MoveTile>();
        
    }
    private void Start() 
    {
        inGameUI = InGameUI.Instance;
        playerSave = PlayerSaveManager.Instance;
        gameManager = PuzzleGameManager.Instance;
        gameManager.OnRotatingTile += gameManager_OnRotatingTile;
        wasRotating = false;
        
        inputOnOff_Checker = new List<bool>();
        for(int i=0; i<inputColliderList.Count;i++)
        {
            inputOnOff_Checker.Add(false);
        }
        if(isPuzzleAnswer)
        {
            ChangeVisual();
        }
        // startBlock = StartBlock.Instance;
        
    }

    private void gameManager_OnRotatingTile(object sender, EventArgs e)
    {
        // Debug.Log("Ded");
        NoElectricity();
        // OffAllInput();
        wasRotating = true;
    }

    private void Update() 
    {
        //if(!moveTile.IsBeingClicked() && !isBeingRotateed)
        if(gameManager.GetStateGame() == PuzzleGameManager.GameState.Start)
        {
            if(wasRotating)
            {
                NoElectricity();
                // OffAllInput();
            }
            if(!gameManager.IsTileMoving() && !gameManager.IsTIleRotating() && !wasRotating  && inputType != InputType.NoOutput)
            {
                bool hasElectricityInput = false;
                // int counter = 0;
                // foreach(Collider2D colliderInput in inputColliderList)
                for(int i=0;i<inputColliderList.Count;i++)
                {
                    
                    // Debug.Log(colliderInput);
                    Collider2D[] collidersInside = new Collider2D[5];   
                    inputColliderList[i].OverlapCollider(new ContactFilter2D(), collidersInside);
                    // Debug.Log(collidersInside.Length);
                    hasElectricityInput = false;
                    // counter++;
                    foreach(Collider2D collider in collidersInside)
                    {
                        
                        
                        hasElectricityInput = false;
                        if(collider && collider.gameObject.CompareTag("Output") && collider.transform.parent.transform.parent.gameObject != this.gameObject)
                        {
                            
                            Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                            TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                            if(tilePuzzleColliderInside && (tilePuzzleColliderInside.TileName() == TilePuzzleName.StartPuzzleKanan || tilePuzzleColliderInside.TileName() == TilePuzzleName.StartPuzzleKiri || tilePuzzleColliderInside.TileName() == TilePuzzleName.StartPuzzleAtas || tilePuzzleColliderInside.TileName() == TilePuzzleName.StartPuzzleBawah))
                            {
                                hasElectricityInput = true;
                            }
                            else if(tilePuzzleColliderInside && tilePuzzleColliderInside.HasElectricity())
                            {
                                // YesElectricity();
                                hasElectricityInput = true;
                                // Debug.Log("ato ini ? ");
                            }
                            
                        }
                        if(hasElectricityInput)
                        {
                            // if(!hasElectricity)
                            // {
                                // int position = inputColliderList.IndexOf(colliderInput);
                                // Debug.Log(position + " " + "true");
                                inputOnOff_Checker[i] = true;
                                
                                if(inputType == InputType.AllDirection)
                                {
                                    // Debug.Log(outputColliderList[i]);
                                    if(outputColliderList[i].enabled)outputColliderList[i].enabled = false;
                                }
                                break;
                            // }
                            
                        }
                        else
                        {
                            // int position = inputColliderList.IndexOf(colliderInput);
                            // Debug.Log(position + " " + "false");
                            inputOnOff_Checker[i] = false;
                            if(inputType == InputType.AllDirection)
                            {
                                // Debug.Log(outputColliderList[i]);
                                if(!outputColliderList[i].enabled)outputColliderList[i].enabled = true;
                            }
                            
                        }
                        
                        
                        
                    }
                    
                }
                if(CheckSyaratNyalaTerpenuhi())
                {
                    if(!hasElectricity)YesElectricity();
                }
                else 
                {
                    if(hasElectricity)
                    {
                        NoElectricity();
                    }
                    
                    if(tileName == TilePuzzleName.ANDAtas_Gate_MoveAble || tileName == TilePuzzleName.ANDAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ANDBawah_Gate_MoveAble || tileName == TilePuzzleName.ANDBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKanan_Gate_MoveAble || tileName == TilePuzzleName.ANDKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKiri_Gate_MoveAble || tileName == TilePuzzleName.ANDKiri_Gate_UnMoveAble || tileName == TilePuzzleName.NOTAtas_Gate_MoveAble || tileName == TilePuzzleName.NOTAtas_Gate_UnMoveAble || tileName == TilePuzzleName.NOTBawah_Gate_MoveAble || tileName == TilePuzzleName.NOTBawah_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKanan_Gate_MoveAble || tileName == TilePuzzleName.NOTKanan_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKiri_Gate_MoveAble || tileName == TilePuzzleName.NOTKiri_Gate_UnMoveAble)
                    {
                        ChangeVisual();
                    }
                    else
                    {
                        // OffAllInput();
                    }
                    
                
                }
            }
            else if((gameManager.IsTileMoving() || gameManager.IsTIleRotating()) && inputType != InputType.NoOutput)
            {
                if(hasElectricity)
                {
                    // OffAllInput();
                    NoElectricity();
                }
                
                if(tileName == TilePuzzleName.ANDAtas_Gate_MoveAble || tileName == TilePuzzleName.ANDAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ANDBawah_Gate_MoveAble || tileName == TilePuzzleName.ANDBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKanan_Gate_MoveAble || tileName == TilePuzzleName.ANDKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKiri_Gate_MoveAble || tileName == TilePuzzleName.ANDKiri_Gate_UnMoveAble || tileName == TilePuzzleName.NOTAtas_Gate_MoveAble || tileName == TilePuzzleName.NOTAtas_Gate_UnMoveAble || tileName == TilePuzzleName.NOTBawah_Gate_MoveAble || tileName == TilePuzzleName.NOTBawah_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKanan_Gate_MoveAble || tileName == TilePuzzleName.NOTKanan_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKiri_Gate_MoveAble || tileName == TilePuzzleName.NOTKiri_Gate_UnMoveAble)ChangeVisual();
            }
            if(wasRotating && !isCourotineRunning)StartCoroutine(StartCountDown());
        }
        
        
    }
    public IEnumerator StartCountDown()
    {
        isCourotineRunning = true;
        yield return new WaitForSeconds(0.1f); 
        wasRotating = false;
        isCourotineRunning = false;
    }

    public void ChangeVisual()
    {
        // Debug.Log(gameObject + " test" + hasElectricity);
        int visual = 0;
        if(isRotateAble)
        {
            if(GetComponent<TilePuzzleStraight>())
            {
                visual = GetComponent<TilePuzzleStraight>().GetDirection();
            }
            else
            {
                visual = GetComponent<TilePuzzleCorner>().GetDirection();
            }
            if(hasElectricity)
            {
                // visualSprite = onVisual;
                visualSprite.sprite = onVisual[visual];
            }
            else
            {
                // visualSprite = offVisual;
                visualSprite.sprite = offVisual[visual];
            }
        }
        else
        {
            if(hasElectricity)
            {
                // Debug.Log("harusnya ga ke sini");
                if(tileName == TilePuzzleName.ANDAtas_Gate_MoveAble || tileName == TilePuzzleName.ANDAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ANDBawah_Gate_MoveAble || tileName == TilePuzzleName.ANDBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKanan_Gate_MoveAble || tileName == TilePuzzleName.ANDKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKiri_Gate_MoveAble || tileName == TilePuzzleName.ANDKiri_Gate_UnMoveAble)
                {
                    if(inputOnOff_Checker[0] && inputOnOff_Checker[1])
                    {
                        visual = 0;
                    }
                    else if(inputOnOff_Checker[0] && inputOnOff_Checker[2])
                    {
                        visual = 1;
                    }
                    else if(inputOnOff_Checker[1] && inputOnOff_Checker[2])
                    {
                        visual = 2;
                    }
                    visualSprite.sprite = onVisual[visual];
                }
                if(tileName == TilePuzzleName.NOTAtas_Gate_MoveAble || tileName == TilePuzzleName.NOTAtas_Gate_UnMoveAble || tileName == TilePuzzleName.NOTBawah_Gate_MoveAble || tileName == TilePuzzleName.NOTBawah_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKanan_Gate_MoveAble || tileName == TilePuzzleName.NOTKanan_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKiri_Gate_MoveAble || tileName == TilePuzzleName.NOTKiri_Gate_UnMoveAble)visualSprite.sprite = onVisual[visual];

                if(tileName == TilePuzzleName.ORAtas_Gate_MoveAble || tileName == TilePuzzleName.ORAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ORBawah_Gate_MoveAble || tileName == TilePuzzleName.ORBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ORKanan_Gate_MoveAble || tileName == TilePuzzleName.ORKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ORKiri_Gate_MoveAble || tileName == TilePuzzleName.ORKiri_Gate_UnMoveAble)
                {
                    if(inputOnOff_Checker[0])
                    {
                        visual = 0;
                    }
                    else if(inputOnOff_Checker[1])
                    {
                        visual = 1;
                    }
                    visualSprite.sprite = onVisual[visual];
                }
                
            }
            else
            {
                if(tileName == TilePuzzleName.ANDAtas_Gate_MoveAble || tileName == TilePuzzleName.ANDAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ANDBawah_Gate_MoveAble || tileName == TilePuzzleName.ANDBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKanan_Gate_MoveAble || tileName == TilePuzzleName.ANDKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ANDKiri_Gate_MoveAble || tileName == TilePuzzleName.ANDKiri_Gate_UnMoveAble)
                {
                    if(inputOnOff_Checker[0] || inputOnOff_Checker[1] || inputOnOff_Checker[2])
                    {
                        if(inputOnOff_Checker[0])
                        {
                            visual = 1;
                        }
                        else if(inputOnOff_Checker[1])
                        {
                            visual = 2;
                        }
                        else if(inputOnOff_Checker[2])
                        {
                            visual = 3;
                        }
                    }
                    else if(inputOnOff_Checker[0] && inputOnOff_Checker[1] && inputOnOff_Checker[2])
                    {
                        visual = 0; 
                    }
                    if(gameManager.IsTileMoving() || gameManager.IsTIleRotating())
                    {
                        visual = 0; 
                    }
                    visualSprite.sprite = offVisual[visual];
                }
                if(tileName == TilePuzzleName.NOTAtas_Gate_MoveAble || tileName == TilePuzzleName.NOTAtas_Gate_UnMoveAble || tileName == TilePuzzleName.NOTBawah_Gate_MoveAble || tileName == TilePuzzleName.NOTBawah_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKanan_Gate_MoveAble || tileName == TilePuzzleName.NOTKanan_Gate_UnMoveAble || tileName == TilePuzzleName.NOTKiri_Gate_MoveAble || tileName == TilePuzzleName.NOTKiri_Gate_UnMoveAble)
                {
                    // Debug.Log("here??");
                    visualSprite.sprite = offVisual[visual];
                    if(gameManager.IsTileMoving() || gameManager.IsTIleRotating() || wasRotating)visualSprite.sprite = onVisual[visual];
                    
                }

                if(tileName == TilePuzzleName.ORAtas_Gate_MoveAble || tileName == TilePuzzleName.ORAtas_Gate_UnMoveAble || tileName == TilePuzzleName.ORBawah_Gate_MoveAble || tileName == TilePuzzleName.ORBawah_Gate_UnMoveAble || tileName == TilePuzzleName.ORKanan_Gate_MoveAble || tileName == TilePuzzleName.ORKanan_Gate_UnMoveAble || tileName == TilePuzzleName.ORKiri_Gate_MoveAble || tileName == TilePuzzleName.ORKiri_Gate_UnMoveAble)
                {
                    visualSprite.sprite = offVisual[visual];
                }
                
            }
        }
    }
    
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
    public bool HasElectricity()
    {
        return hasElectricity;
    }
    public void NoElectricity()
    {
        hasElectricity = false;
        
        ChangeVisual();
        
    }
    public void YesElectricity()
    {
        hasElectricity = true;
        ChangeVisual();
        
    }
    public bool IsBeingRotateed()
    {
        return isBeingRotateed;
    }
    protected void ChangeIsBeingRotateed(bool change)
    {
        isBeingRotateed = change;
    }

    // public bool OutputElectricity()
    // {
    //     bool isOutputting = false;

    //     foreach(Collider2D colliderOutput in outputColliderList)
    //     {
    //         Collider2D[] collidersInside = new Collider2D[5];
    //         int colliderCollideTotal = colliderOutput.OverlapCollider(new ContactFilter2D(), collidersInside);
    //         foreach(Collider2D collider in collidersInside)
    //         {
    //             if(collider && collider.gameObject.CompareTag("Input"))
    //             {
    //                 bool isOwnColliderInput = false;
    //                 foreach(Collider2D cll in inputColliderList)
    //                 {
    //                     if(collider == cll)
    //                     {
    //                         isOwnColliderInput = true;
    //                         break;
    //                     }
    //                 }
    //                 if(isOwnColliderInput)continue;
    //                 Transform parent = collider.gameObject.GetComponentInParent<Transform>();
    //                 TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
    //                 if(!tilePuzzleColliderInside)
    //                 {
    //                     // isOutputting = true;
    //                     FinishBlock finish = parent.GetComponentInParent<FinishBlock>();
    //                     isOutputting = finish.GotInputElectricity(collider);
    //                 }
    //                 else if(tilePuzzleColliderInside && !tilePuzzleColliderInside.HasElectricity())
    //                 {
    //                     // isOutputting = true;
    //                     // startBlock.AddTilePuzzleOn(tilePuzzleColliderInside);
    //                     isOutputting = tilePuzzleColliderInside.GotInputElectricity(collider);
    //                     break;
    //                 }
                    
    //             }
    //             // if(PuzzleGameManager.Instance.GetStateGame() == PuzzleGameManager.GameState.Finish)
    //             // {
    //             //     return true;
    //             //     // break;
    //             // }
                
                
    //         }
    //         if(colliderOutput)Debug.Log(isOutputting + " " + gameObject + colliderOutput.gameObject);
            
    //     }

    //     return isOutputting;
    // }
    // public bool GotInputElectricity(Collider2D colliderGotInput)
    // {
    //     bool isTheRightWay = true;
    //     int position = inputColliderList.IndexOf(colliderGotInput);
    //     if(inputType == InputType.AND)
    //     {
    //         saveInputAlreadyGotInputed.Add(colliderGotInput);
    //     }
    //     // inputCounter++;
    //     inputOnOff_Checker[position] = true;
    //     // CheckSyaratNyalaTerpenuhi();
    //     if(CheckSyaratNyalaTerpenuhi())
    //     {
            
    //         YesElectricity();
    //         startBlock.AddTilePuzzleOn(this);
    //         isTheRightWay = OutputElectricity();
    //     }
    //     else
    //     {
    //         isTheRightWay = false;
    //     }

    //     return isTheRightWay;
    // }
    // public bool GotInputElectricity(Collider2D colliderGotInput)
    // {
    //     bool isTheRightWay = true;
    //     int position = inputColliderList.IndexOf(colliderGotInput);
    //     if(inputType == InputType.AND)
    //     {
    //         saveInputAlreadyGotInputed.Add(colliderGotInput);
    //     }
    //     // inputCounter++;
    //     inputOnOff_Checker[position] = true;
    //     // CheckSyaratNyalaTerpenuhi();
    //     if(CheckSyaratNyalaTerpenuhi())
    //     {
            
    //         YesElectricity();
    //         startBlock.AddTilePuzzleOn(this);
    //         isTheRightWay = OutputElectricity();
    //     }
    //     else
    //     {
    //         isTheRightWay = false;
    //     }

    //     return isTheRightWay;
    // }

    public bool CheckSyaratNyalaTerpenuhi()
    {
        if(inputType == InputType.AllDirection)
        {
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {
                    return true;
                }
            }
        }
        else if(inputType == InputType.AND)
        {
            bool isTileOn = false;
            int totalGotInput = 0;
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                // if(!inputChecker)
                // {
                //     isTileOn = false;
                    
                //     break;
                // }
                if(inputChecker)totalGotInput++;
                if(totalGotInput == 2)
                {
                    isTileOn = true;
                    break;
                }
            }
            if(isTileOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(inputType == InputType.OR)
        {
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {
                    // YesElectricity();
                    // OutputElectricity();
                    // break;
                    return true;
                }
            }
        }
        else if(inputType == InputType.NOT)
        {
            bool isTileOn = true;
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {

                    isTileOn = false;
                    break;
                }
            }
            if(isTileOn)
            {
                // YesElectricity();
                // OutputElectricity();
                // Debug.Log("Nyala");
                return true;
            }
            else
            {
                // Debug.Log("Mati");
                // startBlock.NotTheAnswer();
                return false;
            }
        }
        else if(inputType == InputType.NoOutput) return false;
        return false;
    }
    public void OffAllInput()
    {
        for(int i=0;i<inputOnOff_Checker.Count;i++)
        {
            inputOnOff_Checker[i] = false;
        }
    }

    private void OnMouseEnter() 
    {
        if(inGameUI)inGameUI.ChangeNameText(nameTILE);
    }
    private void OnMouseExit() 
    {
        if(inGameUI)inGameUI.ChangeNameText("");
    }

}