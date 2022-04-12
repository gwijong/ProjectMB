using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;
    public SkillData combat;
    public SkillData defense;
    public SkillData smash;
    public SkillData counterAttack;

    /// <summary> 생명력. 피격시 감소 </summary>
    protected int hitPoint;
    /// <summary> 마나. 마법 시전시 감소 </summary>
    protected int manaPoint;
    /// <summary> 스태미나. 스킬 시전시 감소 </summary>
    protected int staminaPoint;
    /// <summary> 체력. 물리 공격력에 영향을 줌 </summary>
    protected int strength;
    /// <summary> 지력. 마법공격력에 영향을 줌 </summary>
    protected int intelligence;
    /// <summary> 솜씨. 밸런스에 영향을 줌 </summary>
    protected int dexterity;
    /// <summary> 의지. 죽음을 이겨내고 데들리 상태가 될 확률이 증가한다 </summary>
    protected int will;
    /// <summary> 행운. 치명타 확률에 영향을 줌 </summary>
    protected int luck;
    /// <summary> 최대물리공격력 </summary>
    protected int maxPhysicalStrikingPower;
    /// <summary> 최대마법공격력 </summary>
    protected int maxMagicStrikingPower;
    /// <summary> 최소물리공격력 </summary>
    protected int minPhysicalStrikingPower;
    /// <summary> 최소마법공격력 </summary>
    protected int minMagicStrikingPower;
    /// <summary> 캐릭터가 입은 부상 </summary>
    protected int wound;
    /// <summary> 공격시 상대방에게 입히는 부상률 </summary>
    protected int woundAttack;
    /// <summary> 치명타 확률 </summary>
    protected float critical;
    /// <summary> 밸런스, 최소, 최대 데미지가 뜨는 비율 </summary>
    protected float balance;
    /// <summary> 물리 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    protected int physicalDefensivePower;
    /// <summary> 마법 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    protected int magicDefensivePower;
    /// <summary> 물리 보호. 1퍼센트 단위로 데미지 감소 </summary>
    protected int physicalProtective;
    /// <summary> 마법 보호. 1퍼센트 단위로 데미지 감소 </summary>
    protected int magicProtective;
    /// <summary> 사망할 공격을 당했을 때 이겨내고 데들리 상태가 될 확률 </summary>
    protected int deadly;
    /// <summary> 이동 속도 </summary>
    protected int speed;
    /// <summary> 다운 게이지 </summary>
    protected int downGauge = 0;
    /// <summary> 다운된 시간 </summary>
    protected float downTime = 2.0f;
    /// <summary> 그로기 시간 </summary>
    protected float groggyTime = 0.5f;
    /// <summary> 경직 체크 </summary>
    protected int stiffnessCount = 0;
    /// <summary> 시전된 스킬 </summary>
    public int currentSkillId;
    /// <summary> 상대 타겟 캐릭터 </summary>
    public Character target;
    /// <summary> 스킬 시전 코루틴 </summary>
    protected IEnumerator cast;
    /// <summary> 경직 시간 코루틴 </summary>
    protected IEnumerator stiffness;

    private void Awake()
    {
        hitPoint = characterData.HitPoint;
        manaPoint = characterData.ManaPoint;
        staminaPoint = characterData.StaminaPoint;
        strength = characterData.Strength;
        intelligence = characterData.Intelligence;
        dexterity = characterData.Dexterity;
        will = characterData.Will;
        luck = characterData.Luck;
        maxPhysicalStrikingPower = characterData.MaxPhysicalStrikingPower;
        maxMagicStrikingPower = characterData.MaxMagicStrikingPower;
        minPhysicalStrikingPower = characterData.MinPhysicalStrikingPower;
        minMagicStrikingPower = characterData.MinMagicStrikingPower;
        wound = characterData.Wound;
        woundAttack = characterData.WoundAttack;
        critical = characterData.Critical;
        balance = characterData.Balance;
        physicalDefensivePower = characterData.PhysicalDefensivePower;
        magicDefensivePower = characterData.MagicDefensivePower;
        physicalProtective = characterData.PhysicalProtective;
        magicProtective = characterData.MagicProtective;
        deadly = characterData.Deadly;
        speed = characterData.Speed;

    }
    protected void Move(Vector3 destination)//캐릭터 이동
    {

    }

    protected void SkillCast(SkillData skilldata) //스킬 준비 시도
    {
        if(staminaPoint>= skilldata.CastCost && stiffnessCount == 0)  //스태미나가 스킬 준비 스태미나보다 많거나 같은 경우
        {
            StopCoroutine(cast);
            staminaPoint -= skilldata.CastCost; //스태미나 감소
            cast = Casting(skilldata.CastTime, skilldata.SkillId);
            StartCoroutine(cast); // 스킬 시전 코루틴 실행
        }
    }

    protected void SkillCancel()//스킬 캔슬
    {
        StopCoroutine(cast);
        currentSkillId = 0;
    }

    protected void Combat(SkillData combat)//타겟에 평타 공격 시전
    {
        target.downGauge += combat.DownGauge;
    }

    protected void CounterAttack(SkillData counterAttack)//타겟에 카운터 어택 시전
    {
        target.downGauge += counterAttack.DownGauge;
    }
    protected void Defense(SkillData defense)//타겟에게 디펜스 시전
    {
        target.Freeze(defense.Coefficient);//디펜스 스킬 계수만큼 경직
    }
    protected void Smash(SkillData smash)//타겟에 스매시 시전
    {
        Groggy(groggyTime);
        target.downGauge += smash.DownGauge;

    }
    public void Down()//캐릭터 다운 처리
    {
        if (downGauge >= 100) //다운게이지가 100이상일 경우는 사망한 경우이므로 리턴
        {
            return;
        }
        else
        {
            Stiffness(downTime);  //downTime만큼 경직
        }
    }
    public void Freeze(float time)//디펜스로 막힐 경우나 카운터 어택에 당한 경우 경직 처리
    {
        Stiffness(time);
    }

    public void Hit(Character enemy, int damage)//피격 처리
    {
        target = enemy;
        if (target.currentSkillId == 0 && currentSkillId == 1)
        {
            Defense(defense);
        }
        else
        {
            if (hitPoint <= 0)
            {
                Die();
                return;
            }
            SkillCancel();
            float stiffnessTime = 0;
            switch (target.currentSkillId)
            {
                case 0:
                    stiffnessTime = target.combat.StiffnessTime;
                    break;
                case 1:
                    stiffnessTime = target.defense.StiffnessTime;
                    break;
                case 2:
                    stiffnessTime = target.smash.StiffnessTime;
                    Groggy(groggyTime);
                    break;
                case 3:
                    stiffnessTime = target.counterAttack.StiffnessTime;
                    break;
            }
            stiffness = Stiffness(stiffnessTime);
            StartCoroutine(stiffness);
        }
        if (downGauge >= 100)
        {
            Down();
            downGauge = 0;
        }
    }
    public void Groggy(float time)//적에게 스매시를 맞을 경우 그로기 상태
    {
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
    }
    protected void Die()//생명력이 0 이하일 경우 사망 처리
    {
        downGauge = 100;
        Down();
    }

    IEnumerator Stiffness(float time)//경직 시간 코루틴
    {
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
    }

    IEnumerator Casting(float time, int skillId)//스킬 시전 시간 코루틴
    {
        yield return new WaitForSeconds(time);
        currentSkillId = skillId;
    }
}
