using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Counter untuk AND ato siapapun yg nantinya butuh byk input gitu di syarat")]
    private int inputCounter = 0;
    private void Start() 
    {
        inputCounter = 0;
        startBlock = StartBlock.Instance;

        // if(isPuzzleAnswer)
        // {
        //     if(hasElectricity)visual.GetComponent<SpriteRenderer>().sprite = onVisual;
        //     else visual.GetComponent<SpriteRenderer>().sprite = offVisual;
        // }
        

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
        // Debug.Log("MATI WOI");
        hasElectricity = false;
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
                        isOutputting = tilePuzzleColliderInside.GotInputElectricity(collider);
                        break;
                    }
                    
                }
            }
            
        }

        return isOutputting;
    }
    public bool GotInputElectricity(Collider2D colliderGotInput)
    {
        bool isTheRightWay = true;
        int position = inputColliderList.IndexOf(colliderGotInput);
        if(inputType == InputType.AND)
        {
            saveInputAlreadyGotInputed.Add(colliderGotInput);
        }
        inputCounter++;
        inputOnOff_Checker[position] = true;
        // CheckSyaratNyalaTerpenuhi();
        if(CheckSyaratNyalaTerpenuhi())
        {
            startBlock.AddTilePuzzleOn(this);
            isTheRightWay = OutputElectricity();
        }
        else
        {
            isTheRightWay = false;
        }

        return isTheRightWay;
    }
    public bool CheckSyaratNyalaTerpenuhi()
    {
        if(inputType == InputType.AllDirection)
        {
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {
                    hasElectricity = true;
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
                hasElectricity = true;
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
                    hasElectricity = true;
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
                hasElectricity = true;
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
    public void CheckIfThereInput()
    {
        bool hasOtherInput = true;

        for(int i=0;i<inputColliderList.Count;i++)
        {
            bool adalahTempatListrikMasuk = false;
            foreach(Collider2D coll in saveInputAlreadyGotInputed)
            {
                if(inputColliderList[i] == coll)
                {
                    adalahTempatListrikMasuk = true;
                    break;
                }
                    
            }
            if(adalahTempatListrikMasuk)continue;
            Collider2D[] collidersInside = new Collider2D[5];
            int colliderCollideTotal = inputColliderList[i].OverlapCollider(new ContactFilter2D(), collidersInside);
            foreach(Collider2D collider in collidersInside)
            {
                hasOtherInput = false;
                if(collider && collider.gameObject.CompareTag("Output"))
                {
                    hasOtherInput = true;
                    break;
                }
            }
        }
        if(!hasOtherInput)
        {
            startBlock.NotTheAnswer();
        }
    }


}
