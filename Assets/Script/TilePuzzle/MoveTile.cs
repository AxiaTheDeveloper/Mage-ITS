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
    [SerializeField]private Vector3 mousePos;
    [SerializeField]private float startPosX, startPosY, leftMax, topMax, rightMax, downMax;

    [SerializeField]private Collider2D top,down,left,right;

    [SerializeField]private RaycastHit2D[] hitObjectLeft, hitObjectRight, hitObjectTop, hitObjectDown;
    [SerializeField]private LayerMask tileLayerCollide;

    private bool goHorizontal, goVertical, isFirstTime = true;

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

        startPosX = transform.position.x;
        startPosY = transform.position.y;

        
        CheckTileBeside();

        ErrorLog();
        // leftMax = transform.position.x - tilePuzzleManager.JarakAntarTile();
        // rightMax = transform.position.x + tilePuzzleManager.JarakAntarTile();
        // topMax = transform.position.y + tilePuzzleManager.JarakAntarTile();
        // downMax = transform.position.y - tilePuzzleManager.JarakAntarTile();
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

            
            
            if(mousePos.x != 0)
            {
                if(transform.localPosition.x % 2 != 0)
                {
                    if(transform.localPosition.x % 2 >= 1)
                    {
                        Debug.Log(Mathf.Floor(transform.localPosition.x) + 1);
                        transform.localPosition = new Vector2(Mathf.Floor(transform.localPosition.x) + 1, transform.localPosition.y);
                        
                    }
                    else
                    {   
                        // Debug.Log(Mathf.Floor(transform.localPosition.x));
                        transform.localPosition = new Vector2(Mathf.Floor(transform.localPosition.x), transform.localPosition.y);
                        
                    }
                }
            }
            else if(mousePos.y != 0)
            {
                if(transform.localPosition.y % 2 != 0)
                {
                    if(transform.localPosition.y % 2 >= 1)
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Floor(transform.localPosition.y) + 1);
                    }
                    else
                    {
                        transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Floor(transform.localPosition.y));
                    }
                }
                
            }
            

            startPosX = transform.position.x;
            startPosY = transform.position.y;

            CheckTileBeside();
            //trus di sini cek collider apa ada kiri kanan
            
            wasBeingClicked = false;
        }
        if(isBeingClicked)
        {
            mousePos = gameInput.GetMousePosition();
            // Debug.Log("World input" + mousePos);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            // Debug.Log(mousePos);
            
            
            
            
            
            

            // //ke kanan
            // if(( mousePos.x >= 0 && mousePos.y < 0 && mousePos.x >= mousePos.y))
            // {
            //     mousePos.y = 0f;
            // }
            // //ke bawah
            // else if(mousePos.x < mousePos.y)
            // {
            //     mousePos.x = 0f;
            // }
            
            // Debug.Log(mousePos);
            // Debug.Log((mousePos.y - transform.localPosition.y) + "DAN" + (mousePos.x - transform.localPosition.x));
            // if(Mathf.Abs(mousePos.y ) > Mathf.Abs(mousePos.x))
            // {
                
            //     Debug.Log(mousePos + " x 0");
            //     goHorizontal = false;
            //     goVertical = true;
            //     mousePos.x = 0f;
            //     // Debug.Log(mousePos);
            // }
            // else
            // {
            //     Debug.Log(mousePos + " y 0");
            //     mousePos.y = 0f;
            //     goHorizontal = true;
            //     goVertical = false;
            // }
            
            if(mousePos.x < leftMax)
            {
                // Debug.Log("melebihi dari leftmax");
                mousePos.x = leftMax;   
            }
            else if(mousePos.x > rightMax)
            {
                // Debug.Log("melebihi dari rightmax");
                mousePos.x = rightMax; 
            }
            if(mousePos.y > topMax)
            {
                mousePos.y = topMax;   
            }
            else if(mousePos.y < downMax)
            {
                mousePos.y = downMax; 
            }
            if(mousePos.x != 0 && mousePos.y != 0)
            {

            }
            else{
                transform.localPosition = mousePos;
            }
            
            // Debug.Log(mousePos);
            

            // Debug.Log(mousePos);
            // if(mousePos.x == 6)   Debug.Log(mousePos);
            
            
            // Vector3 localDirection = transform.InverseTransformDirection(mousePos - transform.localPosition);

            // transform.Translate(localDirection, Space.Self);

            
            
            if(mousePos.x != 0)
            {
                if(transform.localPosition.x % 2 == 0 && transform.localPosition.x != startPosX) 
                {
                    wasBeingClicked = true;
                    isBeingClicked = false;
                }
            }
            else if(mousePos.y != 0)
            {
                if(transform.localPosition.y % 2 == 0 && transform.localPosition.y != startPosY) 
                {
                    wasBeingClicked = true;
                    isBeingClicked = false;
                }
            }
        
            
            
        }

    }

    public void ChangeIsBeingClicked(bool change)
    {
        isBeingClicked = change;
    }
    public void ChangeWasBeingClicked(bool change)
    {
        wasBeingClicked = change;
    }
    public bool IsBeingClicked()
    {
        return isBeingClicked;
    }
    public void ChangeLeftMax(float change)
    {
        leftMax = change;
        if(leftMax < tilePuzzleManager.MinPuzzleSize().x)
        {
            leftMax = tilePuzzleManager.MinPuzzleSize().x;
        }
        
        
        // Debug.Log("Dipanggil Left");
    }
    public void ChangeRightMax(float change)
    {
        rightMax = change;
        if(rightMax > tilePuzzleManager.MaxPuzzleSize().x)
        {
            rightMax = tilePuzzleManager.MaxPuzzleSize().x;
        }
        Debug.Log(rightMax + "batas kanan baru" + gameObject);
        // Debug.Log("Dipanggil Right");
    }
    public void ChangeTopMax(float change)
    {
        topMax = change;
        if(topMax > tilePuzzleManager.MinPuzzleSize().y)
        {
            topMax = tilePuzzleManager.MinPuzzleSize().y;
        }
        
        // Debug.Log("Dipanggil Top");
    }
    public void ChangeDownMax(float change)
    {
        downMax = change;
        if(downMax < tilePuzzleManager.MaxPuzzleSize().y)
        {
            downMax = tilePuzzleManager.MaxPuzzleSize().y;
        }
        // Debug.Log("Dipanggil Down");
    }
    public void CheckTileBeside()
    {

        hitObjectLeft = Physics2D.RaycastAll(new Vector2(transform.position.x - 1.1f, transform.position.y), new Vector2(-1,0), 0.9f, (int)tileLayerCollide);
        ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
        foreach(RaycastHit2D hit in hitObjectLeft)
        {
            Debug.Log(hit.collider.gameObject);
            if(hit.collider.gameObject != gameObject)
            {
                ChangeLeftMax(hit.transform.localPosition.x + tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
                
        }


        hitObjectRight = Physics2D.RaycastAll(new Vector2(transform.position.x + 1.1f, transform.position.y), new Vector2(1,0), 0.9f, (int)tileLayerCollide);
        ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
        foreach(RaycastHit2D hit in hitObjectRight)
        {
            if(hit.collider.gameObject != gameObject)
            {
                ChangeRightMax(hit.transform.localPosition.x - tilePuzzleManager.JarakAntarTile());
                Debug.Log(hit.transform.localPosition.x +" " + tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
        }
            
       

        hitObjectTop = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1.1f), new Vector2(0,1), 0.9f, (int)tileLayerCollide);
        ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
        foreach(RaycastHit2D hit in hitObjectTop)
        {
            if(hit.collider.gameObject != gameObject)
            {
                ChangeTopMax(hit.transform.localPosition.y - tilePuzzleManager.JarakAntarTile());
                Debug.Log(hit.collider + "top" + gameObject);
                break;
            }
            ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
        }    

        hitObjectDown = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 1.1f), new Vector2(0,-1), 0.9f, (int)tileLayerCollide);
        ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
        foreach(RaycastHit2D hit in hitObjectDown)
        {
            if(hit.collider.gameObject != gameObject)
            {
                ChangeDownMax(hit.transform.localPosition.y + tilePuzzleManager.JarakAntarTile());
                Debug.Log(hit.collider + "down" + gameObject);
                break;
            }
            ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
        }
                
    }

}
