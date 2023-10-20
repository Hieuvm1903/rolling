using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    public SkinBall skinBall;
    public int SkinLength { get { return skinBall.Length; } }
    public SkyBoxData dataSky;
    public int SkyLength { get { return dataSky.Length; } }

    public static int PlayerCoin
    {
        get { return PlayerPrefs.GetInt("Coin", 0); }
        set {
            PlayerPrefs.SetInt("Coin", value);
            UIManager.Instance.SetCoin();
                ; }
    }    

    public static int LevelPassed
    {
        get { return PlayerPrefs.GetInt("Level", 0); }
        set { PlayerPrefs.SetInt("Level", value); }
    }
    public static int BallLives
    {
        get { return PlayerPrefs.GetInt("Lives", 5); }
        set { ;
            PlayerPrefs.SetInt("Lives", value);
        }
    }

    public static int Ticket
    {
        get { return PlayerPrefs.GetInt("tickets", 10); }
        set { PlayerPrefs.SetInt("tickets", value); 
        
        }

    }
    public static int IsMusic { get { return PlayerPrefs.GetInt("music", 0); }
        set { PlayerPrefs.SetInt("music", value);
            SoundManager.Instance.MusicOn(value == 0);
        }
    }
    public static int IsSound
    {
        get { return PlayerPrefs.GetInt("sound", 0); }
        set { PlayerPrefs.SetInt("sound", value);
            SoundManager.Instance.SoundOn(value == 0);
        }
    }
    public static int IsHaptic
    {
        get { return PlayerPrefs.GetInt("haptic", 0); }
        set { PlayerPrefs.SetInt("haptic", value); 
        SoundManager.Instance.HapticOn(value == 0);
        }
    }

    public void AddCoin(int coin)
    {
        PlayerCoin+=coin;
    }
    public int PlayerLives
    {
        get { return PlayerPrefs.GetInt("Live", 0); }
        set { PlayerPrefs.SetInt("Live", value); }
    }
    private void Start()
    {
        SetSkinState(GetLastSkin(), ShopState.Bought);
        SetSkyState(GetLastSky(),ShopState.Bought);
    }

    public ShopState GetSkinState(SkinType type)
    {
        return (ShopState)PlayerPrefs.GetInt("_ball"+type.ToString(), 0);
    }
    public void SetSkinState(SkinType type,ShopState state) 
    {
        PlayerPrefs.SetInt("_ball"+type.ToString(),(int)state);
    }
    public SkinType GetLastSkin()
    {
        return (SkinType)PlayerPrefs.GetInt("last_skin", 0);
    }
    public void SetLastSkin(SkinType type)
    {
        PlayerPrefs.SetInt("last_skin", (int)type);
    }
    public SkinData GetSkin(SkinType type)
    {
        return skinBall.GetSkin(type);
    }

    public ShopState GetSkyState(SkyType type)
    {
        return (ShopState)PlayerPrefs.GetInt("_sky" + type.ToString(), 0);
    }
    public void SetSkyState(SkyType type, ShopState state)
    {
        PlayerPrefs.SetInt("_sky" + type.ToString(), (int)state);
    }
    public SkyType GetLastSky()
    {
        return (SkyType)PlayerPrefs.GetInt("last_sky", 0);
    }
    public void SetLastSky(SkyType type)
    {
        PlayerPrefs.SetInt("last_sky", (int)type);
    }
    public SkyBox GetSky(SkyType type)
    {
        return dataSky.GetSky(type);
    }
    public int LiveGot(string name)
    {
        return PlayerPrefs.GetInt(name, 0);
    }
    public void SetLive(string name)
    {
        PlayerPrefs.SetInt(name, 1);
    }









}
