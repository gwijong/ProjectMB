using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    /// <summary> 생명력 게이지 summary>
    Gauge hitPoint;
    /// <summary> 마나 게이지 summary>
    Gauge manaPoint;
    /// <summary> 스태미나 게이지 summary>
    Gauge staminaPoint;
    /// <summary> 다운 게이지 summary>
    Gauge downGauge;
    public CharacterData data;

    /// <summary> 준비 완료된 현재 스킬</summary>
    Skill skill;
    /// <summary> 스킬 장전까지 남은 시간</summary>
    float skillCastingTimeLeft = 0.0f;
    /// <summary> 지정한 공격 타겟</summary>
    public Hitable attackTarget;

    bool controllable = true;

    protected override void Start()
    {
        base.Start();       
        agent.angularSpeed = 1000;  //내비게이션 회전값
        agent.acceleration = 100; //내비게이션 가속도
        agent.speed = data.Speed; //내비게이션 이동속도

        hitPoint.Max = data.HitPoint; //최대 생명력
        hitPoint.Current = data.HitPoint;  //현재 생명력
        hitPoint.FillableRate = 1.0f;  //부상률

        manaPoint.Max = data.ManaPoint; //최대 마나
        manaPoint.Current = data.ManaPoint;  //현재 마나

        staminaPoint.Max = data.StaminaPoint; //최대 스태미나
        staminaPoint.Current = data.StaminaPoint;  //현재 스태미나
        staminaPoint.FillableRate = 1.0f;  //허기

        downGauge.Max = 100; //다운 게이지
        downGauge.Current = 0;  //현재 누적된 다운게이지
    }

    /// <summary> 공격 함수</summary>
    public virtual void Attack(Hitable enemyTarget)
    {
        if (enemyTarget.TakeDamage(this) == true)//공격에 성공한 경우
        {
            Debug.Log("공격 성공");
        }
        else//공격에 실패한 경우
        {
            //공격이 실패한 경우에는 공격 대상자가 리턴값을 받아서 경직에 스스로 걸리게 해야해
            //디펜스 쓸때 공격자는 공격 모션은 유지하지만 락걸림
            //카운터는 반격 당하고 다운됨
            Debug.Log("공격 실패");
        };
    }

    /// <summary> 상대방이 이 캐릭터에 데미지를 주려고 상대방이 부르는 함수</summary>
    public override bool TakeDamage(Character enmeyAttacker)
    {
        bool result = true;//기본적으로 공격은 성공하지만 경합일 경우 아래쪽에서 실패 체크

        //서로 마주보고 싸우는 경우 또는 디펜스.카운터 같은, 공격이 들어오면 무조건 스킬 사용 가능한지 체크해야 하는 경우
        if(enmeyAttacker.skill!=null && this.attackTarget == enmeyAttacker || this.skill.mustCheck()==true)
        {
            
            result = enmeyAttacker.skill.WinnerCheck(this.skill); //상대방 스킬과 내 스킬의 우선순위 비교
        };
        DownCheck();
        DieCheck();
        return result;
    }
    public void DownCheck()
    {

    }
    public void DieCheck()
    {

    }
}
