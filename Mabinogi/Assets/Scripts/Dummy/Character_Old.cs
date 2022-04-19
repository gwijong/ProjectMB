using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGauge_Old
{
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
}
public class CharacterStatus_Old
{
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
}

public class CharacterSkill_Old
{
    public Skill_Old type;
    public int rank;
}

public class Character_Old : CharacterState_Old
{
    public CharacterData characterData;



    public CharacterGauge_Old gauge = new CharacterGauge_Old();
    public CharacterStatus_Old stat = new CharacterStatus_Old();
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
    /// <summary> 다운 게이지 </summary>
    public int downGauge = 0;
    /// <summary> 다운된 시간 </summary>
    public float downTime = 4.0f;
    /// <summary> 경직 체크 </summary>
    public int stiffnessCount = 0;
    /// <summary> 시전된 스킬 </summary>
    public Define.SkillState currentSkillId;
    /// <summary> 상대 타겟 캐릭터 </summary>
    public Character_Old target;
    /// <summary> 스킬 시전 코루틴 </summary>
    protected IEnumerator cast;
    /// <summary> 경직 시간 코루틴 </summary>
    protected IEnumerator stiffness;
    /// <summary> 피격 애니메이션용 변수 </summary>
    protected int hitCount = 0;

    protected Rigidbody rigid;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        
    }
    void OnEnable()
    {
        gauge.hitPoint = characterData.HitPoint;
        gauge.maxHitPoint = characterData.HitPoint;
        gauge.manaPoint = characterData.ManaPoint;
        gauge.maxManaPoint = characterData.ManaPoint;
        gauge.staminaPoint = characterData.StaminaPoint;
        gauge.maxStaminaPoint = characterData.StaminaPoint;
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
        
    }
    protected virtual void Update()
    {
        OffensiveCheck();
    }

    protected void SkillCast(Skill_Old skill) //스킬 준비 시도
    {
        if (State == Define.State.Die)
        {
            return;
        }

        if (gauge.staminaPoint>= skill.skillData.CastCost && stiffnessCount == 0)  //스태미나가 스킬 준비 스태미나보다 많거나 같은 경우
        {
            if (cast != null)
            {
                StopCoroutine(cast);
            }
            gauge.staminaPoint -= skill.skillData.CastCost; //스태미나 감소
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
        currentSkillId = Define.SkillState.Combat;
    }

    public void Down()//캐릭터 다운 처리
    {
        rigid.AddForce(gameObject.transform.forward *-600);
        rigid.AddForce(gameObject.transform.up * 200);
        AniOff();
        offensive = true;
        ani.SetBool("BlowawayA", true);
        StartCoroutine("BlowawayTimer");
        if (gauge.hitPoint <= 0)
        {
            Die();
            return;
        }
        stiffness = Stiffness(downTime);
        StartCoroutine(stiffness); //downTime만큼 경직
    }
    IEnumerator BlowawayTimer()
    {
        yield return new WaitForSeconds(0.05f);
        ani.SetBool("BlowawayA", false);
    }

    public void DownCheck()
    {
        if (downGauge >= 100 || gauge.hitPoint <= 0) //다운게이지가 100이 넘으면 다운
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
        if (State == Define.State.Die)
        {
            return;
        }
        AniOff();
        offensive = true;

        ani.SetBool("Hit" + ('A' + (hitCount++ % 2)), true);

        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        gauge.hitPoint -= (int)hitDamage;
        stiffness = Stiffness(enemyStiffnessTime);
        StartCoroutine(stiffness);
        downGauge += enemyAttackDownGauge;
        DownCheck();
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, float enemyBalance)//다운되는 스킬 피격 처리
    {
        if (State == Define.State.Die)
        {
            return;
        }
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        gauge.hitPoint -= (int)hitDamage;
    }

    public void Groggy(float time)//적에게 스매시나 카운터 맞을 경우 그로기 상태
    {
        AniOff();
        offensive = true;
        ani.SetBool("Groggy", true);
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
        Invoke("Down", 0.2f);
    }
    protected void Die()//생명력이 0 이하일 경우 사망 처리
    {
        State = Define.State.Die;       
    }

    public IEnumerator Stiffness(float time)//경직 시간 코루틴
    {
        offensive = true;
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
        if (stiffnessCount == 0&& State != Define.State.Die)
        {
            AniOff();
            offensive = true;
        }
    }

    IEnumerator Casting(float time, Define.SkillState skillId)//스킬 시전 시간 코루틴
    {
        yield return new WaitForSeconds(time);
        currentSkillId = skillId;
    }

    public override void AniOff()
    {
        base.AniOff();
    }

    public void OffensiveCheck()
    {
        ani.SetBool("Offensive", offensive);

        if (target != null && target.State != Define.State.Die)
        {
            offensive = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && offensive == true)
        {
            offensive = false;
            target = null;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && offensive == false)
        {
            offensive = true;
        }
    }

    public void Attack()
    {
        switch (currentSkillId)
        {
            case Define.SkillState.Combat:
                offensive = true;
                GetComponent<Combat_Old>().SkillUse(target);
                break;
            case Define.SkillState.Smash:
                offensive = true;
                GetComponent<Smash_Old>().SkillUse(target);
                break;
        }
        AttackWait(1f);
    }
    IEnumerator AttackWait(float time)
    {
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
    }
}
