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

    //¿¹½Ã:GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//°³ Â¢´Â È¿°úÀ½
    /// <summary> »ç¿îµå ÀÌÆåÆ® Àç»ý</summary>
    public void PlaySfxPlayer(Define.NPCSoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPCSoundEffect.dog01_natural_stand_offensive:
                npcEffectPlayer.clip = dog01_natural_stand_offensive;  //°³ Â¢±â
                break;
            case Define.NPCSoundEffect.wolf01_natural_stand_offensive:
                npcEffectPlayer.clip = wolf01_natural_stand_offensive; //´Á´ë Â¢±â
                break;
            case Define.NPCSoundEffect.sheep:
                npcEffectPlayer.clip = sheep; //¾ç ¿ï±â
                break;
            case Define.NPCSoundEffect.chicken_fly:
                npcEffectPlayer.clip = chicken_fly; //´ß ³¯±â
                break;
        }

        npcEffectPlayer.Play();
    }
}
