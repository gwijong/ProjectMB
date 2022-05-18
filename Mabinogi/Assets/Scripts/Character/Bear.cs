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

    public void Bark()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.wolf01_natural_stand_offensive);// 효과음
    }
}
