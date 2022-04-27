using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.dog;  //개 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }


    protected override void Update()
    {
        base.Update();
    }
}
