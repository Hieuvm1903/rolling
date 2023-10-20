using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip addCoin;
    [SerializeField] AudioClip collision;
    public AudioMixer mixer;
    private void Start()
    {
        MusicOn(DataManager.IsMusic == 0);
        SoundOn(DataManager.IsSound == 0);
        HapticOn(DataManager.IsHaptic == 0);

    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void AddCoin()
    {
        PlayAudio(addCoin);
    }
    public void Collision()
    {
        PlayAudio(collision);
    }
    public void Play()
    {
        audioSource.Play();
    }
    public void MusicOn(bool on)
    {
      //  print("music");
      //  print(on ? 0 : 1);
    }
    public void SoundOn(bool on)
    {
       // print("sound");
      ///  print(on ? 0 : 1);

    }
    public void HapticOn(bool on)
    {
       // print("haptic");
      //  print(on ? 0 : 1);

    }



}
