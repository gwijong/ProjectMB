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
            case Define.Item.Fruit: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Fruit").Width, Resources.Load<ItemData>("Data/ItemData/Fruit").Height); //나무열매는 1*1이다
            case Define.Item.Wool: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Wool").Width, Resources.Load<ItemData>("Data/ItemData/Wool").Height);  //양털은 2*2이다
            case Define.Item.Egg: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Egg").Width, Resources.Load<ItemData>("Data/ItemData/Egg").Height);  //달걀은 1*1이다
            case Define.Item.Firewood: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Firewood").Width, Resources.Load<ItemData>("Data/ItemData/Firewood").Height);  //나무장작 1*3이다
            case Define.Item.LifePotion: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/LifePotion").Width, Resources.Load<ItemData>("Data/ItemData/LifePotion").Height);  //생명력물약은 1*1이다
            case Define.Item.ManaPotion: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/ManaPotion").Width, Resources.Load<ItemData>("Data/ItemData/ManaPotion").Height);  //마나물약은 1*1이다
            case Define.Item.Bottle: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Bottle").Width, Resources.Load<ItemData>("Data/ItemData/Bottle").Height);  //빈병은 1*2이다
            case Define.Item.BottleWater: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/BottleWater").Width, Resources.Load<ItemData>("Data/ItemData/BottleWater").Height);  //물병은 1*2이다
            default : return Vector2Int.one;
        }
    }

    /// <summary>Define.Item.GetItemImage(); 하면 해당 아이템 스프라이트 반환 </summary>
    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").ItemSprite; //나무열매
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").ItemSprite;  //양털
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").ItemSprite;  //달걀
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/FireWood").ItemSprite;  //나무장작
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").ItemSprite;  //생명력물약
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").ItemSprite;  //마나물약
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").ItemSprite;  //빈병
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").ItemSprite;  //물병
            default: return null;
        }
    }

    /// <summary>Define.Item.GetMaxStack(); 하면 해당 아이템 최대 겹칠 수 있는 수 반환 </summary>
    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").Stack; //나무열매는 5개 겹칠 수 있다
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").Stack;  //양털은 5개 겹칠 수 있다
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").Stack;  //달걀은 5개 겹칠 수 있다
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood").Stack;  //나무장작은 5개 겹칠 수 있다
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").Stack;  //생명력 물약은 10개 겹칠 수 있다
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").Stack;  //마나 물약은 10개 겹칠 수 있다
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").Stack;  //빈병은 겹칠 수 없다
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").Stack;  //물병은 겹칠 수 없다
            default: return 1;
        }
    }
}
