using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MovableObject
{
    Gauge hitPoint;
    Gauge manaPoint;
    Gauge staminaPoint;
    Gauge downGauge;

    SkillType currentSkill;  //내가 준비된 스킬
    float SkillReadyLeft = 0.0f; //스킬 장전까지 남은 시간

    Hitable attackTarget;

    bool offensive = false;
    bool controllable = true;

    public virtual void Attack(Hitable target)
    {
        if(target.TakeDamage(this))
        {
            Debug.Log("공격 성공");
        }
        else
        {
            //디펜스 쓸때 공격자는 공격 모션은 유지하지만 락걸림
            Debug.Log("공격 실패");
        };
    }

    //상대방이 데미지를 주려고 부르는 함수!
    public override bool TakeDamage(Pawn from)
    {
        bool result = true;//기본적으로 공격은 성공하지만 경합일 경우 아래쪽에서 실패 체크
        //내가 상대방을 보고 있는 경우에 그 상대방이 나를 때린 경우 또는 지정하지 않아도 경합하는 경우
        if( from.currentSkill!=null && from == attackTarget || currentSkill.IsPatrol())
        {
            //상대의 공격이랑 내 공격이랑 체크!
            result = from.currentSkill.ValidCheck(currentSkill);
        };

        //얘가 본인의 사망 체크 하게 하고
        //다운도 얘가 하게 해

        return result;
    }
}
