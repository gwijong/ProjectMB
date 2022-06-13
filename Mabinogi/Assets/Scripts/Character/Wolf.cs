using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 늑대 몬스터 캐릭터 </summary>
public class Wolf : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.wolf;  //늑대 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void Bark()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_stand_offensive, transform.position);//늑대 짖는 효과음
    }
    public void Samsh()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_attack_smash, transform.position);// 효과음
    }
    /// <summary> 카운터 효과음 </summary>
    public void Counter()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_attack_counter, transform.position);// 효과음
    }
    /// <summary> 다운 효과음 </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_down, transform.position);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_hit, transform.position);// 효과음
    }
}
