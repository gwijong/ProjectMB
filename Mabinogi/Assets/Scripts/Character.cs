using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))] //리지드바디가 기본 장착되어 있어야 함
[RequireComponent(typeof(BoxCollider))] //콜라이더가 기본 장착되어 있어야 함
/// <summary> 상호작용하는 모든 캐릭터에 달린 스크립트</summary>
public class Character : Movable
{

    #region 맴버 변수
    [Tooltip("캐릭터 이름")]
    /// <summary> 캐릭터 이름 summary>
    public string characterName;
    [Tooltip("이름 텍스트 UI의 Y높이")]
    /// <summary> 이름 텍스트 UI의 Y높이 summary>
    public float nameYpos;
    /// <summary> 생명력 게이지 summary>
    public Gauge hitPoint = new Gauge();
    /// <summary> 마나 게이지 summary>
    public Gauge manaPoint = new Gauge();
    /// <summary> 스태미나 게이지 summary>
    public Gauge staminaPoint = new Gauge();
    /// <summary> 다운 게이지 summary>
    protected Gauge downGauge = new Gauge();
    /// <summary> 캐릭터 데이터(플레이어, 개, 늑대 등) summary>
    [Tooltip("캐릭터 데이터(플레이어, 개, 늑대 등)")]
    public CharacterData data;
    [SerializeField]
    [Tooltip("지정한 타겟")]
    /// <summary> 지정한 타겟</summary>
    protected Interactable focusTarget;
    /// <summary> 지정한 타겟의 타입 enum</summary>
    protected Define.InteractType focusType;
    /// <summary> 이동 타입 enum</summary>
    protected Define.MoveType movetype;
    /// <summary> 준비 완료된 현재 스킬</summary>
    [SerializeField]
    [Tooltip("준비 완료된 현재 스킬")]
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
    protected bool offensive = true;

    //능력치들
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

    /// <summary> 다운 지속 시간 </summary>
    protected float downTime = 4.0f;
    /// <summary> 공격 지속 시간 </summary>
    protected float attackTime = 1.0f;
    /// <summary> 피격 지속 시간 </summary>
    protected float hitTime = 3.0f;
    /// <summary> 그로기 지속 시간 </summary>
    protected float groggyTime = 6.0f;
    /// <summary> 공격 실패 지속 시간 </summary>
    protected float attackFailTime = 3.0f;
    /// <summary> 피격시 동작 불가 체크 </summary>
    protected int waitCount = 0;
    /// <summary> 피격시 동작 불가 코루틴 </summary>
    protected IEnumerator wait;
    /// <summary> 피격 애니메이션 A, B용 변수 </summary>
    protected int hitCount = 0;
    /// <summary> 그로기 상태 체크 </summary>
    protected bool groggy = false;
    /// <summary> 사망 체크 </summary>
    [Tooltip("사망 체크")]
    public bool die = false;
    /// <summary> 리지드바디 </summary>
    protected Rigidbody rigid;
    /// <summary> 애니메이터 </summary>
    protected Animator anim;

    //스킬데이터 스크립터블 오브젝트들
    /// <summary> 기본공격 컴벳 스킬 데이터 </summary>
    protected SkillData combatData;
    /// <summary> 디펜스 스킬 데이터 </summary>
    protected SkillData defenseData;
    /// <summary> 스매시 스킬 데이터 </summary>
    protected SkillData smashData;
    /// <summary> 카운터 어택 스킬 데이터 </summary>
    protected SkillData counterData;

    [SerializeField]
    [Tooltip("내비게이션 회전값")]
    /// <summary> 내비게이션 회전값 </summary>
    protected float angularSpeed = 1000f;
    [SerializeField]
    [Tooltip("내비게이션 가속도")]
    /// <summary> 내비게이션 가속도 </summary>
    protected float acceleration = 100f;
    /// <summary> 소지한 골드 </summary>
    public int gold;
    /// <summary> 부활했는지 체크 </summary>
    public bool respawn = false;
    


    #endregion

