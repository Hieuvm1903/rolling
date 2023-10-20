using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : UIAbstract
{
    public void Continue()
    {
        GameManager.Instance.Continue();
        UIManager.Instance.Continue();
    }
    public void Quit()
    {
        GameManager.Instance.OnInit();
        GameManager.ChangeState(GameState.MainMenu);

        UIManager.Instance.MainMenu();

    }
}
