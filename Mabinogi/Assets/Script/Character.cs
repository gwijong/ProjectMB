using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : Movable
{
    
    /// <summary> 생명력 게이지 summary>
    protected Gauge hitPoint = new Gauge();
    /// <summary> 마나 게이지 summary>
    protected Gauge manaPoint = new Gauge();
    /// <summary> 스태미나 게이지 summary>
    protected Gauge staminaPoint = new Gauge();
    /// <summary> 다운 게이지 summary>
    protected Gauge downGauge = new Gauge();

    public CharacterData data;

    [SerializeField]
    /// <summary> 지정한 타겟</summary>
    protected Interactable focusTarget;
    protected Define.InteractType focusType;

    [SerializeField]
    /// <summary> 준비 완료된 현재 스킬</summary>
    protected Skill loadedSkill;
    /// <summary> 준비중인 현재 스킬</summary>
    protected Skill reservedSkill;
    /// <summary> 내가 배운 스킬 리스트</summary>
    protected SkillList skillList;
    /// <summary> 스킬 장전까지 남은 시간</summary>
    protected float skillCastingTimeLeft = 0.0f;
    /// <summary> 조작 가능 여부</summary>
    protected bool controllable = true;
    /// <summary> 일상, 전투모드 값</summary>
    protected bool offensive = false;
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

    /// <summary> 다운 지속 시간 </summary>
    public float downTime = 4.0f;
    /// <summary> 공격 지속 시간 </summary>
    public float attackTime = 1.0f;
    /// <summary> 동작 불가 체크 </summary>
    public int waitCount = 0;
    /// <summary> 동작 불가 코루틴 </summary>
    protected IEnumerator wait;
    /// <summary> 피격 애니메이션 A, B용 변수 </summary>
    protected int hitCount = 0;

    Rigidbody rigid;
    Animator anim;

    public SkillData combatData;
    public SkillData defenseData;
    public SkillData smashData;
    public SkillData counterData;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent.angularSpeed = 1000;  //내비게이션 회전값
        agent.acceleration = 100; //내비게이션 가속도
        agent.speed = data.Speed; //내비게이션 이동속도

        hitPoint.Max = data.HitPoint; //최대 생명력      
        hitPoint.FillableRate = 1.0f;  //부상률
        hitPoint.Current = data.HitPoint;  //현재 생명력
        

        manaPoint.Max = data.ManaPoint; //최대 마나
        manaPoint.FillableRate = 1.0f;  //마나 최대 비율
        manaPoint.Current = data.ManaPoint;  //현재 마나

        staminaPoint.Max = data.StaminaPoint; //최대 스태미나
        staminaPoint.FillableRate = 1.0f;  //허기
        staminaPoint.Current = data.StaminaPoint;  //현재 스태미나
        

        downGauge.Max = 100; //다운 게이지
        downGauge.FillableRate = 1.0f;
        downGauge.Current = 0;  //현재 누적된 다운게이지


        maxPhysicalStrikingPower = data.MaxPhysicalStrikingPower;
        maxMagicStrikingPower = data.MaxMagicStrikingPower;
        minPhysicalStrikingPower = data.MinPhysicalStrikingPower;
        minMagicStrikingPower = data.MinMagicStrikingPower;
        wound = data.Wound;
        woundAttack = data.WoundAttack;
        critical = data.Critical;
        balance = data.Balance;
        physicalDefensivePower = data.PhysicalDefensivePower;
        magicDefensivePower = data.MagicDefensivePower;
        physicalProtective = data.PhysicalProtective;
        magicProtective = data.MagicProtective;
        deadly = data.Deadly;
        speed = data.Speed;
    }
    
    protected virtual void Update()
    {
        if(skillCastingTimeLeft>0 && reservedSkill != null)
        {
            skillCastingTimeLeft -= Time.deltaTime;
            agent.speed = walkSpeed;
        }else if(skillCastingTimeLeft <= 0 && reservedSkill != null)
        {
            loadedSkill = reservedSkill;
            reservedSkill = null;
        }
             
        PlayAnim("Move", agent.velocity.magnitude);

        if (waitCount == 0)
        {
            MoveStop(false);
        }
        else
        {
            focusTarget = null;
            MoveStop(true);
        }

        //일단은 무조건 가는데 상황에 따라서 (스킬을 쓰면 스킬 사거리 까지만)
        //                                (무기 사거리일 수도 있고)
        //                                (대화 거리일 수도 있고)
        if (focusTarget != null)
        {
            float distance = (focusTarget.transform.position - transform.position).magnitude;

            if (distance > InteractableDistance(focusType))
            {
                MoveTo(focusTarget.transform.position);
            }
            else
            {
                MoveStop(true);
                
                switch(focusTarget.Interact(this))
                {
                    case Define.InteractType.Attack:
                        Attack((Hitable)focusTarget);
                        break;
                    case Define.InteractType.Talk:
                        //여기선 대화로 풀어나가기
                        break;
                };
                //일 다 봤으니까 풀어주기!
                focusTarget = null;
            }        
        };
    }

    public virtual float InteractableDistance(Define.InteractType wantType)
    {
        switch(wantType)
        {
            case Define.InteractType.Attack:
                {
                    //나중에 무기 사거리나 스킬 사거리 들어가면 여기에서 조정해줘!
                    return 2;
                }
            case Define.InteractType.Get: return 1;
            case Define.InteractType.Talk: return 3;
            default: return 2;
        };
    }

    /// <summary> 공격 함수</summary>
    public virtual void Attack(Hitable enemyTarget)
    {
        this.transform.LookAt(enemyTarget.transform);
        enemyTarget.transform.LookAt(this.transform);
        if (waitCount != 0)
        {
            return;
        }
        wait = Wait(attackTime);
        StartCoroutine(wait);

        if (enemyTarget.TakeDamage(this) == true)//공격에 성공한 경우
        {
            PlayAnim("Combat");
            Debug.Log("공격 성공");
        }
        else//공격에 실패한 경우
        {
            //공격이 실패한 경우에는 공격 대상자가 리턴값을 받아서 경직에 스스로 걸리게 해야해
            //디펜스 쓸때 공격자는 공격 모션은 유지하지만 락걸림
            //카운터는 반격 당하고 다운됨

            Debug.Log("공격 실패");
        };
    }

    public override Define.InteractType Interact(Interactable other)
    {       
        if(IsEnemy(this, other))
        {
            return Define.InteractType.Attack;
        }
        else
        {
            return Define.InteractType.Talk;
        };
    }

    public override void MoveTo(Vector3 goalPosition)
    {
        base.MoveTo(goalPosition);
    }

    /// <summary> 상대방이 이 캐릭터에 데미지를 주려고 상대방이 부르는 함수</summary>
    public override bool TakeDamage(Character Attacker)
    {
        reservedSkill = null;
        skillCastingTimeLeft = 0;

        SetOffensive(true);
        bool result = true;//기본적으로 공격은 성공하지만 경합일 경우 아래쪽에서 실패 체크

        //서로 마주보고 싸우는 경우 또는 디펜스.카운터 같은, 공격이 들어오면 무조건 스킬 사용 가능한지 체크해야 하는 경우
        if(Attacker.loadedSkill != null && this.focusTarget == Attacker || (this.loadedSkill != null && this.loadedSkill.mustCheck) )
        {
            result = Attacker.loadedSkill.WinnerCheck(this.loadedSkill); //상대방 스킬과 내 스킬의 우선순위 비교
        };

        if(result == true)
        {

            switch (Attacker.loadedSkill.type)
            {
                case Define.SkillState.Combat:
                    break;
                case Define.SkillState.Defense:
                    break;
                case Define.SkillState.Smash:
                    this.downGauge.Current += smashData.DownGauge;
                    this.hitPoint.Current -= Attacker.maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank;
                    Debug.Log("스매시 공격력: " + Attacker.maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank);
                    break;
                case Define.SkillState.Counter:
                    break;
            }
            
            if (this.hitPoint.Current <= 0)
            {
                DieCheck();
                DownCheck();
            }
            else if (this.downGauge.Current < 100)
            {
                PlayAnim("HitA");
            }
            else if (this.downGauge.Current >= 100)
            {
                DownCheck();
            }
        }
        return result;
    }

    /// <summary> 마우스 입력으로 타겟 설정 시도, 키보드 입력시 해제 </summary>
    public bool SetTarget(Character target)
    {
        if (target == null)
        {
            focusTarget = null;
            return false;
        };

        if (target.gameObject.layer == (int)Define.Layer.Enemy)
        {
            focusTarget = target;
            return true;
        };

        return false;

    }

    /// <summary> 스페이스바 입력으로 일상, 전투모드 전환 </summary>
    public void SetOffensive()
    {
        offensive = !offensive;
        PlayAnim("Offensive", offensive);
    }
    /// <summary> 매개변수 값을 줘서 일상, 전투모드 전환 </summary>
    public void SetOffensive(bool value)
    {
        offensive = value;
        PlayAnim("Offensive", offensive);
    }

    /// <summary> 다운 </summary>
    public void DownCheck()
    {
        rigid.AddForce(gameObject.transform.forward * -600);
        rigid.AddForce(gameObject.transform.up * 200);
        wait = Wait(downTime);
        StartCoroutine(wait);
        PlayAnim("BlowawayA");
        downGauge.Current = 0;
    }

    /// <summary> 사망 체크 </summary>
    public void DieCheck()
    {

        PlayAnim("Die");
    }

    /// <summary> 애니메이터 파라미터(trigger) 설정</summary>
    protected void PlayAnim(string wantName)  
    {
        if (anim != null) anim.SetTrigger(wantName);
    }
    /// <summary> 애니메이터 파라미터(bool) 설정</summary>
    protected void PlayAnim(string wantName, bool value)
    {
        if (anim != null) anim.SetBool(wantName, value);
    }
    /// <summary> 애니메이터 파라미터(float) 설정</summary>
    protected void PlayAnim(string wantName, float value)
    {
        if (anim != null) anim.SetFloat(wantName, value);
    }
    /// <summary> 애니메이터 파라미터(int) 설정</summary>
    protected void PlayAnim(string wantName, int value)
    {
        if (anim != null) anim.SetInteger(wantName, value);
    }

    public IEnumerator Wait(float time)//경직 시간 코루틴
    {
        offensive = true;
        waitCount++;
        yield return new WaitForSeconds(time);
        waitCount--;       
    }

    public void Casting(Define.SkillState value)
    {
        SkillInfo currentSkill = skillList[value];
        if (currentSkill == null) return;
        reservedSkill = currentSkill.skill;
        skillCastingTimeLeft = currentSkill.skill.castingTime;//업데이트문에 델타타임으로 조절//캔슬 시 skill을 null
    }
}
