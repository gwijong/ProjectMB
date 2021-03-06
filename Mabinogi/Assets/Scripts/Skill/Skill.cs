using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> 스킬 경합시 승리자 체크 델리게이트</summary>
//            반환값      델리게이트 이름    매개변수
public delegate bool winnerCheckDelegate(Skill other);

/// <summary> 스킬종류,시전시간,이기는스킬,경합,선제공격,트리거</summary>
public class Skill
{
    /// <summary> 경합에서 이기면 트루</summary>
    public winnerCheckDelegate WinnerCheck;//경합에서 이기면 트루 지면 펄스
    /// <summary> 스킬 타입</summary>
    public Define.SkillState type;
    /// <summary> 애니메이터 파라미터 Trigger 이름</summary>
    public string AnimName;
    /// <summary> 스킬 시전 시간</summary>
    public float castingTime;
    /// <summary> 반드시 스킬 경합 체크</summary>
    public bool mustCheck;
    /// <summary>선제 공격 불가 스킬일 경우</summary>
    public bool cannotAttack;

    /// <summary> Skill 클래스 생성자</summary>
    public Skill(Define.SkillState wantType, string wantAnimName, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false, bool wantCannotAttack = false)
    {
        type = wantType; //스킬 타입
        castingTime = wantCastingTime; //스킬 시전 시간
        WinnerCheck = wantWinnerCheck;  //스킬 경합시 이기는 스킬 체크
        mustCheck = wantMustCheck;  //반드시 스킬 경합 체크
        cannotAttack = wantCannotAttack; //선제공격불가 스킬
        AnimName = wantAnimName; // Trigger 이름
    }

    /// <summary> Skill 클래스 combatMastery 객체 생성</summary>
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, "Combat", Define.SkillState.Combat.GetSkillData().CastTime, CombatWinCheck);
    /// <summary> Skill 클래스 smash 객체 생성</summary>
    public static Skill smash           = new Skill(Define.SkillState.Smash, "Smash", Define.SkillState.Smash.GetSkillData().CastTime, SmashWinCheck);
    /// <summary> Skill 클래스 counterAttack 객체 생성</summary>
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, "Counter", Define.SkillState.Counter.GetSkillData().CastTime, CounterWinCheck, true, true);
    /// <summary> Skill 클래스 defense 객체 생성</summary>
    public static Skill defense         = new Skill(Define.SkillState.Defense, "Defense", Define.SkillState.Defense.GetSkillData().CastTime, DefenseWinCheck, true, true);
    /// <summary> Skill 클래스 windmill 객체 생성</summary>
    public static Skill windmill        = new Skill(Define.SkillState.Windmill, "Windmill", Define.SkillState.Windmill.GetSkillData().CastTime, WindmillWinCheck, false, true);
    /// <summary> Skill 클래스 icebolt 객체 생성</summary>
    public static Skill icebolt = new Skill(Define.SkillState.Icebolt, "MagicProcessing", Define.SkillState.Icebolt.GetSkillData().CastTime, IceboltWinCheck, false, false);

    /// <summary> 근접 평타가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool CombatWinCheck(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:  //상대방의 스킬이 디펜스나 카운터일 경우 지므로 false 반환
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> 스매시가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool SmashWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Combat:  //상대방의 스킬이 컴벳이나 카운터일 경우 지므로 false 반환
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> 디펜스가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool DefenseWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false; //상대방의 스킬이 스매시일 경우 지므로 false 반환
            default: return true;
        };
    }

    /// <summary> 카운터가 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool CounterWinCheck(Skill other) //카운터는 언제나 이긴다
    {
        switch (other.type)
        {
            case Define.SkillState.Windmill: return false; //상대방의 스킬이 윈드밀일 경우 지므로 false 반환
            default: return true;
        };
    }

    /// <summary> 윈드밀이 이기는 경우는 true 지는 경우 false 반환</summary>
    static bool WindmillWinCheck(Skill other) //윈드밀은 디펜스를 제외하고 언제나 이긴다
    {
        switch (other.type)
        {
            case Define.SkillState.Defense: return false; //상대방의 스킬이 디펜스일 경우 지므로 false 반환
            default: return true;
        };
    }

    static bool IceboltWinCheck(Skill other) //아이스볼트는 디펜스를 제외하고 언제나 이긴다
    {
        switch (other.type)
        {
            case Define.SkillState.Defense: return false; //상대방의 스킬이 디펜스일 경우 지므로 false 반환
            default: return true;
        };
    }
}

/// <summary> 스킬 하나 정보(스킬상태, 스킬랭크)</summary>
public class SkillInfo
{
    /// <summary> 스킬</summary>
    public Skill skill;
    /// <summary> 스킬 랭크</summary>
    public int rank;

    /// <summary> SkillInfo 생성자</summary>
    public SkillInfo(Define.SkillState wantType, int wantRank) 
    {
        skill = wantType.GetSkill(); //원하는 스킬의 Skill 클래스 객체 가져옴
        rank = wantRank; //스킬 랭크는 데미지 계산이나 스킬 시전시간 등의 계산에 쓰임
    }
}


/// <summary> 내가 배운 스킬</summary>
public class SkillList
{
    /// <summary> 내가 배운 스킬들</summary>
    SkillInfo[] skills;
    //    프로퍼티 이름이 본인 이름이다 그래서 그냥 배열처럼 해당 SkillInfo에 대괄호[] 붙여서 사용한다
    /// <summary> 스킬 정보 하나 인덱스로 사용</summary>
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;  //스킬 범위를 넘어가는 경우 예외처리

            return skills[index]; 
        }
    }
    //    프로퍼티 이름이 본인 이름이다 그래서 그냥 배열처럼 해당 SkillInfo에 대괄호[] 붙여서 사용한다
    /// <summary> 스킬 정보 하나 Define.SkillState 오버로드</summary>
    public SkillInfo this[Define.SkillState type]
    {
        get
        {
            foreach(SkillInfo current in skills)
            {
                if (current.skill.type == type) return current;
            };

            return null; //가은 스킬타입 못 찾으면 null 반환
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
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });

    /// <summary> 플레이어가 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList player = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
        new SkillInfo(Define.SkillState.Windmill, 1),
        new SkillInfo(Define.SkillState.Icebolt, 1),
    });
    /// <summary> 암탉이 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList hen = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
    });
    /// <summary> 수탉이 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList rooster = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
    });

    /// <summary> 여우가 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList fox = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
    });
    /// <summary> 양이 가진 스킬들을 스킬 리스트에 대입</summary>
    /// 
    public static SkillList sheep = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
    });
    /// <summary> 늑대가 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });

    /// <summary> 곰이 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList bear = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });

    /// <summary> 골렘이 가진 스킬들을 스킬 리스트에 대입</summary>
    public static SkillList golem = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 1),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
    });
}

