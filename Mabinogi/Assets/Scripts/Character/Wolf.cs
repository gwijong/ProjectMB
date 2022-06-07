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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_stand_offensive, transform.position);//늑대 짖는 효과음
    }
}
