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
    [SerializeField]private StartBlock startBlock;
    [Header("Visuaaaaaaaaaal")]
    [SerializeField]protected GameObject visual;
    [SerializeField]private SpriteRenderer visualSprite;
    [SerializeField]private Sprite onVisual;
    [SerializeField]private Sprite offVisual;

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
    private void Awake() 
    {
        // Debug.Log(transform.GetChild(0).GetComponent<Transform>() + " " + gameObject);
        if(!visualSprite)visualSprite = transform.GetChild(0).GetComponent<Transform>().GetComponentInChildren<SpriteRenderer>();
        // if(visualSprite)Debug.Log("Ada" + gameObject);
    }
    private void Start() 
    {
        // inputCounter = 0;
        startBlock = StartBlock.Instance;

        if(isPuzzleAnswer)
        {
            ChangeVisual();
        }
        
        

        inputOnOff_Checker = new List<bool>();
        if(inputType == InputType.AND)
        {
            saveInputAlreadyGotInputed = new List<Collider2D>();
        }
        
        for(int i=0; i<inputColliderList.Count;i++)
        {
            inputOnOff_Checker.Add(false);
        }
    }
    public void ChangeVisual()
    {
        if(hasElectricity)
        {
            // visualSprite = onVisual;
            visualSprite.color = Color.green;
        }
        else
        {
            // visualSprite = offVisual;
            visualSprite.color = Color.red;
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

    public bool OutputElectricity()
    {
        bool isOutputting = false;

        foreach(Collider2D colliderOutput in outputColliderList)
        {
            Collider2D[] collidersInside = new Collider2D[5];
            int colliderCollideTotal = colliderOutput.OverlapCollider(new ContactFilter2D(), collidersInside);
            foreach(Collider2D collider in collidersInside)
            {
                if(collider && collider.gameObject.CompareTag("Input"))
                {
                    Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                    TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                    if(!tilePuzzleColliderInside)
                    {
                        // isOutputting = true;
                        FinishBlock finish = parent.GetComponentInParent<FinishBlock>();
                        isOutputting = finish.GotInputElectricity(collider);
                    }
                    else if(tilePuzzleColliderInside && !tilePuzzleColliderInside.HasElectricity())
                    {
                        // isOutputting = true;
                        // startBlock.AddTilePuzzleOn(tilePuzzleColliderInside);
                        StartCoroutine(tilePuzzleColliderInside.GotInputElectricity(collider,result =>
                            {
                                isOutputting = result;
                            }
                        ));
                        break;
                    }
                    
                }
                if(PuzzleGameManager.Instance.GetStateGame() == PuzzleGameManager.GameState.Finish)
                {
                    return true;
                    // break;
                }
            }
            
        }

        return isOutputting;
    }
    public IEnumerator GotInputElectricity(Collider2D colliderGotInput, Action<bool> callback)
    {
        // Debug.Log("hi");
        bool isTheRightWay = true;
        int position = inputColliderList.IndexOf(colliderGotInput);
        if(inputType == InputType.AND)
        {
            saveInputAlreadyGotInputed.Add(colliderGotInput);
        }
        // inputCounter++;
        inputOnOff_Checker[position] = true;
        // CheckSyaratNyalaTerpenuhi();
        if(CheckSyaratNyalaTerpenuhi())
        {
            yield return new WaitForSeconds(0.1f);
            YesElectricity();
            startBlock.AddTilePuzzleOn(this);
            isTheRightWay = OutputElectricity();
            Debug.Log(isTheRightWay + "benar");
        }
        else
        {
            isTheRightWay = false;
            Debug.Log(isTheRightWay + "salah");
        }
        
        callback(isTheRightWay);
    }

    public bool CheckSyaratNyalaTerpenuhi()
    {
        if(inputType == InputType.AllDirection)
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
                // YesElectricity();
                // OutputElectricity();
                return true;
            }
            else
            {
                // if(inputCounter == 1)
                // {
                //     CheckIfThereInput();
                // }
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
