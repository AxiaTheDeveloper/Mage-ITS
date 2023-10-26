using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    [SerializeField]private Collider2D collInput;

    private void Update() 
    {
        if(PuzzleGameManager.Instance.GetStateGame() == PuzzleGameManager.GameState.Start)
        {
            Collider2D[] collidersInside = new Collider2D[5];
            collInput.OverlapCollider(new ContactFilter2D(), collidersInside);
            foreach(Collider2D collider in collidersInside)
            {
                if(collider && collider.gameObject.CompareTag("Output"))
                {
                    Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                    TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                    if(tilePuzzleColliderInside && tilePuzzleColliderInside.HasElectricity())
                    {
                        Debug.Log("Game WIN");
                        PlayerSaveManager.Instance.CalculateScore();
                        PuzzleGameManager.Instance.FinishGame();
                        
                    }
                }
                        
            }
        }
        
    }
}
