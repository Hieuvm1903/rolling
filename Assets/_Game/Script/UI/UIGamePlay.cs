using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UIAbstract
{
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] List<GameObject> images;
    [SerializeField] List<GameObject> lossImages;
    [SerializeField] List<Transform> bases;
    [SerializeField] GameObject[] skins = new GameObject[5];
    [SerializeField] List<Tween> tweens = new List<Tween>();
    [SerializeField] Slider slider;
    [SerializeField] Text magnetTxt;
    private void Start()
    {
        Level.slide = slider;
        
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.liveLoss = OnLiveLoss;
        levelTxt.text = "Level " + (DataManager.LevelPassed+1).ToString();
        OnInit();
        Magnet.text = magnetTxt;
        

    }
    public void Pause()
    {
        GameManager.Instance.Pause();
        UIManager.Instance.Pause();
    }
    public void OnLiveLoss(bool isAdd)
    {
        int i = DataManager.BallLives;
        if(!isAdd)
        {
            Vector3 startPos = images[i].transform.position;
            Vector3 scale = images[i].transform.localScale;
            DOVirtual.DelayedCall(0.1f, () =>
            {
                Vector3 endPos = Camera.main.WorldToScreenPoint(Ball.InitPos.position) ;
                GameObject g = Instantiate(skins[i], skins[i].transform.position, skins[i].transform.rotation, bases[i]);
                skins[i].SetActive(false);
                g.transform.DORotate(Vector3.right * -31+Vector3.up*180, 0.6f).OnComplete(() => Destroy(g));
                images[i].transform.DOMove(endPos, 0.6f);
                images[i].transform.DOScale(3.5f, 0.6f).OnComplete(() =>
                {
                    Transform tf = GameManager.Instance.SkinBall();
                    tf.parent.gameObject.SetActive(true);
                    lossImages[i].SetActive(true);
                    images[i].SetActive(false);
                    images[i].transform.position = startPos;
                    images[i].transform.localScale = scale;
                    skins[i].SetActive(true);
                }
                );




            }
            );
            
        }
        else
        {
             Spawn(i - 1); 
        }


    }
    void OnInit()
    {
        int live = DataManager.BallLives;
        for (int i = 0; i < 5; i++)
        {
            Spawn(i);
            if (skins[i])
            {
                skins[i].transform.DOKill();
                Destroy(skins[i]);
            }
            SkinData skin = DataManager.Instance.GetSkin(DataManager.Instance.GetLastSkin())    ;
            skins[i] = Instantiate(skin.skinTire, bases[i].position, bases[i].rotation, bases[i]);
            if(skin.skinHead)
            {
                Instantiate(skin.skinHead, skins[i].transform.position, skins[i].transform.rotation, skins[i].transform);
            }
        }
        StartCoroutine(IERotate());
        for (int i = live; i < 5; i++)
        {
            images[i].SetActive(false);
            lossImages[i].SetActive(true);
        }
    }
    IEnumerator IERotate()
    {
        for (int i = 0; i < 5; i++)
        {
            tweens.Add( skins[i].transform.DORotate(Vector3.up * 360, 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.InOutCubic));
            yield return new WaitForSeconds(0.1f);

        }
    }
    void Spawn(int i)
    {
        images[i].SetActive(true);
        lossImages[i].SetActive(false);

        
    }
    public void MagnetOn()
    {
        Magnet.isMagnet= true;
        Magnet.MagnetOn(10,7);
    }


}
