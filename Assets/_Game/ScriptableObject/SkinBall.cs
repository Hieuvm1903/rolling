using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkinBall", menuName = "ScriptableObjects/SkinBall", order = 1)]

public class SkinBall : ScriptableObject
{
    public SkinData[] skinDatas;
    public SkinData GetSkin(SkinType type)
    {
        foreach (var skin in skinDatas)
        {
            if (skin.type == type)
            { return skin; }
        }
        return skinDatas[0];
    }    
    public int Length { get { return skinDatas.Length; } }
}
[System.Serializable]
public class SkinData
{
    public SkinType type;
    public int price;
    public GameObject skinTire;
    public GameObject skinHead;
}
public enum SkinType
{
    basic,
    energy,
    flash,
    robot,
    spiral,
    police,
    planet,
    soccer,
    tennis,
    pufferfish,
    gamer,
    cosmo,
    pig,
    squidgame,
    basketball,
    pumpkin,
    dragonball,
    chaos,


}
