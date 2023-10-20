using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIAbstract
{
    [SerializeField] Toggle music;
    [SerializeField] Toggle sound;
    [SerializeField] Toggle haptic;

    [SerializeField] Image musicIm;
    [SerializeField] Image soundIm;
    [SerializeField] Image hapticIm;

    [SerializeField] RectTransform musicO;
    [SerializeField] RectTransform soundO;
    [SerializeField] RectTransform hapticO;

    float musicX;
    float soundX;
    float hapticX;
    public override void SetCoinText()
    {
        
    }
    private void Start()
    {
        musicX = musicO.anchoredPosition.x;
        music.onValueChanged.AddListener(ChangeMusic);
        music.isOn = DataManager.IsMusic == 0;
        ChangeMusic(music.isOn);

        soundX = soundO.anchoredPosition.x;
        sound.onValueChanged.AddListener(ChangeSound);
        sound.isOn = DataManager.IsSound == 0;
        ChangeSound(sound.isOn);

        hapticX = hapticO.anchoredPosition.x;
        haptic.onValueChanged.AddListener(ChangeHaptic);
        haptic.isOn = DataManager.IsHaptic == 0;
        ChangeHaptic(haptic.isOn);
    }
    public void ChangeMusic(bool on)
    {       
        DataManager.IsMusic = on ? 0 : 1;
        musicO.DOAnchorPosX(on ? musicX : -musicX, 0.5f).OnComplete(()=>musicIm.enabled = on);
    }
    public void ChangeSound(bool on)
    {
        DataManager.IsSound = on ? 0 : 1;
        soundO.DOAnchorPosX(on ? soundX : -soundX, 0.5f).OnComplete(() => soundIm.enabled = on);
    }
    public void ChangeHaptic(bool on)
    {
        DataManager.IsHaptic = on ? 0 : 1;
        hapticO.DOAnchorPosX(on ? hapticX : -hapticX, 0.5f).OnComplete(() => hapticIm.enabled = on);
    }
    public void OpenMain()
    {
        UIManager.Instance.MainMenu();
    }


}
