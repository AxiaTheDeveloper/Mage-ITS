using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelMaxMoveScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class MaxMove
    {
        public int[] maxMove = new int[3];
    }
    public MaxMove[] MaxMovePerLevel;
    
}
