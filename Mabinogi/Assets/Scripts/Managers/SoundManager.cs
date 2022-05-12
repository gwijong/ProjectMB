using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> 배경음 오디오소스 </summary>
    AudioSource bgmPlayer;

    [SerializeField]
    /// <summary> 효과음 오디오소스 </summary>
    AudioSource effectPlayer;
    
    [Tooltip("주먹 평타")]
    [SerializeField]
    /// <summary> 주먹 평타 </summary>
    AudioClip punch_hit;
    
    [Tooltip("다운 되는 중")]
    [SerializeField]
    /// <summary> 다운 되는 중 </summary>
    AudioClip punch_blow;
    
    [Tooltip("디펜스 방어")]
    [SerializeField]
    /// <summary> 디펜스 방어 </summary>
    AudioClip guard;
    
    [Tooltip("생산 성공")]
    [SerializeField]
    /// <summary> 생산 성공 </summary>
    AudioClip emotion_success;
    
    [Tooltip("생산 실패")]
    [SerializeField]
    /// <summary> 생산 실패 </summary>
    AudioClip emotion_fail;
    
    [Tooltip("음식 먹기")]
    [SerializeField]
    /// <summary> 음식 먹기 </summary>
    AudioClip eatfood;
    
    [Tooltip("다운 바닥에 떨어짐")]
    [SerializeField]
    /// <summary> 다운 바닥에 떨어짐 </summary>
    AudioClip down;
    
    [Tooltip("물약 마시기")]
    [SerializeField]
    /// <summary> 물약 마시기 </summary>
    AudioClip drinkpotion;
    
    [Tooltip("몬스터 등장")]
    [SerializeField]
    /// <summary> 몬스터 등장 </summary>
    AudioClip dungeon_monster_appear1;
    
    [Tooltip("스킬 취소")]
    [SerializeField]
    /// <summary> 스킬 취소 </summary>
    AudioClip skill_cancel;
    
    [Tooltip("스킬 시전")]
    [SerializeField]
    /// <summary> 스킬 시전 </summary>
    AudioClip skill_standby;
    
    [Tooltip("스킬 준비 완료")]
    [SerializeField]
    /// <summary> 스킬 준비 완료 </summary>
    AudioClip skill_ready;

    [Tooltip("소지품창 열기")]
    [SerializeField]
    /// <summary> 소지품창 열기 </summary>
    AudioClip inventory_open;

    [Tooltip("소지품창 닫기")]
    [SerializeField]
    /// <summary> 소지품창 닫기 </summary>
    AudioClip inventory_close;

    [Tooltip("버튼 클릭")]
    [SerializeField]
    /// <summary> 버튼 클릭 </summary>
    AudioClip gen_button_down;

    [Tooltip("레벨업")]
    [SerializeField]
    /// <summary> 레벨업 </summary>
    AudioClip character_levelup;

    //예시: GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_ready);//스킬 준비 완료 효과음
    /// <summary> 사운드 이펙트 재생</summary>
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
            case Define.SoundEffect.skill_standby:
                effectPlayer.clip = skill_standby;
                break;
            case Define.SoundEffect.skill_ready:
                effectPlayer.clip = skill_ready;
                break;
            case Define.SoundEffect.inventory_open:
                effectPlayer.clip = inventory_open;
                break;
            case Define.SoundEffect.inventory_close:
                effectPlayer.clip = inventory_close;
                break;
            case Define.SoundEffect.gen_button_down:
                effectPlayer.clip = gen_button_down;
                break;
            case Define.SoundEffect.character_levelup:
                effectPlayer.clip = character_levelup;
                break;
        }
        effectPlayer.Play();
    }
}