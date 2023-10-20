using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkyBox", menuName = "ScriptableObjects/SKyBox", order = 1)]

public class SkyBoxData : ScriptableObject
{
    public SkyBox[] skyBoxes;
    public int Length { get { return skyBoxes.Length; } }
    public SkyBox GetSky(SkyType type)
    {
        foreach(var sky in skyBoxes)
        {
            if( sky.skyType == type)
            { return sky; }
        }
        return skyBoxes[0];
    }    
}
[System.Serializable]
public class SkyBox
{
    public int price;
    public Material material;
    public SkyType skyType;



}
public enum SkyType
{
    city,
    bright,
    cloud,
    desert,
    landscape,
    mountain,
    northpole


}
