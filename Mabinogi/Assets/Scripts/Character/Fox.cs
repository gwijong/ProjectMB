using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 여우 몬스터 </summary>
public class Fox : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.fox;  //여우 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.none, transform.position);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.none, transform.position);// 효과음
    }
}
