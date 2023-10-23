using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBlock : MonoBehaviour
{
    public void GotInputElectricity(Collider2D colliderGotInput)
    {
        Debug.Log("Game WIN");
    }
}
