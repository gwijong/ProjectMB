
public class Define
{
    /// <summary> 마우스 입력값</summary>
    public enum mouseKey
    {
        LeftClick,
        rightClick,
        middleClick
    }
    /// <summary> 상호작용 타입</summary>
    public enum InteractType
    {
        None,
        Talk,
        Attack,
        Get,
        Sheeping,
    }
    /// <summary> 캐릭터의 상태</summary>
    public enum State
    {
        Die,
        Moving,
        Idle,
        Casting,
    }
    /// <summary> 캐릭터의 스킬 상태</summary>
    public enum SkillState
    {
        Combat = 0,
        Defense = 1,
        Smash = 2,
        Counter = 3,
    }

    /// <summary> 레이어 정의</summary>
    public enum Layer
    {
        Enemy = 6,
        Livestock = 7,
        Ground = 8,
        Block = 9,
        Tree = 10,
        Item = 11,
        NPC = 12,
        Player = 13,
    }

    /// <summary> 씬 정의</summary>
    public enum Scene
    {
        World,
    }

    /// <summary> 이동 상태 </summary>
    public enum MoveState
    {  //속도만 다르게 해줌
       //걷고 뛰는건 Character 에서 해줌
        Rooted,
        Walkable,
        Runnable,
    }
    
    /// <summary> 아이템 종류 </summary>
    public enum Item
    {
        None,
        Fruit,
        Bottle,
        BottleWater,
        Egg,
        LifePotion,
        ManaPotion,
        Wool,
        Firewood,
    }
}
