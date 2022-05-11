using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //플레이어 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
        
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> 양털 채집 </summary>
    public void Sheeping()
    {
        PlayAnim("Sheeping");
        StartCoroutine(Wait(5f));
        //GameManager.itemManager.DropItem(Define.Item.Wool, 1);
        StartCoroutine(DropItem());
    }

    /// <summary> 2초뒤 양털 생성 </summary>
    IEnumerator DropItem() 
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.itemManager.DropItem(Define.Item.Wool, 1);
    }
}
