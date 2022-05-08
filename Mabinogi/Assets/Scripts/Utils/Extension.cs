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

    /// <summary>Define.Item.GetSize(); 하면 해당 아이템 사이즈 반환 </summary>
    public static Vector2Int GetSize(this Define.Item item)
    {
        switch (item)
        {          
            case Define.Item.Fruit: return new Vector2Int(1, 1); //나무열매는 1*1이다
            case Define.Item.Wool: return new Vector2Int(2, 2);  //양털은 2*2이다

            default : return Vector2Int.one;
        }
    }

    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<Sprite>("UI/Inventory/Fruit"); //나무열매는 1*1이다
            case Define.Item.Wool: return Resources.Load<Sprite>("UI/Inventory/Wool");  //양털은 2*2이다

            default: return null;
        }
    }

    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return 1; //나무열매는 1*1이다
            case Define.Item.Wool: return 5;  //양털은 2*2이다

            default: return 1;
        }
    }
}
