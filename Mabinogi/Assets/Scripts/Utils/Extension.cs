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
            case Define.SkillState.Counter: return Skill.counterAttack; //카운터
            case Define.SkillState.Defense: return Skill.defense; //디펜스
            case Define.SkillState.Smash:   return Skill.smash;  //스매시
            default:                        return Skill.combatMastery; //근접 공격
        }
    }

    /// <summary>Define.Item.GetSize(); 하면 해당 아이템 사이즈 반환 </summary>
    public static Vector2Int GetSize(this Define.Item item)
    {
        switch (item)
        {          
            case Define.Item.Fruit: return new Vector2Int(1, 1); //나무열매는 1*1이다
            case Define.Item.Wool: return new Vector2Int(2, 2);  //양털은 2*2이다
            case Define.Item.Egg: return new Vector2Int(1, 1);  //달걀은 1*1이다
            case Define.Item.Firewood: return new Vector2Int(1, 3);  //나무장작 1*3이다
            case Define.Item.LifePotion: return new Vector2Int(1, 1);  //생명력물약은 1*1이다
            case Define.Item.ManaPotion: return new Vector2Int(1, 1);  //마나물약은 1*1이다
            case Define.Item.Bottle: return new Vector2Int(1, 2);  //빈병은 1*2이다
            case Define.Item.BottleWater: return new Vector2Int(1, 2);  //물병은 1*2이다
            default : return Vector2Int.one;
        }
    }

    /// <summary>Define.Item.GetItemImage(); 하면 해당 아이템 스프라이트 반환 </summary>
    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<Sprite>("UI/Inventory/Fruit"); //나무열매
            case Define.Item.Wool: return Resources.Load<Sprite>("UI/Inventory/Wool");  //양털
            case Define.Item.Egg: return Resources.Load<Sprite>("UI/Inventory/Egg");  //달걀
            case Define.Item.Firewood: return Resources.Load<Sprite>("UI/Inventory/FireWood");  //나무장작
            case Define.Item.LifePotion: return Resources.Load<Sprite>("UI/Inventory/LifePotion");  //생명력물약
            case Define.Item.ManaPotion: return Resources.Load<Sprite>("UI/Inventory/ManaPotion");  //마나물약
            case Define.Item.Bottle: return Resources.Load<Sprite>("UI/Inventory/Bottle");  //빈병
            case Define.Item.BottleWater: return Resources.Load<Sprite>("UI/Inventory/BottleWater");  //물병
            default: return null;
        }
    }

    /// <summary>Define.Item.GetMaxStack(); 하면 해당 아이템 최대 겹칠 수 있는 수 반환 </summary>
    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return 5; //나무열매는 5개 겹칠 수 있다
            case Define.Item.Wool: return 5;  //양털은 5개 겹칠 수 있다
            case Define.Item.Egg: return 5;  //달걀은 5개 겹칠 수 있다
            case Define.Item.Firewood: return 5;  //나무장작은 5개 겹칠 수 있다
            case Define.Item.LifePotion: return 10;  //생명력 물약은 10개 겹칠 수 있다
            case Define.Item.ManaPotion: return 10;  //마나 물약은 10개 겹칠 수 있다
            case Define.Item.Bottle: return 1;  //빈병은 겹칠 수 없다
            case Define.Item.BottleWater: return 1;  //물병은 겹칠 수 없다
            default: return 1;
        }
    }
}
