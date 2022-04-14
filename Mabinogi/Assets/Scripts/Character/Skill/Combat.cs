using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Skill
{
    /*
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
     */
    public override void SkillUse(Character enemyTarget)
    {
        if (enemyTarget.currentSkillId == Define.SkillState.Defense)  //방어자가 디펜스를 사용한 경우
        {
            enemyTarget.GetComponent<Defense>().SkillUse(character);
            return;//아무일도 하지 않고 상대방의 디펜스에서 처리한다.
        }
        else if(enemyTarget.currentSkillId == Define.SkillState.Counter)
        {
            enemyTarget.GetComponent<CounterAttack>().SkillUse(character);
            return;//아무일도 하지 않고 상대방의 카운터에서 처리한다.
        }
        else
        {
            character.AniOff();
            ani.SetBool("Combat", true);
            enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower, 
            skillData.Coefficient, character.balance, skillData.StiffnessTime, skillData.DownGauge);
        }        
    }
}
