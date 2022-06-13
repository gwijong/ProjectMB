using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 닭 동물 캐릭터 </summary>
public class Hen : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.hen;  //암탉 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //상대방과 내가 적인지 체크
        {
            return Define.InteractType.Attack; //상호작용 타입을 공격으로 리턴
        }
        return Define.InteractType.Egg; //적이 아니면 달걀채집 리턴
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
