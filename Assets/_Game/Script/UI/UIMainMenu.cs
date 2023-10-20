using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIAbstract
{
    [SerializeField] List<Image> images = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> texts= new List<TextMeshProUGUI>();
    public override void Open()
    {
        base.Open();
        DOTween.Play(this);
        SetLevel();
        GameManager.Instance.OnInit();
        GameManager.ChangeState(GameState.MainMenu);
    }
    public void Play()
    {
        UIManager.Instance.Continue();
        GameManager.Instance.Continue();
        DOVirtual.DelayedCall(0.05f,() => Level.slide.value = 0);

    }
    public void World()
    {
        CamerManager.Instance.World();
        UIManager.Instance.OpenWorld();
    }    
    public void Ball()
    {
        CamerManager.Instance.Ball();
        UIManager.Instance.OpenBall();
    }    
    public void AddTicket()
    {

    }
    public void Setting()
    {
        UIManager.Instance.OpenSetting();
    }
    void SetLevel()
    {
        for(int i =0;i<3;i++)
        {
            images[i].color = Color.white;  
        }
        images[3].color = Color.yellow;
        int level = DataManager.LevelPassed+1;
        int t = Mathf.CeilToInt(level / 4f);
        int start = t * 4 -4;
        int end = t * 4-1;
        for (int i = start;i<=end;i++)
        {
            texts[i%4].text = (i+1).ToString();
            if(level > i)
            {
                images[i % 4].color = Color.blue;
            }
            
        }
        images[(level-1) % 4].transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }    



}
