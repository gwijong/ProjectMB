
public class Define
{
    public enum mouseKey
    {
        LeftClick,
        rightClick,
        middleClick
    }
    public enum InteractType
    {
        None,
        Talk,
        Attack,
        Get,
    }
    public enum State
    {
        Die,
        Moving,
        Idle,
        Casting,
    }
    public enum SkillState
    {
        Combat = 0,
        Defense = 1,
        Smash = 2,
        Counter = 3,
    }

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
    public enum Scene
    {
        World,
    }
}
