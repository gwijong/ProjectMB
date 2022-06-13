using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 몬스터 골렘 </summary>
public class Golem : Character
{
    bool bossDieCheck = false;
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.golem;  //골렘 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (die == true && bossDieCheck ==false) //보스 사망시 이펙트 효과
        {
            bossDieCheck = true;
            GameObject dieEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/ChargingPop"));
            dieEffect.transform.position = gameObject.transform.position + Vector3.up * 3;
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.item_get, transform.position);// 효과음
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        bossDieCheck = false;
    }
    /// <summary> 전투모드 효과음 </summary>
    public void StandOffensive()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_woo, transform.position);// 효과음
    }
    /// <summary> 걷기 효과음 </summary>
    public void Walk()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_walk, transform.position);// 효과음
    }
    /// <summary> 일어나기 효과음 </summary>
    public void DownToStand()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_downb_to_stand, transform.position);// 효과음
    }
    /// <summary> 맞기 효과음 </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_hit, transform.position);// 효과음
    }
    /// <summary> 다운 효과음 </summary>
    public void Down()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_blowaway_ground, transform.position);// 효과음
    }

}
