using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance{get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
    public bool GetMouse0InputDown()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool GetMouse0InputUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    public bool GetMouse1InputDown()
    {
        return Input.GetMouseButtonDown(1);
    }
}
