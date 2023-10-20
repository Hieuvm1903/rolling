using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{
    GameObject skin;
    bool isGot;
    string dataName;
    private void Start()
    {
        dataName = name + transform.parent.name + transform.root.name;
        isGot = DataManager.Instance.LiveGot(dataName) == 0;
        if (isGot)
        {

            GameObject go = DataManager.Instance.GetSkin(DataManager.Instance.GetLastSkin()).skinTire;
            skin = Instantiate(go, transform.position, transform.rotation, transform);
            skin.transform.DORotate(Vector3.up * 360, 3f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

        }

    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(isGot)
        {
            
            GameObject go = DataManager.Instance.GetSkin(DataManager.Instance.GetLastSkin()).skinTire;
            skin = Instantiate(go, transform.position, transform.rotation, transform);
            skin.transform.DORotate(Vector3.up * 360, 3f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            if(DataManager.BallLives<5)
            {
                DataManager.BallLives++;
                UIManager.Instance.OnLivesChange(true);
            }
            DataManager.Instance.SetLive(dataName);
            gameObject.SetActive(false);


        }
    }
    private void OnDestroy()
    {
        if(skin)
        skin.transform.DOKill();
    }
}
