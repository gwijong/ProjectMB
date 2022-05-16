using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>target이 compare과 똑같거나 서브클래스인지 확인</summary>
    public static bool IsClassOf(this System.Type target, System.Type compare)
    {
        return target == compare || target.IsSubclassOf(compare);
    }

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

    /// <summary>Define.SkillData.GetSkillData(); 하면 해당 스킬 데이터 반환</summary>
    public static SkillData GetSkillData(this Define.SkillState skill)
    {
        switch (skill)
        {
            case Define.SkillState.Combat: return Resources.Load<SkillData>("Data/SkillData/Combat"); //기본 근접공격 스킬 데이터 반환
            case Define.SkillState.Defense: return Resources.Load<SkillData>("Data/SkillData/Defense");  //디펜스 스킬 데이터 반환
            case Define.SkillState.Smash: return Resources.Load<SkillData>("Data/SkillData/Smash");  //스매시 스킬 데이터 반환
            case Define.SkillState.Counter: return Resources.Load<SkillData>("Data/SkillData/CounterAttack");  //카운터 스킬 데이터 반환      
            default: return Resources.Load<SkillData>("Data/SkillData/Combat"); //이상한 값 들어오면 기본 근접공격 스킬 데이터 반환
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

    /// <summary>Define.Item.MakePrefab(); 아이템 프리팹 생성후 아이템 게임오브젝트 반환 </summary>
    public static GameObject MakePrefab(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.None: //버릴 아이템 타입이 none이면 버릴 아이템이 없으므로 탈출
                return null;
            case Define.Item.Fruit:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Fruit")); //열매 아이템 생성
            case Define.Item.Wool:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Wool")); //양털 아이템 생성
            case Define.Item.Egg:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Egg")); //달걀 아이템 생성
            case Define.Item.Firewood:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Firewood")); //장작 아이템 생성
            case Define.Item.LifePotion:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/LifePotion")); //생명력 포션 아이템 생성
            case Define.Item.ManaPotion:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/ManaPotion")); //마나 포션 아이템 생성
            case Define.Item.Bottle:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Bottle")); //빈병 아이템 생성
            case Define.Item.BottleWater:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/BottleWater")); //물병 아이템 생성
            default: return null;
        };
    }

    /// <summary>Define.Item.GetItemName(); 아이템 이름 스트링 반환 </summary>
    public static string GetItemName(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").ItemName; //나무열매 이름
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").ItemName;  //양털 이름
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").ItemName;  //달걀 이름
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood").ItemName;  //나무장작 이름
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").ItemName;  //생명력 물약 이름
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").ItemName;  //마나 물약 이름
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").ItemName;  //빈병 이름
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").ItemName;  //물병 이름
            default: return ""; //그 외 아이템은 "" 스트링 반환
        }
    }

    /// <summary>Define.Item.GetItemData(); 아이템 스크립터블 오브젝트 데이터 반환 </summary>
    public static ItemData GetItemData(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit"); //나무열매 스크립터블 오브젝트
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool");  //양털 스크립터블 오브젝트
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg");  //달걀 스크립터블 오브젝트
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood");  //나무장작 스크립터블 오브젝트
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion");  //생명력 물약 스크립터블 오브젝트
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion");  //마나 물약 스크립터블 오브젝트
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle");  //빈병 스크립터블 오브젝트
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater");  //물병 스크립터블 오브젝트
            default: return null;
        }
    }
}