    protected override void Awake()
    {
        base.Awake();
        #region 변수에 기본값 할당
        //스킬데이터 스크립터블 오브젝트 데이터 할당
        combatData = Define.SkillState.Combat.GetSkillData();
        defenseData = Define.SkillState.Defense.GetSkillData();
        smashData = Define.SkillState.Smash.GetSkillData();
        counterData = Define.SkillState.Counter.GetSkillData();

        rigid = GetComponent<Rigidbody>();//리지드바디 할당
        anim = GetComponent<Animator>();//애니메이터 할당
        agent.angularSpeed = angularSpeed;  //내비게이션 회전값
        agent.acceleration = acceleration; //내비게이션 가속도
        agent.speed = data.Speed; //내비게이션 이동속도
        runSpeed = data.Speed; //이동 속도
        walkSpeed = data.Speed / 2; //걷는 속도
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
        downGauge.FillableRate = 1.0f;//다운게이지 최대 비율
        downGauge.Current = 0;  //현재 누적된 다운게이지

        maxPhysicalStrikingPower = data.MaxPhysicalStrikingPower;  //최대 물리공격력
        maxMagicStrikingPower = data.MaxMagicStrikingPower;  //최대 마법공격력
        minPhysicalStrikingPower = data.MinPhysicalStrikingPower;  //최소 물리공격력
        minMagicStrikingPower = data.MinMagicStrikingPower;  //최소 마법공격력
        wound = data.Wound;  //부상
        woundAttack = data.WoundAttack;  //공격 시 부상률
        critical = data.Critical;  //치명타
        balance = data.Balance;  //밸런스
        physicalDefensivePower = data.PhysicalDefensivePower;  //물리 방어력
        magicDefensivePower = data.MagicDefensivePower;  //마법 방어력
        physicalProtective = data.PhysicalProtective;  //물리 보호
        magicProtective = data.MagicProtective;  //마법 보호
        deadly = data.Deadly;  //데들리 확률

        SetOffensive();//일상모드로 전환하고 이동속도를 걷기로 맞춰줌

        GameObject namePrefab = Instantiate(Resources.Load<GameObject>("Prefabs/UI/NameUICanvas")); //이름 표시 캔버스 생성
        namePrefab.transform.localScale = Vector3.one * 0.01f; //이름 표시 캔버스 크기 조절
        namePrefab.transform.SetParent(transform); //이름 표시 캔버스를 이 캐릭터의 자식 오브젝트로 설정
        namePrefab.transform.localPosition = new Vector3(0, nameYpos, 0); //이름 표시 캔버스의 좌표를 이 캐릭터 좌표에서 nameYpos만큼 위로 올려줌
        namePrefab.GetComponentInChildren<UnityEngine.UI.Text>().text = characterName; //이름 표시 캔버스의 텍스트에 characterName대입

        #endregion
    }

    protected virtual void OnUpdate()
    {
        GaugeCheck(); // 각종 게이지 관리

        PlayAnim("Move", agent.velocity.magnitude);  //이동을 내비에서 float 값으로 받아와서 애니메이션 재생

        TargetLookAt(focusTarget); //타겟 바라보기

        SkillReady(); //skillCastingTimeLeft 남은시간 체크

        MovableCheck(); //waitCount를 이용한 이동 가능 체크

        TargetCheck(); //지정한 타겟 체크

        DestinationCheck();

    }

    /// <summary> 게이지들 업데이트 </summary>
    void GaugeCheck()
    {
        downGauge.Current -= 5 * Time.deltaTime; //다운게이지를 초당 5씩 빼줌
        if (loadedSkill.type == Define.SkillState.Combat && reservedSkill == null) //스킬이 컴벳이고 시전중인 스킬이 없는 경우
        {
            staminaPoint.Current += 0.4f * Time.deltaTime; //스태미나 초당 0.4 증가
            manaPoint.Current += 0.1f * Time.deltaTime; // 마나 초당 0.1 증가
        }
        if (loadedSkill.type == Define.SkillState.Counter) //시전된 스킬이 카운터이면
        {
            staminaPoint.Current -= 1 * Time.deltaTime; //초당 1씩 스태미나 감소
        }
    }

    void DestinationCheck()
    {
        if(movetype == Define.MoveType.DropItem)
        {
            Vector3 positionDiff = (agent.destination - transform.position);//상대 좌표에서 내 좌표 뺌
            positionDiff.y = 0;//높이 y는 배제함
            float distance = positionDiff.magnitude;  //타겟과 나의 거리
            if (distance < 1) //거리가 상호작용 가능 거리보다 먼 경우
            {
                Inventory.DropItem();
                movetype = Define.MoveType.Move;
                MoveStop(true);
            }
        }
    }

