using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWorld : UIAbstract
{
    [SerializeField] Button equipBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] Button ticketBtn;
    [SerializeField] TextMeshProUGUI priceTxt;
    ShopState state;
    int currentID;
    public override void Open()
    {
        currentID = (int)DataManager.Instance.GetLastSky();
        ChangeSky();
        base.Open();
    }
    public void Back()
    {
        UIManager.Instance.MainMenu();
    }
    public void Next()
    {
        currentID++;
        if(currentID>=DataManager.Instance.SkyLength)
        {
            currentID= 0;
        }
        ChangeSky();

    }
    public void Prev()
    {
        currentID--;
        if(currentID<0)
        {
            currentID = DataManager.Instance.SkyLength -1;
        }
        ChangeSky();
    }
    void ChangeSky()
    {
        Material mat = DataManager.Instance.GetSky((SkyType)currentID).material;
        RenderSettings.skybox = mat;
        ChangeButtonState();

    }    
    public void Select()
    {
        DataManager.Instance.SetLastSky((SkyType)currentID);
        Back();
    }
    public void Buy()
    {
        SkyBox sky = DataManager.Instance.GetSky((SkyType)currentID);
        int price = sky.price;
        if (DataManager.PlayerCoin >= price)
        {
            DataManager.PlayerCoin -= price;
            DataManager.Instance.SetSkyState((SkyType)currentID, ShopState.Bought);
            ChangeButtonState();
        }
    }
    public void UseTicket()
    {

    }
    void ChangeButtonState()
    {
        SkyBox sky = DataManager.Instance.GetSky((SkyType)currentID);
        state = DataManager.Instance.GetSkyState((SkyType)currentID);

        int price = sky.price;
        equipBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        ticketBtn.gameObject.SetActive(false);

        switch (state)
        {
            case ShopState.UnBought:
                {
                    buyBtn.gameObject.SetActive(true);
                    buyBtn.image.color = DataManager.PlayerCoin >= price ? Color.green : Color.gray;

                    priceTxt.text = price.ToString();
                    ticketBtn.gameObject.SetActive(true);
                    break;
                }
            
            case ShopState.Bought:
                {
                    equipBtn.gameObject.SetActive(true);
                    break;
                }
        }
    }
}
public enum ShopState
{
    UnBought,
    
    Bought,


}
