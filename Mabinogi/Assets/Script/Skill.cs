using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//둘 다 서로를 때리려고 달려올 때에만 경합해라!
//아니면 그냥 바로 써라!
public class Skill
{
    public float castingTime;
    
    public virtual bool mustCheck() { return false; } //대상을 지정한 상태가 아니라고 하더라도 무조건 경합 확인
    public virtual bool WinnerCheck(Skill other) { return true; }//경합에서 이기면 트루 지면 펄스
}

public class BattleSkill : Skill
{
    public override bool WinnerCheck(Skill other) 
    {
        System.Type otherType = other.GetType();

        if (otherType == typeof(Defense))
        {
            return false;
        }
        else if(otherType == typeof(Counter))
        {
            return false;
        };

        return true;
    }
}

public class CombatMastery : BattleSkill 
{
    public override bool WinnerCheck(Skill other)
    {
        return true;
    }
}
public class Smash : BattleSkill 
{
    public override bool WinnerCheck(Skill other)
    {
        System.Type otherType = other.GetType();

        if (otherType == typeof(CombatMastery))
        {
            return false;
        }
        else if (otherType == typeof(Counter))
        {
            return false;
        };

        return true;
    }
}
public class Defense : BattleSkill //추가 구현하세요
{
    public override bool mustCheck() { return true; }
}
public class Counter : BattleSkill
{
    public override bool mustCheck() { return true; }
}

//이거는 스킬 하나야!
public class SkillInfo
{
    public Skill type;
    public int rank;
}

//JSON으로 만들어서
//처음에 시작할 때 dogSkill도 초기화해주고
//각 static 안에 있는 내용을 초기화해줘!
//매니저같은 거에서 초기화해주면 돼! 초기화해줄 때 new를 붙여서 꼭 해!
public class SkillList
{
    SkillInfo[] skills;

    public static SkillList dog;
    public static SkillList chicken;
    public static SkillList wolf;
    //스타트에서 스킬리스트를 new 붙여서 인스턴스 만들어서 json 파일 안의 정보로 대입하세요
    //데이터 저장하고 불러올때는 스킬ID enum으로 가져옴 enum을 스위치 돌려서 SkillList 클래스에 넣음
}

