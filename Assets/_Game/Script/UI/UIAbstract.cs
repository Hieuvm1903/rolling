using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIAbstract : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI coinTxt;
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public virtual void SetCoinText()
    {
        coinTxt.text = DataManager.PlayerCoin.ToString();
    }
}
