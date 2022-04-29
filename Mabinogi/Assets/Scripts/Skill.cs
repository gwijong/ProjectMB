using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 승리자 체크 델리게이트</summary>
//            반환값      델리게이트 이름    매개변수
public delegate bool winnerCheckDelegate(Skill other);

/// <summary> 둘 다 서로를 때리려고 달려오면 경합하고 아니면 바로 스킬 사용</summary>
public class Skill
{
    /// <summary> 경합에서 이기면 트루</summary>
    public winnerCheckDelegate WinnerCheck;//경합에서 이기면 트루 지면 펄스

    /// <summary> 스킬 타입</summary>
    public Define.SkillState type;

    public string AnimName;

    /// <summary> 스킬 시전 시간</summary>
    public float castingTime;

    /// <summary> 무조건 경합 체크</summary>
    public bool mustCheck;
    /// <summary> 공격 불가 스킬일 경우</summary>
    public bool cannotAttack;

    /// <summary> Skill 클래스 생성자</summary>
    public Skill(Define.SkillState wantType, string wantAnimName, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false, bool wantCannotAttack = false)
    {
        type = wantType;
        castingTime = wantCastingTime;
        WinnerCheck = wantWinnerCheck;
        mustCheck = wantMustCheck;
        cannotAttack = wantCannotAttack;
        AnimName = wantAnimName;
    }

    //                                                               타입,시전시간,이기는 거 체크,무조건체크여부
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, "Combat", 0.0f, CombatWinCheck);
    public static Skill smash           = new Skill(Define.SkillState.Smash, "Smash", 1.0f, SmashWinCheck);
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, "Counter", 1.5f, CounterWinCheck, true, true);
    public static Skill defense         = new Skill(Define.SkillState.Defense, "Defense", 1.0f, DefenseWinCheck, true, true);

    /// <summary> 근접 평타가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool CombatWinCheck(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> 스매시가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool SmashWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Combat:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> 디펜스가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool DefenseWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false;
            default: return true;
        };
    }

    /// <summary> 카운터가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool CounterWinCheck(Skill other)
    {
        return true;
    }
}

/// <summary> 스킬 하나</summary>
public class SkillInfo
{
    /// <summary> 스킬</summary>
    public Skill skill;
    /// <summary> 스킬 랭크</summary>
    public int rank;

    public SkillInfo(Define.SkillState wantType, int wantRank) 
    {
        skill = wantType.GetSkill();
        rank = wantRank;
    }
}


/// <summary> 내가 배운 스킬</summary>
public class SkillList
{
    /// <summary> 내가 배운 스킬들</summary>
    SkillInfo[] skills;
    /// <summary> 개의 스킬 정보</summary>
    static CharacterSkill dogSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/DogSkill"); 
    static CharacterSkill playerSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/PlayerSkill");
    /// <summary> 스킬 정보 하나 인덱스로 사용</summary>
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;  //스킬 범위를 넘어가는 경우 예외처리

            return skills[index]; 
        }
    }
    /// <summary> 스킬 정보 하나 Define.SkillState 오버로드</summary>
    public SkillInfo this[Define.SkillState type]
    {
        get
        {
            foreach(SkillInfo current in skills)
            {
                if (current.skill.type == type) return current;
            };

            return null;
        }
    }

    /// <summary> 내가 배운 스킬들을 SkillInfo배열에서 가져와 대입</summary>
    public SkillList(SkillInfo[] value) 
    { 
        skills = value;
    }

    /// <summary> 개가 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList dog = new SkillList(new SkillInfo[]
    {       
        new SkillInfo(Define.SkillState.Combat, dogSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, dogSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, dogSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, dogSkill.CounterRank),
    });

    public static SkillList player = new SkillList(new SkillInfo[]
{
        new SkillInfo(Define.SkillState.Combat, playerSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, playerSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, playerSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, playerSkill.CounterRank),
});
    /// <summary> 암탉이 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList hen = new SkillList(new SkillInfo[]
{
        new SkillInfo(Define.SkillState.Combat, playerSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, playerSkill.DefenseRank),
});

    /// <summary> 늑대가 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 4),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });

}

