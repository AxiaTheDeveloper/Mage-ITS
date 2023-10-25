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
    private bool isRotating = false;
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
        PuzzleGameManager.Instance.ChangeIsTileRotating(true);
        Quaternion rotasi_visual = visual.transform.localRotation;
        float rotasiNew = rotasi_visual.eulerAngles.z + rotasi;
        if(rotasiNew == 360)
        {
            rotasiNew = 0;
        }
        isRotating = true;
        LeanTween.rotate(visual, new Vector3(0f,0f,rotasiNew), 0.5f).setOnComplete(
            ()=> Berotasi()
        );
        // visual.transform.rotation = Quaternion.Euler(0f,0f,rotasiNew);
        // Debug.Log(visual.transform.rotation.eulerAngles.z);
        
        // Debug.Log(direction);
    }
    private void Berotasi()
    {
        PuzzleGameManager.Instance.ChangeIsTileRotating(false);
        isRotating = false;
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
    }
    public bool IsRotating()
    {
        return isRotating;
    }
}
