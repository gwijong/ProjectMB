using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.sheep;  //양 스킬 리스트 사용
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
        return Define.InteractType.Sheeping; //적이 아니면 양털채집 리턴
    }

    public void Bark()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.sheep);//양 우는 효과음
    }
}
