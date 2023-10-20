using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBall : UIAbstract
{
    [SerializeField] Button equipBtn;
    [SerializeField] Button buyBtn;
    [SerializeField] Button ticketBtn;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Transform tf;
    GameObject currentSkin;
    ShopState state;
    int currentID;

    public override void Open()
    {
        currentID = (int)DataManager.Instance.GetLastSkin();
        Transform tf1 = GameManager.Instance.SkinBall();
        tf1.parent.gameObject.SetActive(false);
        tf.position = tf1.position;
        tf.rotation = tf1.parent.rotation;
        tf.localScale = tf1.root.localScale;
        base.Open();
        tf.DORotate(Vector3.up * -720, 30, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        tf.DOMoveY(tf.position.y + 0.5f, 5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //ChangeBall();
        SkinData skin = DataManager.Instance.GetSkin((SkinType)currentID);
        currentSkin = Instantiate(skin.skinTire, tf.position, tf.rotation, tf);
        if(skin.skinHead)
        {
            Instantiate(skin.skinHead, currentSkin.transform.position, currentSkin.transform.rotation, currentSkin.transform);
        }
        ChangeButtonState();
    }
    public override void Close()
    {
        base.Close();
        tf.DOKill();
        currentSkin.transform.DOKill();
    }
    public void Back()
    {
        UIManager.Instance.MainMenu();
        Destroy(currentSkin);
    }
    public void Next()
    {
        currentID++;
        if (currentID >= DataManager.Instance.SkinLength)
        {
            currentID = 0;
        }
        ChangeBall(true);
        

    }
    public void Select()
    {
        DataManager.Instance.SetLastSkin((SkinType)currentID);
        Back();

    }
    public void Buy()
    {
        SkinData skin = DataManager.Instance.GetSkin((SkinType)currentID);
        int price= skin.price;
        if(DataManager.PlayerCoin>=price)
        {
            DataManager.PlayerCoin -= price;
            DataManager.Instance.SetSkinState((SkinType)currentID, ShopState.Bought);
            ChangeButtonState();
        }

    }
    public void Purchase()
    {

    }
    public void Prev()
    {
        currentID--;
        if (currentID < 0)
        {
            currentID = DataManager.Instance.SkinLength - 1;
        }
       ChangeBall(false);
    }
    void ChangeButtonState()
    {
        SkinData skin = DataManager.Instance.GetSkin((SkinType)currentID);
        state = DataManager.Instance.GetSkinState((SkinType)currentID);
        equipBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        ticketBtn.gameObject.SetActive(false);
        
        switch (state)
        {
            case ShopState.UnBought:
                {
                    buyBtn.gameObject.SetActive(true);

                    buyBtn.image.color = DataManager.PlayerCoin>=skin.price? Color.green:Color.gray;
                    priceText.text = skin.price.ToString();
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
    void ChangeBall(bool isNext)
    {
        SkinData skin = DataManager.Instance.GetSkin((SkinType)currentID);
        Vector3 v = isNext?Vector3.back:Vector3.forward;
        
        

        if (currentSkin)
        {
            GameObject dGO = currentSkin;
            dGO.transform.DOMove(tf.position + v * 5f, 0.2f).OnComplete(
                () =>
                {
                    dGO.transform.DOKill();
                    Destroy(dGO);
                    
                });
            
        }
        currentSkin = Instantiate(skin.skinTire, tf.position + v * -5f, tf.rotation, tf);
        if(skin.skinHead)
        {
            Instantiate(skin.skinHead,currentSkin.transform.position,currentSkin.transform.rotation,currentSkin.transform);
        }
        currentSkin.transform.DOMove(tf.position, 0.21f);
        //currentSkin.SetActive(true);

        ChangeButtonState();
    }




}
