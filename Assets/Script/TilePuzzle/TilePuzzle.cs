using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InputType
{
    AllDirection, AND, OR, NOT
}
public class TilePuzzle : MonoBehaviour
{
    [SerializeField]private TilePuzzleName tileName;
    // [SerializeField]private StartBlock startBlock;
    [SerializeField]private PuzzleGameManager gameManager;
    [SerializeField]private MoveTile moveTile;
    [Header("Visuaaaaaaaaaal")]
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
    private void Awake() 
    {
        // Debug.Log(transform.GetChild(0).GetComponent<Transform>() + " " + gameObject);
        
        if(!visualSprite)visualSprite = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        // if(visualSprite)Debug.Log("Ada" + gameObject);
        
        moveTile = GetComponent<MoveTile>();
        
    }
    private void Start() 
    {
        wasRotating = false;
        if(isPuzzleAnswer)
        {
            ChangeVisual();
        }
        inputOnOff_Checker = new List<bool>();
        for(int i=0; i<inputColliderList.Count;i++)
        {
            inputOnOff_Checker.Add(false);
        }
        // startBlock = StartBlock.Instance;
        gameManager = PuzzleGameManager.Instance;
        gameManager.OnRotatingTile += gameManager_OnRotatingTile;
    }

    private void gameManager_OnRotatingTile(object sender, EventArgs e)
    {
        // Debug.Log("Ded");
        NoElectricity();
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
            }
            if(!gameManager.IsTileMoving() && !gameManager.IsTIleRotating() && !wasRotating)
            {
                bool hasElectricityInput = false;
                foreach(Collider2D colliderInput in inputColliderList)
                {
                    // Debug.Log(colliderInput);
                    Collider2D[] collidersInside = new Collider2D[5];   
                    colliderInput.OverlapCollider(new ContactFilter2D(), collidersInside);
                    // Debug.Log(collidersInside.Length);
                    hasElectricityInput = false;
                    foreach(Collider2D collider in collidersInside)
                    {
                        
                        hasElectricityInput = false;
                        if(collider && collider.gameObject.CompareTag("Output") && collider.transform.parent.transform.parent.gameObject != this.gameObject)
                        {
                            
                            Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                            TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                            if(!tilePuzzleColliderInside)
                            {
                                if(collider.transform.parent.transform.parent.gameObject)
                                {
                                    hasElectricityInput = true;
                                    // Debug.Log("Dari sini?");
                                }
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
                            int position = inputColliderList.IndexOf(colliderInput);
                            // Debug.Log(position + " " + "true");
                            inputOnOff_Checker[position] = true;
                            break;
                        }
                        else
                        {
                            int position = inputColliderList.IndexOf(colliderInput);
                            // Debug.Log(position + " " + "false");
                            inputOnOff_Checker[position] = false;
                        }
                        
                        
                        

                        
                        
                    }
                    
                }
                if(CheckSyaratNyalaTerpenuhi())
                {
                    if(!hasElectricity)YesElectricity();
                }
                else 
                {
                    if(hasElectricity)NoElectricity();
                }
            }
            else if(gameManager.IsTileMoving() || gameManager.IsTIleRotating())
            {
                
                if(hasElectricity)NoElectricity();
            }
            if(wasRotating)StartCoroutine(StartCountDown());
        }
        
        
    }
    public IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(0.01f); 
        wasRotating = false;
    }

    public void ChangeVisual()
    {
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
        // if(hasElectricity)
        // {
        //     // visualSprite = onVisual;
        //     visualSprite.sprite = onVisual[visual];
        // }
        // else
        // {
        //     // visualSprite = offVisual;
        //     visualSprite.sprite = offVisual[visual];
        // }
        
        
        
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
            bool isTileOn = true;
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(!inputChecker)
                {
                    isTileOn = false;
                    
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
                return true;
            }
            else
            {
                // startBlock.NotTheAnswer();
                return false;
            }
        }
        return false;
    }
    public void OffAllInput()
    {
        for(int i=0;i<inputOnOff_Checker.Count;i++)
        {
            inputOnOff_Checker[i] = false;
        }
    }


}
