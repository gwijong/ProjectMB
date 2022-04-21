using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//            반환값      델리게이트 이름    매개변수
public delegate bool winnerCheckDelegate(Skill other);

//둘 다 서로를 때리려고 달려올 때에만 경합해라!
//아니면 그냥 바로 써라!
public class Skill
{
    public winnerCheckDelegate WinnerCheck;//경합에서 이기면 트루 지면 펄스

    public Define.SkillState type;

    public float castingTime;

    public bool mustCheck; //대상을 지정한 상태가 아니라고 하더라도 무조건 경합 확인S

    public Skill(Define.SkillState wantType, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false)
    {
        type = wantType;
        castingTime = wantCastingTime;
        WinnerCheck = wantWinnerCheck;
        mustCheck = wantMustCheck;
    }

    //                                              타입,       시전시간,    이기는 거 체크,    무조건체크여부
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, 0.0f, CombatNormal);
    public static Skill smash           = new Skill(Define.SkillState.Smash, 1.0f, CombatBreakDefense);
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, 1.5f, AllwaysWinner, true);
    public static Skill defense         = new Skill(Define.SkillState.Defense, 1.0f, LoseForSmash, true);

    static bool CombatNormal(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    static bool CombatBreakDefense(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    static bool LoseForSmash(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false;
            default: return true;
        };
    }

    static bool AllwaysWinner(Skill other)
    {
        return true;
    }
}

//이거는 스킬 하나야!
public class SkillInfo
{
    public Skill skill;
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

    SkillInfo[] skills;
    static CharacterSkill dogSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/DogSkill");
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;

            return skills[index]; 
        }
    }

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

    public SkillList(SkillInfo[] value) { skills = value; }

    public static SkillList dog = new SkillList(new SkillInfo[]
    {       
        new SkillInfo(Define.SkillState.Combat, dogSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, dogSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, dogSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, dogSkill.CounterRank),
    });
    public static SkillList chicken;

    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 4),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });
    //스타트에서 스킬리스트를 new 붙여서 인스턴스 만들어서 json 파일 안의 정보로 대입하세요
    //데이터 저장하고 불러올때는 스킬ID enum으로 가져옴 enum을 스위치 돌려서 SkillList 클래스에 넣음
}

