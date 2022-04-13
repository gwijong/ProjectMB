using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Skill
{
    /*
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
    */
    public override void SkillUse(Character enemyTarget)
    {
        character.AniOff();
        ani.SetBool("Counter", true);
        enemyTarget.Groggy(skillData.StiffnessTime);
        enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower,
        skillData.Coefficient, character.balance);
    }
}
