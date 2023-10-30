using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerSaveScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class LevelIdentity
    {
        public int levelScore;
        public bool levelDone;
        public bool levelUnlocked;
    }
    public LevelIdentity[] levelIdentities;
    public bool isPlayerRestartLevel;
}
