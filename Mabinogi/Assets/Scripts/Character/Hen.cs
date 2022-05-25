using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hen : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.hen;  //암탉 스킬 리스트 사용
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

    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //상대방과 내가 적인지 체크
        {
            return Define.InteractType.Attack; //상호작용 타입을 공격으로 리턴
        }
        return Define.InteractType.Egg; //적이 아니면 달걀채집 리턴
    }

    public void Fly()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_fly);//닭 나는 효과음
    }

    /// <summary> 다운 효과음 </summary>
    public void Blowaway()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_down);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_hit);// 효과음
    }
}
