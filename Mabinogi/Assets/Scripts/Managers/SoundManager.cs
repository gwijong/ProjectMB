using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;

    //public float bgm;
    //public float sfx;

    [SerializeField]
    AudioClip Button;
    [SerializeField]
    AudioClip Buy;
    [SerializeField]
    AudioClip Clear;
    [SerializeField]
    AudioClip Fail;
    [SerializeField]
    AudioClip Grow;
    [SerializeField]
    AudioClip PauseIn;
    [SerializeField]
    AudioClip PauseOut;
    [SerializeField]
    AudioClip Sell;
    [SerializeField]
    AudioClip Touch;
    [SerializeField]
    AudioClip Unlock;
    void Start()
    {
        //GameManager.update.UpdateMethod -= OnUpdate;
        //GameManager.update.UpdateMethod += OnUpdate;
    }

    //사용 예시    GameManager.soundmanager.PlaySfxPlayer("PauseOut");
    public void PlaySfxPlayer(string audioClipName)
    {
        switch (audioClipName)
        {
            case "Button":
                sfxPlayer.clip = Button;
                break;
            case "Buy":
                sfxPlayer.clip = Buy;
                break;
            case "Clear":
                sfxPlayer.clip = Clear;
                break;
            case "Fail":
                sfxPlayer.clip = Fail;
                break;
            case "Grow":
                sfxPlayer.clip = Grow;
                break;
            case "PauseIn":
                sfxPlayer.clip = PauseIn;
                break;
            case "PauseOut":
                sfxPlayer.clip = PauseOut;
                break;
            case "Sell":
                sfxPlayer.clip = Sell;
                break;
            case "Touch":
                sfxPlayer.clip = Touch;
                break;
            case "Unlock":
                sfxPlayer.clip = Unlock;
                break;
        }
        sfxPlayer.Play();
    }
}