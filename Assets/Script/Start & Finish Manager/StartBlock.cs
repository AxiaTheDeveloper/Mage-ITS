using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{
    public static StartBlock Instance{get; private set;}
    // public enum StartEnergyDirection
    // {
    //     Top, Down, Left, Right
    // }
    // [SerializeField]private StartEnergyDirection directionStart;
    [SerializeField]private Collider2D outputCollider;
    [SerializeField]private Collider2D[] collidersInside;
    [SerializeField]private List<TilePuzzle> tilePuzzleOnList; // kalo pake button buat nyalain lsg gitu gaperlu bikin list

    private void Awake() 
    {
        Instance = this;
        // tilePuzzleOnList = new List<TilePuzzle>();
    }
    public void AddTilePuzzleOn(TilePuzzle tileAdd)
    {
        tilePuzzleOnList.Add(tileAdd);
    }
    // public void RemoveTilePuzzleOn(TilePuzzle tileRemove)
    // {
    //     tilePuzzleOnList.Remove(tileRemove);
    // }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Return) && PuzzleGameManager.Instance.GetStartState() == PuzzleGameManager.StartState.Normal)
        {
            Debug.Log("pew pew");
            StartCoroutine(StartOutputElectricity());
        }
    }

    public IEnumerator StartOutputElectricity()
    {
        PuzzleGameManager.Instance.StartChecking();
        // bool isOutputting = false;
        bool isTheRightWay = false;
        collidersInside = new Collider2D[5];
        int colliderCollideTotal = outputCollider.OverlapCollider(new ContactFilter2D(), collidersInside);
        // Debug.Log(colliderCollideTotal + " " + gameObject);
        foreach(Collider2D collider in collidersInside)
        {
            // Debug.Log(collider);
            if(collider && collider.gameObject.CompareTag("Input"))
            {
                bool isCourotine = true;
                Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                if(!tilePuzzleColliderInside.HasElectricity())
                {
                    // isOutputting = true;
                    // AddTilePuzzleOn(tilePuzzleColliderInside);
                    StartCoroutine(tilePuzzleColliderInside.GotInputElectricity(collider,result =>
                        {
                            isTheRightWay = result;
                            isCourotine = false;
                        }
                    ));
                    yield return new WaitUntil(()=> !isCourotine);
                    // Debug.Log(isTheRightWay;
                    break;
                }
            }
        }
        if(!isTheRightWay)
        {
            NotTheAnswer();
        }

        // if(!isOutputting)
        // {
        //     Debug.Log("Tidak ada apa-apa");
        // }
    }

    public void NotTheAnswer()
    {
        // StartCoroutine(waaiit());
        Debug.Log("Clear");

        for(int i = tilePuzzleOnList.Count - 1;i>=0;i--)
        {
            // Debug.Log(tilePuzzleOnList[i]);
            tilePuzzleOnList[i].NoElectricity();
            tilePuzzleOnList[i].OffAllInput();
        }

        tilePuzzleOnList.Clear();
        PuzzleGameManager.Instance.StartGame();
        // Debug.Log(tilePuzzleOnList.Count + "slsai");
    }
    private IEnumerator waaiit()
    {
        yield return new WaitForSeconds(0.2f);
    }
}

