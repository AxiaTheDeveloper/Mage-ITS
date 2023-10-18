using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    [SerializeField]private TilePuzzle tilePuzzle;
    [SerializeField]private TilePuzzleManager tilePuzzleManager;
    [SerializeField]private GameInput gameInput;

    
    private bool isMoveAble;
    private bool isBeingClicked = false, wasBeingClicked = true;
    private Vector3 mousePos;
    private float startPosX, startPosY, leftMax, topMax, rightMax, downMax;

    [SerializeField]private Collider2D top,down,left,right;

    private void Awake() 
    {
        tilePuzzle = GetComponent<TilePuzzle>();
        isMoveAble = tilePuzzle.IsMoveAble();
        tilePuzzleManager = GetComponentInParent<TilePuzzleManager>();

        for(int i=1;i<transform.childCount;i++)
        {
            if(i==1)top = transform.GetChild(i).GetComponent<Collider2D>();
            if(i==2)down = transform.GetChild(i).GetComponent<Collider2D>();
            if(i==3)left = transform.GetChild(i).GetComponent<Collider2D>();
            if(i==4)right = transform.GetChild(i).GetComponent<Collider2D>();
        }
    }
    private void Start() 
    {
        gameInput = GameInput.Instance;

        ErrorLog();
        // leftMax = transform.position.x - tilePuzzleManager.JarakAntarTile();
        // rightMax = transform.position.x;
        topMax = transform.position.y + tilePuzzleManager.JarakAntarTile();
        downMax = transform.position.y - tilePuzzleManager.JarakAntarTile();
    }
    private void ErrorLog()
    {
        if(!gameInput)Debug.LogError("Gameinput di " + gameObject + "belum ada");
        if(!tilePuzzle)Debug.LogError("TilePuzzle di " + gameObject + "belum ada");
        if(!tilePuzzleManager)Debug.LogError("TilePuzzleManager di " + gameObject + "belum ada");
    }
    private void Update() 
    {
        if(wasBeingClicked)
        {
            //sbnrnya ada checker si kalo misal td tu digeser ud masuk ke zona empty lain gitu, ntr dia pindah ke posisi itu lsg, tp skrg ini dulu trus ada checker di seklilingnya ada kosong ato ga, kalo ga ya  min max posnya ya dibikin dia gabisa gerak
            startPosX = transform.position.x;
            startPosY = transform.position.y;
            
            wasBeingClicked = false;
        }
        if(isBeingClicked)
        {
            mousePos = gameInput.GetMousePosition();
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            
            if(mousePos.x < tilePuzzleManager.MinPuzzleSize().x)
            {
                // Debug.Log("Ini kalo diujung kiri");
                if(leftMax > tilePuzzleManager.MinPuzzleSize().x) mousePos.x = leftMax;
                else mousePos.x = tilePuzzleManager.MinPuzzleSize().x;
                
            }
            else if(mousePos.x > tilePuzzleManager.MaxPuzzleSize().x)
            {
                // Debug.Log("Ini kalo diujung kanan");
                if(rightMax < tilePuzzleManager.MaxPuzzleSize().x) mousePos.x = rightMax;
                else mousePos.x = tilePuzzleManager.MaxPuzzleSize().x;
            }
            else
            {
                // Debug.Log("Ini kalo di antara");
                if(mousePos.x < leftMax)
                {
                    // Debug.Log("Ini kalo di lebih batas kiri");
                    mousePos.x = leftMax;
                }
                else if(mousePos.x > rightMax)
                {
                    // Debug.Log("Ini kalo di lebih batas kanan");
                    mousePos.x = rightMax;
                }
            }

            if(mousePos.y > tilePuzzleManager.MinPuzzleSize().y) // dibalik krn minus
            {
                if(tilePuzzleManager.MinPuzzleSize().y > topMax) mousePos.y = topMax;
                else mousePos.y = tilePuzzleManager.MinPuzzleSize().y;
                
            }
            else if(mousePos.y < tilePuzzleManager.MaxPuzzleSize().y)
            {
                if(tilePuzzleManager.MaxPuzzleSize().y < downMax) mousePos.y = downMax;
                else mousePos.y = tilePuzzleManager.MaxPuzzleSize().y;
                
            }
            else
            {
                if(mousePos.y > topMax)
                {
                    mousePos.y = topMax;
                }
                else if(mousePos.y < downMax)
                {
                    mousePos.y = downMax;
                }
            }

            transform.localPosition = mousePos;
            
            wasBeingClicked = true;
        }

    }

    public void ChangeIsBeingClicked(bool change)
    {
        isBeingClicked = change;
    }
    public bool IsBeingClicked()
    {
        return isBeingClicked;
    }
    public void ChangeLeftMax(float change)
    {
        leftMax = change;
    }
    public void ChangeRightMax(float change)
    {
        rightMax = change;
    }
    public void ChangeTopMax(float change)
    {
        topMax = change;
    }
    public void ChangeDownMax(float change)
    {
        downMax = change;
    }

}
