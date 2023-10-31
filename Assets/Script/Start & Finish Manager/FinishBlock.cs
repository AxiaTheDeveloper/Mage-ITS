using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    [SerializeField]private Collider2D[] collInput;
    [SerializeField]private GameObject[] inputVisual; 
    [SerializeField]private int totalInputNeeded;
    private int totalInput;
    [SerializeField]private GameObject visualOff, visualOn;

    private void Update() 
    {
        if(PuzzleGameManager.Instance.GetStateGame() == PuzzleGameManager.GameState.Start)
        {
            totalInput = 0;
           
            for(int i=0;i<collInput.Length;i++)
            {
                Collider2D[] collidersInside = new Collider2D[5];
                collInput[i].OverlapCollider(new ContactFilter2D(), collidersInside);
                foreach(Collider2D collider in collidersInside)
                {
                    if(collider && collider.gameObject.CompareTag("Output"))
                    {
                        
                        Transform parent = collider.gameObject.GetComponentInParent<Transform>();
                        TilePuzzle tilePuzzleColliderInside = parent.GetComponentInParent<TilePuzzle>();
                        if(tilePuzzleColliderInside && tilePuzzleColliderInside.HasElectricity())
                        {
                            // Debug.Log("Game WIN");
                            totalInput++;
                            inputVisual[i].SetActive(true);
                        }
                        else
                        {
                            inputVisual[i].SetActive(false);
                        }
                        break;
                    }
                            
                }
                if(totalInput == totalInputNeeded)
                {
                    ChangeVisual(true);
                    PuzzleGameManager.Instance.FinishGame();
                    break;
                }
            }
            
        }
        
    }
    public void ChangeVisual(bool finish)
    {
        visualOn.SetActive(finish);
        visualOff.SetActive(!finish);
    }

}
