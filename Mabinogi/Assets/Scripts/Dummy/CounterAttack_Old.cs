using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack_Old : Skill_Old
{
    /*
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
    */
    public override void SkillUse(Character_Old enemyTarget)
    {
        character.AniOff();
        ani.SetBool("Counter", true);
        enemyTarget.Groggy(skillData.StiffnessTime);
        enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower,
        skillData.Coefficient, character.balance);
    }
}
