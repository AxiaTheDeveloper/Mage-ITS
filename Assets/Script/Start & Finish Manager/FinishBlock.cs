using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    public bool GotInputElectricity(Collider2D colliderGotInput)
    {
        Debug.Log("Game WIN");
        PuzzleGameManager.Instance.FinishGame();
        return true;
    }
}