    /// <summary> 지정한 타겟이 사물인지, 캐릭터인지 체크하는 구간 </summary>
    void TargetCheck()
    {
        if (focusTarget != null) //마우스로 클릭한 타겟이 있는 경우
        {
            Vector3 positionDiff = (focusTarget.transform.position - transform.position);//상대 좌표에서 내 좌표 뺌
            positionDiff.y = 0;//높이 y는 배제함
            float distance = positionDiff.magnitude;  //타겟과 나의 거리
            if (distance > InteractableDistance(focusTarget.Interact(this))) //거리가 상호작용 가능 거리보다 먼 경우
            {
                MoveTo(focusTarget.transform.position);  //다가가도록 이동
                if (focusTarget.Interact(this) == Define.InteractType.Attack)//대상이 적이면 전투모드로 이동
                {
                    SetOffensive(true); //전투모드로 전환
                }
            }
            else //거리가 상호작용 가능 거리보다 가까운 경우
            {
                MoveStop(true); //다가가는 이동 멈춤


                Character focusAsCharacter = null; //나무 같은 사물은 못 들어감, 늑대 , 닭 같은 캐릭터만 들어감

                //Type.IsSubclassOf : Type에서 파생되는지 여부를 확인
                if (focusTarget.GetType().IsSubclassOf(typeof(Character))) //파생 클래스 캐릭터가 있는지 체크, 있으면 true
                {
                    focusAsCharacter = (Character)focusTarget;//지정된 대상
                };
                switch (focusTarget.Interact(this))//대상과 자신의 상호작용 타입
                {
                    case Define.InteractType.Attack: //상호작용 타입이 공격이면
                        if (skillCastingTimeLeft > 0 //스킬 시전 시간이 남았거나
                            || loadedSkill.cannotAttack //선공 불가 스킬을 시전했거나
                            || (focusAsCharacter != null && focusAsCharacter.die == true)) //지정된 대상이 있으면서 지정된 대상이 사망했으면
                        {
                            break;//케이스문 탈출
                        };
                        Attack((Hitable)focusTarget); //공격한다
                        break;
                    case Define.InteractType.Talk:  //상호작용 타입이 대화이면
                        if (GetType().IsClassOf(typeof(Player)) && focusTarget.GetType().IsClassOf(typeof(NPC)))
                        {
                            ((Player)this).Talk((NPC)focusTarget);//플레이어의 대화 메서드 실행
                        }
                        break;
                    case Define.InteractType.Sheeping:  //상호작용 타입이 양털 채집이면
                        if (GetComponent<Player>() != null)
                        {
                            GetComponent<Player>().Sheeping();//플레이어의 양털채집 메서드 실행
                        }
                        break;
                    case Define.InteractType.Egg:  //상호작용 타입이 달걀 채집이면
                        if (GetComponent<Player>() != null)
                        {
                            GetComponent<Player>().Egg();//플레이어의 달걀채집 메서드 실행
                        }
                        break;
                    case Define.InteractType.Get:  //상호작용 타입이 아이템 줍기이면
                        ItemInpo itemInpo = focusTarget.GetComponent<ItemInpo>(); //타겟의 아이템 정보 스크립트 컴포넌트 가져오기 시도
                        if (itemInpo != null)//타겟이 아이템 정보 스크립트 컴포넌트를 가지고 있으면
                        {
                            itemInpo.GetItem(); //아이템 정보 스크립트의 아이템 줍기 메서드 실행
                        }
                        break;
                    default:
                        Debug.Log("타겟과의 상호작용에서 오류가 발생함");//이상한 값 들어오면 디버그

                        break;
                };
                focusTarget = null; //상호작용이 끝났으므로 타겟을 비움
            }
        };
    }
    /// <summary> 움직일 수 있는지 체크 </summary>
    bool MovableCheck()
    {
        bool CanMove = true; //움직일 수 있는가?
        if (waitCount == 0) // waitCount 누적치가 정확히 0인 경우
        {
            CanMove = true; //이동 가능
        }
        else
        {
            CanMove = false; //waitCount가 남아서 움직일 수 없음
        }

        if (CanMove == false)  //동작 불가일 경우
        {
            focusTarget = null;  //타겟을 null로 바꿈(맞는 중에 공격 할 수 없기 때문)
        };

        MoveStop(!CanMove);//내비게이션 이동 메서드에 이동가능한지 bool값 넣어줌
        return CanMove;
    }

