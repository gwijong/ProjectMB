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

    [Tooltip("튜토리얼 배경음악")]
    [SerializeField]
    /// <summary> 튜토리얼 배경음악 </summary>
    AudioClip tutorialBgm;

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

    [Tooltip("개 짖기")]
    [SerializeField]
    /// <summary> 개 짖기 </summary>
    AudioClip dog01_natural_stand_offensive;

    [Tooltip("개 다운")]
    [SerializeField]
    /// <summary> 개 다운 </summary>
    AudioClip dog01_natural_blowaway;

    [Tooltip("개 맞기")]
    [SerializeField]
    /// <summary> 개 짖기 </summary>
    AudioClip dog01_natural_hit;

    [Tooltip("늑대 짖기")]
    [SerializeField]
    /// <summary> 늑대 짖기 </summary>
    AudioClip wolf01_natural_stand_offensive;

    [Tooltip("늑대 카운터")]
    [SerializeField]
    /// <summary> 늑대 카운터 </summary>
    AudioClip wolf01_natural_attack_counter;

    [Tooltip("늑대 스매시")]
    [SerializeField]
    /// <summary> 늑대 스매시 </summary>
    AudioClip wolf01_natural_attack_smash;

    [Tooltip("늑대 다운")]
    [SerializeField]
    /// <summary> 늑대 다운 </summary>
    AudioClip wolf01_natural_down;

    [Tooltip("늑대 맞기")]
    [SerializeField]
    /// <summary> 늑대 맞기 </summary>
    AudioClip wolf01_natural_hit;

    [Tooltip("양 울기")]
    [SerializeField]
    /// <summary> 양 울기 </summary>
    AudioClip sheep;

    [Tooltip("닭 날기")]
    [SerializeField]
    /// <summary> 닭 날기 </summary>
    AudioClip chicken_fly;

    [Tooltip("닭 다운")]
    [SerializeField]
    /// <summary> 닭 다운 </summary>
    AudioClip chicken_down;

    [Tooltip("닭 맞기")]
    [SerializeField]
    /// <summary> 닭 맞기 </summary>
    AudioClip chicken_hit;

    [Tooltip("곰 전투모드")]
    [SerializeField]
    /// <summary> 곰 전투모드 </summary>
    AudioClip bear01_natural_stand_offensive;

    [Tooltip("곰 스매시")]
    [SerializeField]
    /// <summary> 곰 스매시 </summary>
    AudioClip bear01_natural_attack_smash;

    [Tooltip("곰 카운터")]
    [SerializeField]
    /// <summary> 곰 카운터 </summary>
    AudioClip bear01_natural_attack_counter;

    [Tooltip("곰 다운")]
    [SerializeField]
    /// <summary> 곰 다운 </summary>
    AudioClip bear01_natural_blowaway;

    [Tooltip("곰 맞기")]
    [SerializeField]
    /// <summary> 곰 맞기 </summary>
    AudioClip bear01_natural_hit;

    [Tooltip("골렘 전투모드")]
    [SerializeField]
    /// <summary> 골렘 전투모드 </summary>
    AudioClip golem01_woo;

    /// <summary> 최소 거리 </summary>
    public float minDistance;
    /// <summary> 최대 거리 </summary>
    public float maxDistance;

    private void Start()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary> npc에 따른 배경음악 재생</summary>
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

    /// <summary> 씬에 따른 배경음악 재생</summary>
    public void PlayBgmPlayer(Define.Scene audioClipName)
    {
        switch (audioClipName)
        {
            case Define.Scene.Soulstream:
                bgmPlayer.clip = null;
                break;
            case Define.Scene.Tutorial:
                bgmPlayer.clip = tutorialBgm;
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
    /// <summary> 거리에 따른 볼륨 크기를 가진 사운드 이펙트 재생</summary>
    public void PlaySfxPlayer(Define.SoundEffect audioClipName, Vector3 pos)
    {

        /*
        최소거리 1        1
        최대거리 0       20
        1 ~ 0   1 ~ 20
        result = input
            0 ~ 19
        result = input - min
        1 ~ 0    0 ~ 1
        result = (input - min) / (max - min); 

        1 ~ 0    1 ~ 0
        result = 1 - ((input - min) / (max - min));
         */
        float distanceVolume = (Camera.main.transform.position - pos).magnitude; //카메라와 소리를 내는 타겟 사이의 거리
        distanceVolume = 1 - (distanceVolume - minDistance) / (maxDistance - minDistance); //볼륨을 타겟 거리에서 멀수록 작게 설정
        distanceVolume = Mathf.Clamp(distanceVolume, 0, 1); //0에서 1 사이 값으로 특정
        PlaySfxPlayer(audioClipName, distanceVolume);
    }

    /// <summary> 재생할 효과음 고르기</summary>
    public AudioClip GetClipByName(Define.SoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.SoundEffect.punch_hit:                  return punch_hit;
            case Define.SoundEffect.punch_blow:                 return punch_blow;
            case Define.SoundEffect.guard:                      return guard;
            case Define.SoundEffect.emotion_success:            return emotion_success;
            case Define.SoundEffect.emotion_fail:               return emotion_fail;
            case Define.SoundEffect.eatfood:                    return eatfood;
            case Define.SoundEffect.down:                       return down;
            case Define.SoundEffect.drinkpotion:                return drinkpotion;
            case Define.SoundEffect.dungeon_monster_appear1:    return dungeon_monster_appear1;
            case Define.SoundEffect.skill_cancel:               return skill_cancel;
            case Define.SoundEffect.skill_standby:              return skill_standby;
            case Define.SoundEffect.skill_ready:                return skill_ready;
            case Define.SoundEffect.inventory_open:             return inventory_open;
            case Define.SoundEffect.inventory_close:            return inventory_close;
            case Define.SoundEffect.gen_button_down:            return gen_button_down;
            case Define.SoundEffect.character_levelup:          return character_levelup;
            case Define.SoundEffect.dungeon_door:               return dungeon_door;
            case Define.SoundEffect.magic_ready:                return magic_ready;
            case Define.SoundEffect.magic_standby:              return magic_standby;
            case Define.SoundEffect.magic_lightning:            return magic_lightning;
            case Define.SoundEffect.dog01_natural_stand_offensive: return dog01_natural_stand_offensive;
            case Define.SoundEffect.dog01_natural_blowaway: return dog01_natural_blowaway;
            case Define.SoundEffect.dog01_natural_hit: return dog01_natural_hit;
            case Define.SoundEffect.wolf01_natural_stand_offensive: return wolf01_natural_stand_offensive;
            case Define.SoundEffect.wolf01_natural_attack_counter: return wolf01_natural_attack_counter;
            case Define.SoundEffect.wolf01_natural_attack_smash: return wolf01_natural_attack_smash;
            case Define.SoundEffect.wolf01_natural_hit: return wolf01_natural_hit;
            case Define.SoundEffect.sheep: return sheep;
            case Define.SoundEffect.chicken_fly: return chicken_fly;
            case Define.SoundEffect.chicken_down: return chicken_down;
            case Define.SoundEffect.chicken_hit: return chicken_hit;
            case Define.SoundEffect.bear01_natural_stand_offensive: return bear01_natural_stand_offensive;
            case Define.SoundEffect.bear01_natural_attack_smash: return bear01_natural_attack_smash;
            case Define.SoundEffect.bear01_natural_attack_counter: return bear01_natural_attack_counter;
            case Define.SoundEffect.bear01_natural_blowaway: return bear01_natural_blowaway;
            case Define.SoundEffect.bear01_natural_hit: return bear01_natural_hit;
            case Define.SoundEffect.golem01_woo: return golem01_woo;
            default:                                            return null;
        }
    }
    /// <summary> 효과음 재생</summary>
    public void PlaySfxPlayer(Define.SoundEffect audioClipName, float volume = 1.0f)
    {
        AudioClip clip = GetClipByName(audioClipName);
        PlaySfxPlayer(clip, volume);
    }

    /// <summary> 효과음 재생</summary>
    public void PlaySfxPlayer(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            effectPlayer.PlayOneShot(clip, volume * effectPlayer.volume);
        };
    }
}