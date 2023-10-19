using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColliderCheckerNew : MonoBehaviour
{
    private bool thereSomething = false, firsTimeStart = true;
    private GameObject saveGameObject1, saveGameObject2;
    [SerializeField]private TilePuzzleManager tilePuzzleManager;
    [SerializeField]private MoveTile moveTile;
    [SerializeField]private LayerMask layerClickAble;
    private Collider2D[] colliders;
    [SerializeField]private Vector2 position, size;

    public enum ColliderDirection
    {
        Left, Right, Top, Down
    }
    [SerializeField]private ColliderDirection colliderDirection;

    private void Awake() 
    {
        moveTile = GetComponentInParent<MoveTile>();
    }
    private void Start() 
    {
        tilePuzzleManager = TilePuzzleManager.Instance;
        if(colliderDirection == ColliderDirection.Left)position = new Vector2(transform.localPosition.x - size.x, transform.localPosition.y);
        else if(colliderDirection == ColliderDirection.Right)position = new Vector2(transform.localPosition.x + size.x, transform.localPosition.y);
        else if(colliderDirection == ColliderDirection.Top)position = new Vector2(transform.localPosition.x, transform.localPosition.y + size.x);
        else if(colliderDirection == ColliderDirection.Down)position = new Vector2(transform.localPosition.x, transform.localPosition.y - size.x);

        ErrorLog();
        
    }
    private void ErrorLog()
    {
        if(!moveTile)Debug.LogError("Move tile belum ada di " + gameObject);
    }
    private void Update() {
        // colliders = Physics2D.OverlapBoxAll(position, size, 0, layerClickAble);
        // if(colliderDirection == ColliderDirection.Left)
        // {
        //     foreach(Collider2D hit in colliders)
        //     {
        //         if(hit.gameObject != gameObject)Debug.Log("Left" + hit.gameObject + "from" + gameObject);
        //     }
        //     if(colliders.Length > 1)
        //     {
        //         moveTile.ChangeLeftMax(moveTile.transform.position.x);
        //     }
        //     else
        //     {
        //         moveTile.ChangeLeftMax(moveTile.transform.position.x - tilePuzzleManager.JarakAntarTile());
        //     }
        // }
        // else if(colliderDirection == ColliderDirection.Right)
        // {
        //     foreach(Collider2D hit in colliders)
        //     {
        //         if(hit.gameObject != gameObject)Debug.Log("Right" +hit.gameObject + "from" + gameObject);
        //     }
        //     if(colliders.Length > 1)
        //     {
        //         moveTile.ChangeRightMax(moveTile.transform.position.x);
        //     }
        //     else
        //     {
        //         moveTile.ChangeRightMax(moveTile.transform.position.x + tilePuzzleManager.JarakAntarTile());
        //     }
        // }
        // else if(colliderDirection == ColliderDirection.Top)
        // {
        //     foreach(Collider2D hit in colliders)
        //     {
        //         if(hit.gameObject != gameObject)Debug.Log("Top" + hit.gameObject + "from" + gameObject);
        //     }
        // }
        // else if(colliderDirection == ColliderDirection.Down)
        // {
        //     foreach(Collider2D hit in colliders)
        //     {
        //         if(hit.gameObject != gameObject)Debug.Log("Down" + hit.gameObject + "from" + gameObject);
        //     }
        // }
        
    
        

        // colliderTop = Physics2D.OverlapBoxAll(positionTop, size, 0, layerClickAble);
        // foreach(Collider2D hit in colliderTop)
        // {
        //     
        // }
        // if(colliderLeft.Length != 0)
        // {
        //     moveTile.ChangeTopMax(moveTile.transform.position.y);
        // }
        // else
        // {
        //     moveTile.ChangeTopMax(moveTile.transform.position.y + tilePuzzleManager.JarakAntarTile());
        // }

        // colliderDown = Physics2D.OverlapBoxAll(positionDown, size, 0, layerClickAble);
        // foreach(Collider2D hit in colliderDown)
        // {
        //     Debug.Log("Down" + hit.gameObject);
        // }
        // if(colliderLeft.Length != 0)
        // {
        //     moveTile.ChangeDownMax(moveTile.transform.position.y);
        // }
        // else
        // {
        //     moveTile.ChangeDownMax(moveTile.transform.position.y - tilePuzzleManager.JarakAntarTile());
        // }

        if(saveGameObject1)
        {
            if(colliderDirection == ColliderDirection.Left)
            {

                moveTile.ChangeLeftMax(saveGameObject1.transform.position.x + tilePuzzleManager.JarakAntarTile());
            }
            else if(colliderDirection == ColliderDirection.Right)
            {
                moveTile.ChangeRightMax(saveGameObject1.transform.position.x - tilePuzzleManager.JarakAntarTile());
                
            }
        }
        else
        {
            Debug.Log("ke sini");
            if(colliderDirection == ColliderDirection.Left)
            {
                moveTile.ChangeLeftMax(moveTile.transform.position.x - tilePuzzleManager.JarakAntarTile());
            }
            else if(colliderDirection == ColliderDirection.Right)
            {
                moveTile.ChangeRightMax(moveTile.transform.position.x + tilePuzzleManager.JarakAntarTile());
            }
        }
        
    }
}