    /// <summary> 스킬 준비, 완료 </summary>
    void SkillReady()
    {
        if (skillCastingTimeLeft >= 0 && reservedSkill != null) //스킬 시전 시간이 남았고 시전중인 스킬이 있을 경우
        {
            skillCastingTimeLeft -= Time.deltaTime;  //남은 스킬 시전 시간을 지속적으로 줄여줌
            setMoveSpeed(reservedSkill.type);//스킬 타입에 따라 이동속도 설정
        }
        else if (skillCastingTimeLeft <= 0 && reservedSkill != null)  //스킬 시전 시간이 다 지났을 경우
        {
            loadedSkill = reservedSkill;  //준비된 스킬 장전
            reservedSkill = null;  // 준비중인 스킬 null로 전환
            if (loadedSkill.type != Define.SkillState.Combat)
            {
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_ready);//스킬 준비 완료 효과음
            }
        }
    }

    /// <summary> 시전한 스킬에 따라 이동속도 변경 </summary>
    void setMoveSpeed(Define.SkillState type)
    {
        float result = runSpeed;
        switch (type)  //준비중인 현재 스킬 타입
        {
            case Define.SkillState.Defense:
                result = walkSpeed;//디펜스는 걷는 속도
                break;
            case Define.SkillState.Counter:
                result = 0;  //카운터면 이동 멈춤
                break;
        };
        if (walk && result > walkSpeed) result = walkSpeed;
        agent.speed = result;
    }

    /// <summary> 지정한 타겟 바라봄 </summary>
    public void TargetLookAt(Interactable target)
    {
        if (target != null && agent.velocity.magnitude <= 1.0f && waitCount==0) //타겟이 존재하고 속도가 1 이하로 거의 멈춰있으면서 대기중이지 않으면
        {
            Vector3 look = target.transform.position; //look에 타겟의 좌표 대입
            look.y = transform.position.y; //y좌표는 무시함
            transform.LookAt(look); //타겟을 바라봄
        };
    }

    /// <summary> 지정한 좌표 바라봄 </summary>
    void TargetLookAt(Vector3 target)
    {
        if (target != null && agent.velocity.magnitude <= 1.0f)//타겟이 존재하고 속도가 1 이하로 거의 멈춰있으면
        {
            target.y = transform.position.y; //y좌표는 무시함
            transform.LookAt(target);//타겟을 바라봄
        };
    }

    /// <summary> 상호작용 가능 거리 </summary>
    public virtual float InteractableDistance(Define.InteractType wantType)
    {
        switch (wantType)
        {
            case Define.InteractType.Attack://공격 거리
                {
                    return 4; //무기 사거리가 늘어날 경우 여기서 조정한다.
                }
            case Define.InteractType.Get: return 1; //아이템 줍기 가능 거리
            case Define.InteractType.Sheeping: return 2; //양털 채집 가능 거리
            case Define.InteractType.Talk: return 6;  //대화 가능 거리
            
            default: return 2;  //기본값은 2이다.
        };
    }

    /// <summary> 공격 함수</summary>
    public virtual void Attack(Hitable enemyTarget)//타겟을 공격한다
    {
        SetOffensive(true);//전투모드로 전환
        this.transform.LookAt(enemyTarget.transform); //타겟을 바라본다.

        //동작 불가 상태이거나 컴벳인데 스태미나가 2 미만이면 공격을 못 하므로
        if (waitCount != 0 || (loadedSkill.type == Define.SkillState.Combat && staminaPoint.Current < combatData.CastCost))
        {
            return;  //바로 이 메서드를 나간다
        }
        wait = Wait(attackTime); //공격 딜레이 코루틴
        StartCoroutine(wait);

        //TakeDamage함수로 들어가면 스킬이 풀리기 때문에 스킬을 임시 저장
        Character asCharacter = null;
        if (enemyTarget.GetType().IsSubclassOf(typeof(Character))) //enemyTarget의 클래스나 상속받은 클래스가 Character 클래스의 자식클래스이거나 Character 클래스이면
        {
            asCharacter = (Character)enemyTarget;  //상대방 캐릭터를 취급함
        };

        Define.SkillState otherSkill = Define.SkillState.Combat;  //상대방 스킬은 기본값 기본공격
        if (asCharacter != null) otherSkill = asCharacter.GetSkillType();//상대방 캐릭터가 있는 경우 상대방 스킬 가져옴


        if (enemyTarget.TakeDamage(this) == true)//공격에 성공한 경우
        {
            PlayAnim(loadedSkill.AnimName); //해당 스킬에 맞는 애니메이터 트리거 설정
            float waitTime = 0.0f;//공격 대기 시간
            switch (loadedSkill.type)
            {
                case Define.SkillState.Combat: //근접공격이면 1초 대기
                    waitTime = 1.0f; //공격 대기 시간
                    staminaPoint.Current -= combatData.CastCost; //근접 공격에 성공한 경우 스태미나를 2 차감                   
                    break;
                case Define.SkillState.Smash:
                    waitTime = 4.0f; //스매시면 4초 대기
                    break;
            };

            wait = Wait(waitTime); //공격 스킬 딜레이 만큼 대기
            StartCoroutine(wait);

            loadedSkill = skillList[Define.SkillState.Combat].skill;//공격 후 기본 공격으로 준비된 스킬 초기화
        }
        else//공격에 실패한 경우
        {
            if (asCharacter != null) //타겟이 존재하는경우
            {
                switch (otherSkill)
                {
                    //공격이 실패한 경우에는 공격 대상자가 리턴값을 받아서 경직에 스스로 걸리게 해야함
                    //디펜스 쓸때 공격자는 공격 모션은 유지하지만 락걸림
                    case Define.SkillState.Defense:
                        PlayAnim("Combat");//공격에 실패했지만 공격 애니메이션은 재생한다
                        wait = Wait(attackFailTime); //공격 실패로 3초간 경직
                        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.guard);//닫기 효과음
                        StartCoroutine(wait);
                        break;

                    //카운터는 반격 당하고 다운됨
                    case Define.SkillState.Counter:
                        this.downGauge.Current += counterData.DownGauge;//다운게이지를 100 추가
                        float damage = asCharacter.CalculateDamage(Define.SkillState.Counter); //데미지 계산
                        this.hitPoint.Current -= damage; //현재 생명력에서 데미지 만큼 빼줌
                        Groggy();//반격 당했으므로 그로기 상태에 빠짐
                        break;
                }
            };
            loadedSkill = skillList[Define.SkillState.Combat].skill;// 내 스킬 초기화

        };
    }
    //other가 상호작용을 하려고 오는 놈, 반환값을 받으려고 온 대상
    //this가 나 자신 캐릭터 스크립트 컴포넌트
    //나 자신 캐릭터 스크립트 컴포넌트가 Interact메서드를 실행하면 나의 타입을 리턴한다.
    //focusTarget.Interact(this)는 매개변수 other에 나 자신 this가 들어가면 focusTarget의 타입을 리턴한다.
    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //상대방과 내가 적인지 체크
        {
            return Define.InteractType.Attack; //상호작용 타입을 공격으로 리턴
        }
        else
        {
            return Define.InteractType.Talk; //상호작용 타입을 대화로 리턴
        }
    }

    public override void MoveTo(Vector3 goalPosition, bool isWalk = false)  //내비게이션 이동 메서드
    {
        base.MoveTo(goalPosition, isWalk);
    }

    public void MoveTo(Vector3 goalPosition, Define.MoveType currentMoveType ,bool isWalk = false)  //내비게이션 이동 메서드
    {
        base.MoveTo(goalPosition, isWalk);
        movetype = currentMoveType;
    }

    /// <summary> 상대방이 이 캐릭터에 데미지를 주려고 상대방이 부르는 함수</summary>
    public override bool TakeDamage(Character Attacker)
    {
        transform.LookAt(Attacker.transform); //때린 상대를 바라본다
        reservedSkill = null; //준비중인 스킬 취소
        skillCastingTimeLeft = 0; //준비중인 스킬이 취소되었으므로 취소 시간도 0으로 초기화

        SetOffensive(true); //전투모드로 전환
        bool result = true;//기본적으로 공격은 성공하지만 경합일 경우 아래쪽에서 실패 체크
        agent.speed = runSpeed; //이동속도 초기화
        walk = false;
        //서로 마주보고 싸우는 경우 또는 디펜스.카운터 같은, 공격이 들어오면 무조건 스킬 사용 가능한지 체크해야 하는 경우
        if (Attacker.loadedSkill != null && this.focusTarget == Attacker || (this.loadedSkill != null && this.loadedSkill.mustCheck))
        {
            result = Attacker.loadedSkill.WinnerCheck(this.loadedSkill); //상대방 스킬과 내 스킬의 우선순위 비교
        };


        if (result == true) //상대방의 공격이 성공한 경우
        {
            float damage = 0.0f;
            switch (Attacker.loadedSkill.type)//상대방 스킬에 따라 내가 피해를 입음
            {
                case Define.SkillState.Combat: //근접공격일 경우
                    this.downGauge.Current += combatData.DownGauge; //다운게이지를 40 채워줌
                    damage = Attacker.CalculateDamage(Define.SkillState.Combat); //데미지 계산
                    break;
                case Define.SkillState.Smash: //스매시일 경우
                    damage = Attacker.CalculateDamage(Define.SkillState.Smash); //데미지 계산
                    groggy = true; //그로기 상태로 전환
                    this.downGauge.Current += smashData.DownGauge; //다운게이지를 100 채워줌
                    break;
            }
            this.hitPoint.Current -= damage;//현재 생명력에서 데미지만큼 빼줌

            if (this.hitPoint.Current <= 0.2)//생명력이 0.2이하일 경우 사망
            {                
                Dead();
            }
            else if (this.downGauge.Current < 100) //다운게이지가 100 이하일 경우
            {
                //그로기 상태거나 날아가는 상태가 아니면
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Groggy" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Blowaway_Ground")
                {
                    PlayAnim("HitA");  //피격 애니메이션 재생
                    GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.punch_hit);//펀치 효과음
                }
                wait = Wait(hitTime); // 피격 지속시간만큼 경직
                StartCoroutine(wait);

            }
            else if (this.downGauge.Current >= 100)//다운게이지가 100 이상일 경우
            {
                if (groggy) //그로기 상태에 들어가야 하면
                {
                    Groggy();//그로기 상태로 들어감
                    groggy = false; //그로기 변수 꺼줌
                }
                else
                {
                    DownCheck();//그로기가 아닐 경우 일반 다운 상태로 들어감
                }
            }
            loadedSkill = skillList[Define.SkillState.Combat].skill; //피격 시 준비된 스킬 초기화
        }
        else //공격에 실패한 경우
        {
            switch (loadedSkill.type)
            {
                case Define.SkillState.Defense:
                    PlayAnim("Defense"); //적 디펜스 애니메이션 재생
                    StartCoroutine(Wait(1f));//적 디펜스 시전 중 이동 막기
                    //공격을 막았는데 디펜스로 막음
                    break;
                case Define.SkillState.Counter:
                    PlayAnim("Counter");//적 카운터 애니메이션 재생
                    StartCoroutine(Wait(2f));//적 카운터 시전 중 이동 막기
                    //공격을 막았는데 카운터로 막음
                    break;
                default:
                    break;
            };
            loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 초기화
        }

        return result;
    }



    /// <summary> 대미지 계산하는 구간 </summary>
    public float CalculateDamage(Define.SkillState type)
    {
        switch (type)
        {
            case Define.SkillState.Smash://스매시 데미지 계산
                return maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank;
            case Define.SkillState.Combat://근접 공격 데미지 계산
                return maxPhysicalStrikingPower * combatData.Coefficient * skillList[Define.SkillState.Combat].rank;
            case Define.SkillState.Counter://카운터 데미지 계산
                return maxPhysicalStrikingPower * counterData.Coefficient * skillList[Define.SkillState.Counter].rank;
        };
        return 0; //이상한거 들어오면 0 리턴
    }

    /// <summary> 준비 완료된 스킬 리턴 </summary>
    public Define.SkillState GetSkillType()
    {
        if (loadedSkill == null) return Define.SkillState.Combat; //준비 완료된 스킬이 null이면 기본공격을 넣어줌

        return loadedSkill.type;//준비 완료된 스킬 반환
    }

    /// <summary> 준비중인 스킬 정보 리턴 </summary>
    public Skill GetreservedSkill()
    {
        return reservedSkill;
    }

    /// <summary> 준비 완료된 스킬 정보 리턴 </summary>
    public Skill GetloadedSkill()
    {
        return loadedSkill;
    }

    /// <summary> 현재 생명력 리턴 </summary>
    public float GetCurrentHP()
    {
        return hitPoint.Current;
    }

    /// <summary> 마우스 입력으로 타겟 설정 시도, 키보드 입력시 타겟 해제 </summary>
    public bool SetTarget(Interactable target)
    {
        if (target == null) //타겟이 널이면
        {
            focusTarget = null;  //바라보는 타겟을 null 대입
            return false;
        };
        focusTarget = target;
        return true;

    }

    /// <summary> 스페이스바 입력으로 일상, 전투모드 전환 </summary>
    public void SetOffensive()
    {
        offensive = !offensive; //현재 상태를 뒤집음
        OffensiveSetting();
    }
    /// <summary> 매개변수 값을 줘서 일상, 전투모드 전환 </summary>
    public void SetOffensive(bool value)
    {
        offensive = value;
        OffensiveSetting();
    }

    /// <summary> 일상, 전투모드에 맞춰 애니메이션과 이동속도 전환 </summary>
    void OffensiveSetting()
    {
        PlayAnim("Offensive", offensive); //offensive bool값에 맞춰 애니메이터의 Offensive bool값 전환

        if (!offensive && agent.speed >= runSpeed) //일상모드이고 현재 이동속도가 달리는 속도 이상이면
        {
            walk = true; //걷기로 전환
            agent.speed = walkSpeed; //걷기 속도로 전환
        }
        else if (offensive) //전투모드이면
        {
            walk = false; //뛰기로 전환
            agent.speed = runSpeed; //달리는 속도로 전환
            if (reservedSkill != null) //준비중인 스킬이 있으면
                setMoveSpeed(reservedSkill.type); //준비중인 스킬 이동속도로 세팅
            if (loadedSkill != null) //준비완료된 스킬이 있으면
                setMoveSpeed(loadedSkill.type); //준비완료된 스킬 이동속도로 세팅
        }
    }

    /// <summary> 다운 </summary>
    public void DownCheck()
    {
        rigid.velocity = new Vector3(0, 0, 0);  //리지드바디의 속도를 0으로 초기화 
        Blowaway();//캐릭터가 뒤로 날아감
        wait = Wait(downTime); //조작 불가 코루틴
        StartCoroutine(wait);  //조작 불가 시작
        PlayAnim("BlowawayA"); //날아가는 애니메이션 시작

        downGauge.Current = 0; //다운게이지 초기화
    }

    /// <summary> 사망 시 실행되는 메서드 </summary>
    public void Dead()
    {
        waitCount = 1;
        Blowaway();
        die = true;  //사망 상태로 전환
        PlayAnim("Die");  //사망 트리거 체크
        StartCoroutine("Die");//사망 코루틴 실행
    }

    /// <summary> 애니메이터 파라미터(trigger) 설정</summary>
    protected void PlayAnim(string wantName)
    {
        if (wantName == "Combat") //애니메이션 스킬이 컴벳일 경우 
        {
            anim.SetInteger("CombatNumber", Random.Range(0, 3));//3가지 랜덤한 애니메이션 실행
        }
        if (anim != null) anim.SetTrigger(wantName);//애니메이터의 wantName Trigger 체크
    }
    /// <summary> 애니메이터 파라미터(bool) 설정</summary>
    protected void PlayAnim(string wantName, bool value)
    {
        if (anim != null) anim.SetBool(wantName, value); //애니메이터의 wantName bool 체크
    }
    /// <summary> 애니메이터 파라미터(float) 설정</summary>
    protected void PlayAnim(string wantName, float value)
    {
        if (anim != null) anim.SetFloat(wantName, value); //애니메이터의 wantName float 체크
    }
    /// <summary> 애니메이터 파라미터(int) 설정</summary>
    protected void PlayAnim(string wantName, int value)
    {
        if (anim != null) anim.SetInteger(wantName, value); //애니메이터의 wantName 정수 체크
    }

    /// <summary> 스킬 시전중</summary>
    public void Casting(Define.SkillState value)
    {
        loadedSkill = skillList[Define.SkillState.Combat].skill; //준비 완료된 스킬을 취소하고 기본값인 기본공격으로 전환
        SkillInfo currentSkill = skillList[value]; //현재 스킬은 skillList[value]이다.
        if (currentSkill == null) return; //입력된 현재 스킬이 null일 경우 리턴
        switch (currentSkill.skill.type)//상대방 스킬에 따라 내가 피해를 입음
        {
            case Define.SkillState.Defense:
                if (staminaPoint.Current < defenseData.CastCost)  //현재 스킬 시전에 필요한 스태미나보다 적은 경우
                {
                    return; // 리턴하고 스킬 시전 안함
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//스킬 준비중 효과음
                staminaPoint.Current -= defenseData.CastCost; // 시전비용만큼 스태미나 차감
                break;
            case Define.SkillState.Smash:
                if (staminaPoint.Current < smashData.CastCost) //현재 스킬 시전에 필요한 스태미나보다 적은 경우
                {
                    return; // 리턴하고 스킬 시전 안함
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//스킬 준비중 효과음
                staminaPoint.Current -= smashData.CastCost; // 시전비용만큼 스태미나 차감
                break;
            case Define.SkillState.Counter:
                if (staminaPoint.Current < counterData.CastCost) //현재 스킬 시전에 필요한 스태미나보다 적은 경우
                {
                    return; // 리턴하고 스킬 시전 안함
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//스킬 준비중 효과음
                staminaPoint.Current -= counterData.CastCost; // 시전비용만큼 스태미나 차감
                break;
        }
        reservedSkill = currentSkill.skill; //시전중인 스킬에 대입
        skillCastingTimeLeft = currentSkill.skill.castingTime;//업데이트문에 델타타임으로 조절//캔슬 시 skill을 null
    }
    /// <summary> 경직 시간 코루틴</summary>
    public IEnumerator Wait(float time)
    {
        offensive = true; //전투 모드로 전환
        waitCount++;//동작 불가 카운트 증가
        yield return new WaitForSeconds(time);
        waitCount--; //동작 불가 카운트 감소
    }
    /// <summary> 내가 그로기 상태에 들어감</summary>
    public void Groggy()
    {

        wait = Wait(groggyTime);
        StartCoroutine(wait);  //조작불가 코루틴 시작
        IEnumerator groggy = GroggyDown();
        StartCoroutine(groggy); //그로기 다운 코루틴 시작
        PlayAnim("Groggy"); //그로기 애니메이션 시작
        downGauge.Current = 0; //다운게이지 초기화
    }
    /// <summary> 그로기 이후에 다운 상태로 들어감</summary>
    IEnumerator GroggyDown()
    {
        yield return new WaitForSeconds(1.0f); //1초간 그로기 애니메이션이 재생되도록 대기
        if (this.hitPoint.Current <= 0.2)//생명력이 0.2이하일 경우 사망
        {
            Dead();
        }
        else
        {
            Blowaway();
        }
    }

    ///<summary> 캐릭터가 뒤로 밀려나고 날아감</summary>
    void Blowaway()
    {
        rigid.velocity = new Vector3(0, 0, 0);  //리지드바디의 속도를 0으로 초기화 
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.punch_blow);//날아가는 효과음
        rigid.AddForce(gameObject.transform.forward * -600);//뒤로 밀림
        agent.velocity = (Vector3.up * 500); //위로 날림
    }

    /// <summary> 사망 코루틴</summary>
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f); //1초 대기
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.down);//사망 효과음
        yield return new WaitForSeconds(0.5f); //0.5초 대기
        gameObject.GetComponent<BoxCollider>().enabled = false; //콜라이더 끔
        yield return new WaitForSeconds(5f);
        //Respawn();
    }

    /// <summary> 캐릭터 부활</summary>
    public void Respawn()
    {
        waitCount = 0;
        die = false;
        hitPoint.Current = hitPoint.Max;
        PlayAnim("Reset");//애니메이션을 idle로 전환
        Vector3 spawnPosition = GetRandomPointOnNavMesh(transform.position, 7); //사망 위치 근처에서 내비메시 위의 랜덤 위치 가져오기
        spawnPosition += Vector3.up * 2;  //바닥에서 2만큼 y좌표 위로 올리기
        transform.position = spawnPosition;
        gameObject.GetComponent<BoxCollider>().enabled = true; //콜라이더 켬     
        MoveTo(spawnPosition);//내비메시 이동지점 초기화
        respawn = true; //인공지능 시작
    }

    //내비메시 위의 랜덤한 위치를 반환하는 메서드
    /// <summary> center를 중심으로 distance 반경 안에서의 랜덤한 위치를 찾음 </summary>
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = (Random.insideUnitSphere * distance) + center;

        //내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;
        //maxDistance 반경 안에서 randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);//out = 출력전용 매개변수
        //찾은 점 반환
        return hit.position;
    }
}
