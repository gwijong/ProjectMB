using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 동물 개 </summary>
public class Dog : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.dog;  //개 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> 전투모드 효과음 </summary>
    public void Bark()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_stand_offensive,transform.position);//개 짖는 효과음
    }
    /// <summary> 다운 효과음 </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_blowaway, transform.position);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_hit, transform.position);// 효과음
    }
}
