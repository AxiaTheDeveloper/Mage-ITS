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
    private List<Collider2D> tileConnectedToOutput;// ini tu diisinya oleh si input yg jd on karena kena outputnya
    private void Start() 
    {
        startBlock = StartBlock.Instance;

        // if(isPuzzleAnswer)
        // {
        //     if(hasElectricity)visual.GetComponent<SpriteRenderer>().sprite = onVisual;
        //     else visual.GetComponent<SpriteRenderer>().sprite = offVisual;
        // }
        

        inputOnOff_Checker = new List<bool>();
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
        hasElectricity = false;
    }

    public void OutputElectricity()
    {
        bool isOutputting = false;
        // Debug.Log("Nyala " + gameObject);
        foreach(Collider2D colliderOutput in outputColliderList)
        {
            Collider2D[] collidersInside = new Collider2D[5];
            int colliderCollideTotal = colliderOutput.OverlapCollider(new ContactFilter2D(), collidersInside);
            foreach(Collider2D collider in collidersInside)
            {
                if(collider && collider.gameObject.CompareTag("Input"))
                {
                    // Debug.Log("Inputnya " + collider);
                    
                    Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                    TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                    if(!tilePuzzleColliderInside)
                    {
                        isOutputting = true;
                        FinishBlock finish = parent.GetComponentInParent<FinishBlock>();
                        finish.GotInputElectricity(collider);
                    }
                    else if(tilePuzzleColliderInside && !tilePuzzleColliderInside.HasElectricity())
                    {
                        isOutputting = true;
                        startBlock.AddTilePuzzleOn(tilePuzzleColliderInside);
                        tilePuzzleColliderInside.GotInputElectricity(collider);
                        break;
                    }
                    
                }
            }
            
            
        }
        if(!isOutputting)
        {
            Debug.Log("Tidak ada apa-apa" + gameObject);
            startBlock.NotTheAnswer();
        }
    }
    public void GotInputElectricity(Collider2D colliderGotInput)
    {
        int position = inputColliderList.IndexOf(colliderGotInput);
        inputOnOff_Checker[position] = true;
        CheckSyaratNyalaTerpenuhi();
    }
    public void CheckSyaratNyalaTerpenuhi()
    {

        if(inputType == InputType.AllDirection)
        {
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {
                    hasElectricity = true;
                    OutputElectricity();
                    break;
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
                OutputElectricity();
            }
        }
        else if(inputType == InputType.OR)
        {
            foreach(bool inputChecker in inputOnOff_Checker)
            {
                if(inputChecker)
                {
                    hasElectricity = true;
                    OutputElectricity();
                    break;
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
                    startBlock.NotTheAnswer();
                    isTileOn = false;
                    break;
                }
            }
            if(isTileOn)
            {
                hasElectricity = true;
                OutputElectricity();
            }
        }
    }


}
