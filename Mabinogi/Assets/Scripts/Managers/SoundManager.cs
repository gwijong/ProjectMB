using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 플레이어 사운드 매니저 </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> 배경음 오디오소스 </summary>
    public AudioSource bgmPlayer;

    [SerializeField]
    /// <summary> 효과음 오디오소스 </summary>
    public AudioSource effectPlayer;

    [Tooltip("나오 배경음악")]
    [SerializeField]
    /// <summary> 나오 배경음악 </summary>
    AudioClip naoBgm;

    [Tooltip("고로 배경음악")]
    [SerializeField]
    /// <summary> 고로 배경음악 </summary>
    AudioClip goroBgm;

    [Tooltip("타르라크 배경음악")]
    [SerializeField]
    /// <summary> 타르라크 배경음악 </summary>
    AudioClip tarlachBgm;

    [Tooltip("소울스트림 배경음악")]
    [SerializeField]
    /// <summary> 소울스트림 배경음악 </summary>
    AudioClip soulstreamBgm;

    [Tooltip("인트로 배경음악")]
    [SerializeField]
    /// <summary> 인트로 배경음악 </summary>
    AudioClip introBgm;

    [Tooltip("월드 배경음악")]
    [SerializeField]
    /// <summary> 월드 배경음악 </summary>
    AudioClip worldBgm;

    [Tooltip("던전 배경음악")]
    [SerializeField]
    /// <summary> 던전 배경음악 </summary>
    AudioClip dungeonBgm;

    [Tooltip("보스 배경음악")]
    [SerializeField]
    /// <summary> 보스 배경음악 </summary>
    AudioClip bossBgm;

    [Tooltip("사망 배경음악")]
    [SerializeField]
    /// <summary> 사망 배경음악 </summary>
    AudioClip dieBgm;

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

    [Tooltip("던전 문 열림")]
    [SerializeField]
    /// <summary> 던전 문 열림 </summary>
    AudioClip dungeon_door;

    [Tooltip("마법 시전")]
    [SerializeField]
    /// <summary> 마법 시전 </summary>
    AudioClip magic_standby;

    [Tooltip("마법 준비 완료")]
    [SerializeField]
    /// <summary> 마법 준비 완료 </summary>
    AudioClip magic_ready;

    [Tooltip("마법 발사")]
    [SerializeField]
    /// <summary> 마법 발사 </summary>
    AudioClip magic_lightning;

    private void Start()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayBgmPlayer(Define.NPC audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPC.Nao:
                bgmPlayer.clip = naoBgm;
                break;
            case Define.NPC.Goro:
                bgmPlayer.clip = goroBgm;
                break;
            case Define.NPC.Tarlach:
                bgmPlayer.clip = tarlachBgm;
                break;
            default:
                bgmPlayer.clip = null;
                break;
        }
        bgmPlayer.Play();
    }

    public void PlayBgmPlayer(Define.Scene audioClipName)
    {
        switch (audioClipName)
        {
            case Define.Scene.Soulstream:
                bgmPlayer.clip = null;
                break;
            case Define.Scene.Intro:
                bgmPlayer.clip = introBgm;
                break;
            case Define.Scene.World:
                bgmPlayer.clip = worldBgm;
                break;
            case Define.Scene.Dungeon:
                bgmPlayer.clip = dungeonBgm;
                break;
            case Define.Scene.Boss:
                bgmPlayer.clip = bossBgm;
                break;
            case Define.Scene.Die:
                bgmPlayer.clip = dieBgm;
                break;
            default:
                bgmPlayer.clip = null;
                break;
        }
        bgmPlayer.Play();
    }
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
            case Define.SoundEffect.dungeon_door:
                effectPlayer.clip = dungeon_door;
                break;
            case Define.SoundEffect.magic_ready:
                effectPlayer.clip = magic_ready;
                break;
            case Define.SoundEffect.magic_standby:
                effectPlayer.clip = magic_standby;
                break;
            case Define.SoundEffect.magic_lightning:
                effectPlayer.clip = magic_lightning;
                break;
            default:
                effectPlayer.clip = null;
                break;
        }
        effectPlayer.Play();
    }
}