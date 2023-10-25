using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleStraight : TilePuzzle
{
    public enum DirectionStraightPuzzle
    {
        Horizontal, Vertical
    }
    [SerializeField]private DirectionStraightPuzzle direction;
    [SerializeField]private float rotationVertical;
    private bool isRotating = false;
    private void Awake() 
    {
        visual = transform.GetChild(0).gameObject;
        if(direction == DirectionStraightPuzzle.Vertical)
        {
            RotateVisual(rotationVertical);
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
        isRotating = true;
        LeanTween.rotate(visual, new Vector3(0f,0f,rotasiNew), 0.5f).setOnComplete(
            ()=> Berotasi()
        );
        
        
        // visual.transform.rotation = Quaternion.Euler(0f,0f,rotasiNew);r
        // Debug.Log(visual.transform.rotation.eulerAngles.z);
        
        // Debug.Log(direction);
    }

    private void Berotasi()
    {
        isRotating = false;
        if(visual.transform.rotation.eulerAngles.z == 90 || visual.transform.rotation.eulerAngles.z == -90 || visual.transform.rotation.eulerAngles.z == 270 )
        {
            direction = DirectionStraightPuzzle.Vertical;
        }
        else
        {
            direction = DirectionStraightPuzzle.Horizontal;
        }
    }
    public bool IsRotating()
    {
        return isRotating;
    }

    
}
