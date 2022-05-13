using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC 사운드 매니저 </summary>
public class NPCSoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> NPC 효과음 오디오소스 </summary>
    AudioSource npcEffectPlayer;

    [Tooltip("개 짖기")]
    [SerializeField]
    /// <summary> 개 짖기 </summary>
    AudioClip dog01_natural_stand_offensive;

    [Tooltip("늑대 짖기")]
    [SerializeField]
    /// <summary> 늑대 짖기 </summary>
    AudioClip wolf01_natural_stand_offensive;

    [Tooltip("양 울기")]
    [SerializeField]
    /// <summary> 양 울기 </summary>
    AudioClip sheep;

    [Tooltip("닭 날기")]
    [SerializeField]
    /// <summary> 닭 날기 </summary>
    AudioClip chicken_fly;

    //예시:GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//개 짖는 효과음
    /// <summary> 사운드 이펙트 재생</summary>
    public void PlaySfxPlayer(Define.NPCSoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPCSoundEffect.dog01_natural_stand_offensive:
                npcEffectPlayer.clip = dog01_natural_stand_offensive;
                break;
            case Define.NPCSoundEffect.wolf01_natural_stand_offensive:
                npcEffectPlayer.clip = wolf01_natural_stand_offensive;
                break;
            case Define.NPCSoundEffect.sheep:
                npcEffectPlayer.clip = sheep;
                break;
            case Define.NPCSoundEffect.chicken_fly:
                npcEffectPlayer.clip = chicken_fly;
                break;
        }

        npcEffectPlayer.Play();
    }
}
