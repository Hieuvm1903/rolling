using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]

public class LevelData : ScriptableObject
{
    public Level[] levels;
    public Level GetLevel(int i)
    {
        i = Mathf.Clamp(i, 0, levels.Length-1);   
        return levels[i];
    }
}
