using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Character
{
    protected override void Awake()
    {
        base.Awake();
        walkSpeed = data.Speed;//곰은 무조건 달림
        skillList = SkillList.bear;  //곰 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    private void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    /// <summary> 전투모드 효과음 </summary>
    public void StandOffensive()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_stand_offensive);// 효과음
    }
    /// <summary> 스매시 효과음 </summary>
    public void Samsh()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_attack_smash);// 효과음
    }
    /// <summary> 카운터 효과음 </summary>
    public void Counter()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_attack_counter);// 효과음
    }
    /// <summary> 다운 효과음 </summary>
    public void Blowaway()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_blowaway);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_hit);// 효과음
    }

}
