using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.rooster;  //수탉 스킬 리스트 사용
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
    /// <summary> 닭 나는 효과음 </summary>
    public void Fly()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_fly, transform.position);
    }

    /// <summary> 다운 효과음 </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_down, transform.position);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_hit, transform.position);// 효과음
    }
}
