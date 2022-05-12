using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary> 배경음 오디오소스 </summary>
    public AudioSource bgmPlayer;
    /// <summary> 효과음 오디오소스 </summary>
    public AudioSource effectPlayer;

    [SerializeField]
    AudioClip punch_hit;
    [SerializeField]
    AudioClip punch_blow;
    [SerializeField]
    AudioClip guard;
    [SerializeField]
    AudioClip emotion_success;
    [SerializeField]
    AudioClip emotion_fail;
    [SerializeField]
    AudioClip eatfood;
    [SerializeField]
    AudioClip down;
    [SerializeField]
    AudioClip drinkpotion;
    [SerializeField]
    AudioClip dungeon_monster_appear1;
    [SerializeField]
    AudioClip skill_cancel;
    [SerializeField]
    AudioClip skill_complete;
    [SerializeField]
    AudioClip skill_ready;
    void Start()
    {

    }

    //사용 예시    GameManager.soundmanager.PlaySfxPlayer("punch_hit");
    public void PlaySfxPlayer(Define.SoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.SoundEffect.punch_hit:
                effectPlayer.clip = punch_hit;
                break;
            case Define.SoundEffect.punch_blow:
                effectPlayer.clip = punch_blow;
                break;
            case Define.SoundEffect.guard:
                effectPlayer.clip = guard;
                break;
            case Define.SoundEffect.emotion_success:
                effectPlayer.clip = emotion_success;
                break;
            case Define.SoundEffect.emotion_fail:
                effectPlayer.clip = emotion_fail;
                break;
            case Define.SoundEffect.eatfood:
                effectPlayer.clip = eatfood;
                break;
            case Define.SoundEffect.down:
                effectPlayer.clip = down;
                break;
            case Define.SoundEffect.drinkpotion:
                effectPlayer.clip = drinkpotion;
                break;
            case Define.SoundEffect.dungeon_monster_appear1:
                effectPlayer.clip = dungeon_monster_appear1;
                break;
            case Define.SoundEffect.skill_cancel:
                effectPlayer.clip = skill_cancel;
                break;
            case Define.SoundEffect.skill_complete:
                effectPlayer.clip = skill_complete;
                break;
            case Define.SoundEffect.skill_ready:
                effectPlayer.clip = skill_ready;
                break;
        }
        effectPlayer.Play();
    }
}