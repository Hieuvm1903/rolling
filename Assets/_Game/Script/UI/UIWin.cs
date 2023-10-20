using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWin : UIAbstract
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI normalCoin;
    // Start is called before the first frame update
    public static int coin = 100;
    int multiple;

    public override void Open()
    {
        base.Open();
        anim.enabled = true;
        normalCoin.text = coin.ToString();
    }
    public void ChangeNumber(int i)
    {
        text.text = (i*coin).ToString();
        multiple = i;
    }
    public void GetCoinAds()
    {
        DataManager.Instance.AddCoin(coin * multiple);
        Close(0.5f);
    }
    public void GetCoin()
    {
        DataManager.Instance.AddCoin(coin);
        Close(0.5f);
    }
    void Close(float time)
    {
        Invoke(nameof(OpenMain), time);
    }
    void OpenMain()
    {
        UIManager.Instance.MainMenu();
    }


}
