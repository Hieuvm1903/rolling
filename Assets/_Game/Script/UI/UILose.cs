using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UIAbstract
{
    public override void Open()
    {

        base.Open();
    }
    public void Agree()
    {
        int i = DataManager.Ticket;
        if (i > 0)
        {
            DataManager.Ticket--;
            DataManager.BallLives = 5;
            GameManager.Instance.Continue();
            UIManager.Instance.Continue();
        }
        else
        {
            //ShowAds;
        }
    }   
    public void Deny()
    {
        UIManager.Instance.MainMenu();
    }    
}
