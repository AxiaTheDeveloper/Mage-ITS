using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSaveScriptableObject : ScriptableObject
{
    public int[] levelScore;
    public bool isPlayerRestartLevel;
}
