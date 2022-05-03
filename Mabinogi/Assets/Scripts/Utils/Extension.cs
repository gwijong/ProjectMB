using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>wantType.GetSkill(); 하면 해당하는 스킬을 돌려줌</summary>
    public static Skill GetSkill(this Define.SkillState from)
    {
        switch(from)
        {
            case Define.SkillState.Counter: return Skill.counterAttack;
            case Define.SkillState.Defense: return Skill.defense;
            case Define.SkillState.Smash:   return Skill.smash;
            default:                        return Skill.combatMastery;
        }
    }

    public static Vector2Int GetSize(this Define.Item item)
    {
        switch (item)
        {          
            case Define.Item.Bottle: return new Vector2Int(1, 2);
            case Define.Item.Wool: return new Vector2Int(2, 2);

            default : return Vector2Int.one;
        }
    }
}
