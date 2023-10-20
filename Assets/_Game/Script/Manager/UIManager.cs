using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] UIGamePlay gamePlay;
    [SerializeField] UIPause pause;
    [SerializeField] UIMainMenu menu;
    [SerializeField] UIWorld world;
    [SerializeField] UIBall ball;
    [SerializeField] UIWin win;
    [SerializeField] UILose lose;
    [SerializeField] UISetting setting;

    UIAbstract current;
    void Start()
    {
        current = menu;
        SetCoin();
    }

    void OpenUI(UIAbstract ui)
    {
        current?.Close();
        current= ui;
        current.Open();
        SetCoin();
    }
    public void Pause()
    {
        OpenUI(pause);
    }
    public void Continue()
    {
        OpenUI(gamePlay);
    }
    public void MainMenu()
    {
        OpenUI(menu);
        CamerManager.Instance.OnInit();

    }
    public void OpenSetting()
    {
        OpenUI(setting);
    }
    public void OpenWin()
    {
        OpenUI(win);
    }    
    public void OpenLose()
    {
        OpenUI(lose);
    }    
    public void OpenWorld()
    {
        OpenUI(world);
    }   
    public void OpenBall()
    {
        OpenUI(ball);
    }    
    public void SetCoin()
    {
        current.SetCoinText();
    }
    public Action<bool> liveLoss;
    public void OnLivesChange(bool isAdd)
    {       
            liveLoss?.Invoke(isAdd);        
    }


}
