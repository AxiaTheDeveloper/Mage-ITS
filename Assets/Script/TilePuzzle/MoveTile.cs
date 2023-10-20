using System;
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

    [SerializeField]private List<Collider2D> top,down,left,right;

    [SerializeField]private RaycastHit2D[] hitObjectLeft, hitObjectRight, hitObjectTop, hitObjectDown;
    [SerializeField]private LayerMask tileLayerCollide;

    [SerializeField]private bool canLeft, canRight, canTop, canDown, goHorizontal, goVertical, isFirstTime = true;

    private void Awake() 
    {
        tilePuzzle = GetComponent<TilePuzzle>();
        isMoveAble = tilePuzzle.IsMoveAble();
        tilePuzzleManager = GetComponentInParent<TilePuzzleManager>();
        left = new List<Collider2D>();
        right = new List<Collider2D>();
        top = new List<Collider2D>();
        down = new List<Collider2D>();

    }
    private void Start() 
    {
        gameInput = GameInput.Instance;

        startPosX = transform.position.x;
        startPosY = transform.position.y;


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

            
            
            if(goHorizontal)
            {
                if(transform.localPosition.x % 2 != 0)
                {
                    if(transform.localPosition.x % 2 >= 1)
                    {
                        // Debug.Log(Mathf.Floor(transform.localPosition.x) + 1);
                        transform.localPosition = new Vector2(Mathf.Floor(transform.localPosition.x) + 1, transform.localPosition.y);
                        
                    }
                    else
                    {   
                        // Debug.Log(Mathf.Floor(transform.localPosition.x));
                        transform.localPosition = new Vector2(Mathf.Floor(transform.localPosition.x), transform.localPosition.y);
                        
                    }
                }
            }
            else if(goVertical)
            {
                if(Mathf.Abs(transform.localPosition.y) % 2 != 0)
                {
                    if(Mathf.Abs(transform.localPosition.y) % 2 >= 1)
                    {
                        // Debug.Log(transform.localPosition.y + "atas");
                        transform.localPosition = new Vector2(transform.localPosition.x, -1 * Mathf.Floor(Mathf.Abs(transform.localPosition.y)) + 1);
                    }
                    else
                    {
                        // Debug.Log(transform.localPosition.y + "bwh");
                        // Debug.Log(Mathf.Floor(transform.localPosition.y) + "bawah");
                        transform.localPosition = new Vector2(transform.localPosition.x, -1* Mathf.Floor(Mathf.Abs(transform.localPosition.y)));
                    }
                }
                
            }
            
            goHorizontal = false;
            goVertical = false;

            float oldStartPosX = startPosX;
            float oldStartPosY = startPosY;
            startPosX = transform.position.x;
            startPosY = transform.position.y;

            if(!isFirstTime)
            {
                if(oldStartPosX != startPosX)
                {
                    foreach(RaycastHit2D hit in hitObjectLeft)
                    {
                        if(hit.collider.gameObject != gameObject)
                        {
                            Debug.Log(oldStartPosX + "ubah kanan");
                            // hit.collider.gameObject.GetComponent<MoveTile>().ChangeRightMax(oldStartPosX);
                            hit.collider.gameObject.GetComponent<MoveTile>().CheckTileRightNormal();
                            
                        }
                           
                    }
                    foreach(RaycastHit2D hit in hitObjectRight)
                    {
                        if(hit.collider.gameObject != gameObject)
                        {
                            Debug.Log(oldStartPosX + "ubah kiri");
                            // hit.collider.gameObject.GetComponent<MoveTile>().ChangeLeftMax(oldStartPosX);
                            hit.collider.gameObject.GetComponent<MoveTile>().CheckTileLeftNormal();
                            // canRight = false;
                            
                        }
                    }
                }
                if(oldStartPosY != startPosY)
                {
                    foreach(RaycastHit2D hit in hitObjectTop)
                    {
                        Debug.Log(hit.collider + "atas");
                        if(hit.collider.gameObject != gameObject)
                        {
                            Debug.Log(oldStartPosY + "ubah bawah");
                            // hit.collider.gameObject.GetComponent<MoveTile>().ChangeDownMax(oldStartPosY);
                            hit.collider.gameObject.GetComponent<MoveTile>().CheckTileDownNormal();
                            // canTop = false;
                            
                        }

                    }    
                    foreach(RaycastHit2D hit in hitObjectDown)
                    {
                        if(hit.collider.gameObject != gameObject)
                        {
                            Debug.Log(oldStartPosY + "ubah atas");
                            // hit.collider.gameObject.GetComponent<MoveTile>().ChangeTopMax(oldStartPosY);
                            hit.collider.gameObject.GetComponent<MoveTile>().CheckTileTopNormal();
                            // canDown = false;
                            
                        }

                    }
                }
                
                CheckTileBeside();
            }
            else
            {
                CheckTileBesideNormal();
                isFirstTime = false;
            }
            
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
            
            // Debug.Log(mousePos + " " + transform.localPosition);
            // Debug.Log((mousePos.y - transform.localPosition.y) + "DAN" + (mousePos.x - transform.localPosition.x));
            if(goHorizontal || goVertical)
            {
                if(mousePos.x == startPosX && mousePos.y == startPosY)
                {
                    goHorizontal = false;
                    goVertical = false;
                }
                if(goHorizontal)
                {
                    mousePos.y = startPosY;
                    
                }
                if(goVertical)
                {
                    mousePos.x = startPosX;
                }
            }
            else if(!goHorizontal && !goVertical)
            {
                if(Mathf.Abs(mousePos.y - transform.localPosition.y) > Mathf.Abs(mousePos.x - transform.localPosition.x))
                {
                    
                    // Debug.Log(mousePos + " x 0");
                    goHorizontal = false;
                    goVertical = true;
                    mousePos.x = startPosX;
                    // Debug.Log(mousePos);
                }
                else
                {
                    // Debug.Log(mousePos + " y 0");
                    mousePos.y = startPosY;
                    goHorizontal = true;
                    goVertical = false;
                }
            }
            
            
            if(goVertical)
            {
                // if(canTop)
                // {
                    if(mousePos.y > topMax)
                    {
                        mousePos.y = topMax;   
                    }
                // }
                // if(canDown)
                // {
                    else if(mousePos.y < downMax)
                    {
                        mousePos.y = downMax; 
                    }
                // }
                
            }
            if(goHorizontal)
            {
                // if(canLeft)
                // {
                    if(mousePos.x < leftMax)
                    {
                        // Debug.Log("melebihi dari leftmax");
                        mousePos.x = leftMax;   
                    }
                // }
                // if(canRight)
                // {
                    else if(mousePos.x > rightMax)
                    {
                        // Debug.Log("melebihi dari rightmax");
                        mousePos.x = rightMax; 
                    }
                // }
                
            }
            
            

            //kalo downmax gitu jdnya harus di abs dulu kalo downmaxnya minus, intinya kalo dr pastinya minus trus hsl setelahnya lebih besar????????
            
            
            // Debug.Log(mousePos);
            

            // Debug.Log(mousePos);
            // if(mousePos.x == 6)   Debug.Log(mousePos);
            
            
            // Vector3 localDirection = transform.InverseTransformDirection(mousePos - transform.localPosition);

            // transform.Translate(localDirection * Time.deltaTime, Space.Self);
            transform.localPosition = mousePos;

            
            
            if(goHorizontal)
            {
                if(transform.localPosition.x % 2 == 0 && transform.localPosition.x != startPosX) 
                {
                    wasBeingClicked = true;
                    isBeingClicked = false;
                }
            }
            else if(goVertical)
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
        // Debug.Log(rightMax + "batas kanan baru" + gameObject);
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
        left.Clear();
        ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
        // canLeft = true;
        foreach(RaycastHit2D hit in hitObjectLeft)
        {
            if(hit.collider.gameObject != gameObject)
            {
                left.Add(hit.collider);
                ChangeLeftMax(hit.transform.localPosition.x + tilePuzzleManager.JarakAntarTile());
                // hit.collider.gameObject.GetComponent<MoveTile>().ChangeRightMax(startPosX - tilePuzzleManager.JarakAntarTile());
                hit.collider.gameObject.GetComponent<MoveTile>().CheckTileRightNormal();
                break;
            }
            ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
            // canLeft = true;
                
        }

        hitObjectRight = Physics2D.RaycastAll(new Vector2(transform.position.x + 1.1f, transform.position.y), new Vector2(1,0), 0.9f, (int)tileLayerCollide);
        right.Clear();
        ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
        // canRight = true;
        foreach(RaycastHit2D hit in hitObjectRight)
        {
            if(hit.collider.gameObject != gameObject)
            {
                right.Add(hit.collider);
                ChangeRightMax(hit.transform.localPosition.x - tilePuzzleManager.JarakAntarTile());
                // Debug.Log(hit.transform.localPosition.x +" " + tilePuzzleManager.JarakAntarTile());
                // hit.collider.gameObject.GetComponent<MoveTile>().ChangeLeftMax(startPosX + tilePuzzleManager.JarakAntarTile());
                hit.collider.gameObject.GetComponent<MoveTile>().CheckTileLeftNormal();
                // canRight = false;
                break;
            }
            ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
            // canRight = true;
        }
            
        hitObjectTop = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1.1f), new Vector2(0,1), 0.9f, (int)tileLayerCollide);
        top.Clear();
        ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
        // canTop = true;
        foreach(RaycastHit2D hit in hitObjectTop)
        {

            if(hit.collider.gameObject != gameObject)
            {
                top.Add(hit.collider);
                ChangeTopMax(hit.transform.localPosition.y - tilePuzzleManager.JarakAntarTile());
                // Debug.Log(hit.collider + "top" + gameObject);
                // hit.collider.gameObject.GetComponent<MoveTile>().ChangeDownMax(startPosY + tilePuzzleManager.JarakAntarTile());
                hit.collider.gameObject.GetComponent<MoveTile>().CheckTileDownNormal();
                // canTop = false;
                break;
            }
            ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
            // canTop = true;
        }    

        hitObjectDown = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 1.1f), new Vector2(0,-1), 0.9f, (int)tileLayerCollide);
        down.Clear();
        ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
        // canDown = true;
        foreach(RaycastHit2D hit in hitObjectDown)
        {
            if(hit.collider.gameObject != gameObject)
            {
                down.Add(hit.collider);
                ChangeDownMax(hit.transform.localPosition.y + tilePuzzleManager.JarakAntarTile());
                // Debug.Log(hit.collider + "down" + gameObject);
                // hit.collider.gameObject.GetComponent<MoveTile>().ChangeTopMax(startPosY - tilePuzzleManager.JarakAntarTile());
                hit.collider.gameObject.GetComponent<MoveTile>().CheckTileTopNormal();
                // canDown = false;
                break;
            }
            ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
            // canDown = true;
        }
                
    }
    public void CheckTileLeftNormal()
    {
        hitObjectLeft = Physics2D.RaycastAll(new Vector2(transform.position.x - 1.1f, transform.position.y), new Vector2(-1,0), 0.9f, (int)tileLayerCollide);
        left.Clear();
        ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
        // canLeft = true;
        foreach(RaycastHit2D hit in hitObjectLeft)
        {
            if(hit.collider.gameObject != gameObject)
            {
                left.Add(hit.collider);
                ChangeLeftMax(hit.transform.localPosition.x + tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeLeftMax(startPosX - tilePuzzleManager.JarakAntarTile());
            // canLeft = true;
                
        }  
    }
    public void CheckTileRightNormal()
    {
        hitObjectRight = Physics2D.RaycastAll(new Vector2(transform.position.x + 1.1f, transform.position.y), new Vector2(1,0), 0.9f, (int)tileLayerCollide);
        right.Clear();
        ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
        // canRight = true;
        foreach(RaycastHit2D hit in hitObjectRight)
        {
            if(hit.collider.gameObject != gameObject)
            {
                right.Add(hit.collider);
                ChangeRightMax(hit.transform.localPosition.x - tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeRightMax(startPosX + tilePuzzleManager.JarakAntarTile());
            // canRight = true;
        }  
    }
    public void CheckTileTopNormal()
    {
        hitObjectTop = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1.1f), new Vector2(0,1), 0.9f, (int)tileLayerCollide);
        top.Clear();
        ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
        // canTop = true;
        foreach(RaycastHit2D hit in hitObjectTop)
        {

            if(hit.collider.gameObject != gameObject)
            {
                top.Add(hit.collider);
                ChangeTopMax(hit.transform.localPosition.y - tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeTopMax(startPosY + tilePuzzleManager.JarakAntarTile());
            // canTop = true;
        }    
    }
    public void CheckTileDownNormal()
    {
        hitObjectDown = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 1.1f), new Vector2(0,-1), 0.9f, (int)tileLayerCollide);
        down.Clear();
        ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
        // canDown = true;
        foreach(RaycastHit2D hit in hitObjectDown)
        {
            if(hit.collider.gameObject != gameObject)
            {
                down.Add(hit.collider);
                ChangeDownMax(hit.transform.localPosition.y + tilePuzzleManager.JarakAntarTile());
                break;
            }
            ChangeDownMax(startPosY - tilePuzzleManager.JarakAntarTile());
            // canDown = true;
        }
                
    }

    public void GetTilePuzzleManager(TilePuzzleManager manager)
    {
        tilePuzzleManager = manager;
        // tilePuzzleManager.OnFinishSpawnPuzzle += tilePuzzleManager_OnFinishSpawnPuzzle;
    }

    //yg normal ini cuma cek sendiri sendiri aja + semua
    private void CheckTileBesideNormal()
    {
        CheckTileLeftNormal();
        CheckTileRightNormal();
        CheckTileDownNormal();
        CheckTileTopNormal();
    }
}
