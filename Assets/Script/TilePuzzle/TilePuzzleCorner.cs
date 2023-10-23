using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleCorner : TilePuzzle
{
    public enum DirectionCornerPuzzle
    {
        LB, LU, RU, RB
    }
    [SerializeField]private DirectionCornerPuzzle direction;
    [SerializeField]private float rotationLU, rotationRU, rotationRB;
    private void Awake() 
    {
        visual = transform.GetChild(0).gameObject;
        if(direction == DirectionCornerPuzzle.LU)
        {
            RotateVisual(rotationLU);
        }
        else if(direction == DirectionCornerPuzzle.RU)
        {
            RotateVisual(rotationRU);
        }
        else if(direction == DirectionCornerPuzzle.RB)
        {
            RotateVisual(rotationRB);
        }
    }
    public void RotateVisual(float rotasi)
    {
        Quaternion rotasi_visual = visual.transform.localRotation;
        float rotasiNew = rotasi_visual.eulerAngles.z + rotasi;
        if(rotasiNew == 360)
        {
            rotasiNew = 0;
        }
        visual.transform.rotation = Quaternion.Euler(0f,0f,rotasiNew);
        // Debug.Log(visual.transform.rotation.eulerAngles.z);
        if(visual.transform.rotation.eulerAngles.z == rotationLU)
        {
            direction = DirectionCornerPuzzle.LU;
        }
        else if(visual.transform.rotation.eulerAngles.z == rotationRU)
        {
            direction = DirectionCornerPuzzle.RU;
        }
        else if(visual.transform.rotation.eulerAngles.z == rotationRB)
        {
            direction = DirectionCornerPuzzle.RB;
        }
        else if(visual.transform.rotation.eulerAngles.z == 0)
        {
            direction = DirectionCornerPuzzle.LB;
        }
        // Debug.Log(direction);
    }
}
