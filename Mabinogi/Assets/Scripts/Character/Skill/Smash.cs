using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Skill
{
    public override void SkillUse(Character enemyTarget)
    {
        if (enemyTarget.currentSkillId ==Define.SkillState.Counter)
        {
            enemyTarget.GetComponent<CounterAttack>().SkillUse(character);
            return;//아무일도 하지 않고 상대방의 카운터에서 처리한다.
        }
        character.AniOff();
        ani.SetBool("Smash", true);
        enemyTarget.Groggy(skillData.StiffnessTime);
        enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower,
        skillData.Coefficient, character.balance);
    }
}
