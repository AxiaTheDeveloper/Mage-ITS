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
        visual.transform.rotation = Quaternion.Euler(0f,0f,rotasi_visual.eulerAngles.z + rotasi);
        if(visual.transform.rotation.eulerAngles.z == 90 || visual.transform.rotation.eulerAngles.z == -90)
        {
            direction = DirectionStraightPuzzle.Vertical;
        }
        else
        {
            direction = DirectionStraightPuzzle.Horizontal;
            //kasih jg d corner, trus ntr baru bikin checker buat slsain game
        }
    }

    
}
