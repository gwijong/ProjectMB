using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC »ç¿îµå ¸Å´ÏÀú </summary>
public class NPCSoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> NPC È¿°úÀ½ ¿Àµð¿À¼Ò½º </summary>
    AudioSource npcEffectPlayer;

    [Tooltip("°³ Â¢±â")]
    [SerializeField]
    /// <summary> °³ Â¢±â </summary>
    AudioClip dog01_natural_stand_offensive;

    [Tooltip("°³ ´Ù¿î")]
    [SerializeField]
    /// <summary> °³ ´Ù¿î </summary>
    AudioClip dog01_natural_blowaway;

    [Tooltip("°³ ¸Â±â")]
    [SerializeField]
    /// <summary> °³ Â¢±â </summary>
    AudioClip dog01_natural_hit;

    [Tooltip("´Á´ë Â¢±â")]
    [SerializeField]
    /// <summary> ´Á´ë Â¢±â </summary>
    AudioClip wolf01_natural_stand_offensive;

    [Tooltip("¾ç ¿ï±â")]
    [SerializeField]
    /// <summary> ¾ç ¿ï±â </summary>
    AudioClip sheep;

    [Tooltip("´ß ³¯±â")]
    [SerializeField]
    /// <summary> ´ß ³¯±â </summary>
    AudioClip chicken_fly;

    [Tooltip("´ß ´Ù¿î")]
    [SerializeField]
    /// <summary> ´ß ´Ù¿î </summary>
    AudioClip chicken_down;

    [Tooltip("´ß ¸Â±â")]
    [SerializeField]
    /// <summary> ´ß ¸Â±â </summary>
    AudioClip chicken_hit;

    [Tooltip("°õ ÀüÅõ¸ðµå")]
    [SerializeField]
    /// <summary> °õ ÀüÅõ¸ðµå </summary>
    AudioClip bear01_natural_stand_offensive;

    [Tooltip("°õ ½º¸Å½Ã")]
    [SerializeField]
    /// <summary> °õ ½º¸Å½Ã </summary>
    AudioClip bear01_natural_attack_smash;

    [Tooltip("°õ Ä«¿îÅÍ")]
    [SerializeField]
    /// <summary> °õ Ä«¿îÅÍ </summary>
    AudioClip bear01_natural_attack_counter;

    [Tooltip("°õ ´Ù¿î")]
    [SerializeField]
    /// <summary> °õ ´Ù¿î </summary>
    AudioClip bear01_natural_blowaway;

    [Tooltip("°õ ¸Â±â")]
    [SerializeField]
    /// <summary> °õ ¸Â±â </summary>
    AudioClip bear01_natural_hit;

    [Tooltip("°ñ·½ ÀüÅõ¸ðµå")]
    [SerializeField]
    /// <summary> °ñ·½ ÀüÅõ¸ðµå </summary>
    AudioClip golem01_woo;
    
    //¿¹½Ã:GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//°³ Â¢´Â È¿°úÀ½
    /// <summary> »ç¿îµå ÀÌÆåÆ® Àç»ý</summary>
    public void PlaySfxPlayer(Define.NPCSoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPCSoundEffect.none:
                npcEffectPlayer.clip = null;  //¾Æ¹« ¼Ò¸®µµ ¾È³²
                break;
            case Define.NPCSoundEffect.dog01_natural_stand_offensive:
                npcEffectPlayer.clip = dog01_natural_stand_offensive;  //°³ Â¢±â
                break;
            case Define.NPCSoundEffect.dog01_natural_blowaway:
                npcEffectPlayer.clip = dog01_natural_blowaway;  
                break;
            case Define.NPCSoundEffect.dog01_natural_hit:
                npcEffectPlayer.clip = dog01_natural_hit;  
                break;

            case Define.NPCSoundEffect.wolf01_natural_stand_offensive:
                npcEffectPlayer.clip = wolf01_natural_stand_offensive; //´Á´ë Â¢±â
                break;

            case Define.NPCSoundEffect.sheep:
                npcEffectPlayer.clip = sheep; //¾ç ¿ï±â
                break;

            case Define.NPCSoundEffect.chicken_fly:
                npcEffectPlayer.clip = chicken_fly; 
                break;
            case Define.NPCSoundEffect.chicken_down:
                npcEffectPlayer.clip = chicken_down; 
                break;
            case Define.NPCSoundEffect.chicken_hit:
                npcEffectPlayer.clip = chicken_hit; 
                break;

            case Define.NPCSoundEffect.bear01_natural_stand_offensive:
                npcEffectPlayer.clip = bear01_natural_stand_offensive; 
                break;
            case Define.NPCSoundEffect.bear01_natural_attack_smash:
                npcEffectPlayer.clip = bear01_natural_attack_smash; 
                break;
            case Define.NPCSoundEffect.bear01_natural_attack_counter:
                npcEffectPlayer.clip = bear01_natural_attack_counter; 
                break;
            case Define.NPCSoundEffect.bear01_natural_blowaway:
                npcEffectPlayer.clip = bear01_natural_blowaway; 
                break;
            case Define.NPCSoundEffect.bear01_natural_hit:
                npcEffectPlayer.clip = bear01_natural_hit; 
                break;
            case Define.NPCSoundEffect.golem01_woo:
                npcEffectPlayer.clip = golem01_woo;
                break;
        }
        
        npcEffectPlayer.Play();
    }
}
