using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum SkillId
    {
        combat = 0,
        defense = 1,
        smash = 2,
        counter = 3
    }

    public CharacterData characterData;

    /// <summary> 생명력. 피격시 감소 </summary>
    public int hitPoint;
    /// <summary> 최대 생명력 </summary>
    public int maxHitPoint;
    /// <summary> 마나. 마법 시전시 감소 </summary>
    public int manaPoint;
    /// <summary> 최대 마나 </summary>
    public int maxManaPoint;
    /// <summary> 스태미나. 스킬 시전시 감소 </summary>
    public int staminaPoint;
    /// <summary> 최대 스태미나 </summary>
    public int maxStaminaPoint;
    /// <summary> 체력. 물리 공격력에 영향을 줌 </summary>
    public int strength;
    /// <summary> 지력. 마법공격력에 영향을 줌 </summary>
    public int intelligence;
    /// <summary> 솜씨. 밸런스에 영향을 줌 </summary>
    public int dexterity;
    /// <summary> 의지. 죽음을 이겨내고 데들리 상태가 될 확률이 증가한다 </summary>
    public int will;
    /// <summary> 행운. 치명타 확률에 영향을 줌 </summary>
    public int luck;
    /// <summary> 최대물리공격력 </summary>
    public int maxPhysicalStrikingPower;
    /// <summary> 최대마법공격력 </summary>
    public int maxMagicStrikingPower;
    /// <summary> 최소물리공격력 </summary>
    public int minPhysicalStrikingPower;
    /// <summary> 최소마법공격력 </summary>
    public int minMagicStrikingPower;
    /// <summary> 캐릭터가 입은 부상 </summary>
    public int wound;
    /// <summary> 공격시 상대방에게 입히는 부상률 </summary>
    public int woundAttack;
    /// <summary> 치명타 확률 </summary>
    public float critical;
    /// <summary> 밸런스, 최소, 최대 데미지가 뜨는 비율 </summary>
    public float balance;
    /// <summary> 물리 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    public int physicalDefensivePower;
    /// <summary> 마법 방어력. 1대1 비율로 고정적인 피해량 감소 </summary>
    public int magicDefensivePower;
    /// <summary> 물리 보호. 1퍼센트 단위로 데미지 감소 </summary>
    public int physicalProtective;
    /// <summary> 마법 보호. 1퍼센트 단위로 데미지 감소 </summary>
    public int magicProtective;
    /// <summary> 사망할 공격을 당했을 때 이겨내고 데들리 상태가 될 확률 </summary>
    public int deadly;
    /// <summary> 이동 속도 </summary>
    public int speed;
    /// <summary> 다운 게이지 </summary>
    public int downGauge = 0;
    /// <summary> 다운된 시간 </summary>
    public float downTime = 2.0f;
    /// <summary> 경직 체크 </summary>
    public int stiffnessCount = 0;
    /// <summary> 시전된 스킬 </summary>
    public int currentSkillId;
    /// <summary> 상대 타겟 캐릭터 </summary>
    public Character target;
    /// <summary> 스킬 시전 코루틴 </summary>
    protected IEnumerator cast;
    /// <summary> 경직 시간 코루틴 </summary>
    protected IEnumerator stiffness;
    protected bool die = false;
    protected Animator ani;
    protected Rigidbody rigid;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        hitPoint = characterData.HitPoint;
        maxHitPoint = characterData.HitPoint;
        manaPoint = characterData.ManaPoint;
        maxManaPoint = characterData.ManaPoint;
        staminaPoint = characterData.StaminaPoint;
        maxStaminaPoint = characterData.StaminaPoint;
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

    protected void SkillCast(Skill skill) //스킬 준비 시도
    {
        if(staminaPoint>= skill.skillData.CastCost && stiffnessCount == 0)  //스태미나가 스킬 준비 스태미나보다 많거나 같은 경우
        {
            StopCoroutine(cast);
            staminaPoint -= skill.skillData.CastCost; //스태미나 감소
            cast = Casting(skill.skillData.CastTime, skill.skillData.SkillId);
            StartCoroutine(cast); // 스킬 시전 코루틴 실행
        }
    }

    protected void SkillCancel()//스킬 캔슬
    {
        if (cast != null)
        {
            StopCoroutine(cast);
        }
        currentSkillId = 0;
    }

    public void Down()//캐릭터 다운 처리
    {
        if (die) //사망한 경우이므로 리턴
        {
            return;
        }
        else
        {
            AniOff();
            ani.SetBool("Down", true);
            Stiffness(downTime);  //downTime만큼 경직
        }
    }
    public void DownCheck()
    {
        if (downGauge >= 100) //다운게이지가 100이 넘으면 다운
        {
            Down();
            downGauge = 0;
        }
    }

    public void Freeze(float time)//디펜스로 막힐 경우 경직
    {
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, 
        float enemyBalance, float enemyStiffnessTime, int enemyAttackDownGauge)//평타 피격 처리
    {
        AniOff();
        ani.SetBool("Hit", true);
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        hitPoint = hitPoint - (int)hitDamage;
        if (hitPoint <= 0)
        {
            Die();
            return;
        }
        stiffness = Stiffness(enemyStiffnessTime);
        StartCoroutine(stiffness);
        downGauge = downGauge + enemyAttackDownGauge;
        DownCheck();
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, float enemyBalance)//다운되는 스킬 피격 처리
    {
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        hitPoint = hitPoint - (int)hitDamage;
        if (hitPoint <= 0)
        {
            Die();
            return;
        }
    }

    public void Groggy(float time)//적에게 스매시나 카운터 맞을 경우 그로기 상태
    {
        AniOff();
        ani.SetBool("Groggy", true);
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
        Invoke("Down", 0.2f);
    }
    protected void Die()//생명력이 0 이하일 경우 사망 처리
    {
        AniOff();
        ani.SetBool("Die", true);
        downGauge = 100;
        Down();
    }

    public IEnumerator Stiffness(float time)//경직 시간 코루틴
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

    public void AniOff()
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
        {
            ani.SetBool(parameter.name, false);
        }
    }
    
}
