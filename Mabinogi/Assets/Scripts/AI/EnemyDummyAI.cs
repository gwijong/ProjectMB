using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 공통 인공지능 </summary>
public class EnemyDummyAI : AI
{
    /// <summary> 적(캐릭터 컴포넌트) </summary>
    public Character enemyCharacter; 
    /// <summary> 인공지능 시작 체크 </summary>
    bool aiStart = false; 
    /// <summary> 인공지능이 시전할 랜덤 스킬 번호 </summary>
    int skillNum; 
    /// <summary> AI 코루틴 할당할 변수 </summary>
    IEnumerator dummyAICoroutine;
    /// <summary> 추적 코루틴 할당할 변수 </summary>
    IEnumerator searchCoroutine;

    float progress = 0;

    protected override void Start()
    {
        base.Start();
        Setting();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾아옴
        //playerCharacter = player.GetComponent<Character>();//플레이어 캐릭터 가져오기
        StartCoroutine(searchCoroutine);//UpdatePath 코루틴은 한번 시작되면 1초 간격으로 무한 반복 실행됨 
    }

    public void OnUpdate()
    {
        //플레이어가 너무 멀리 도망가면
        if ((enemyCharacter != null && (enemyCharacter.transform.position - gameObject.transform.position).magnitude > 20))
        {
            Reset();
        }

        if (character.isRespawnAIStart == true) //캐릭터가 리스폰했으면
        {
            character.isRespawnAIStart = false;
            Reset();
            aiStart = false; //인공지능 시작
            StartCoroutine(searchCoroutine); //추적 시작

        }

        if (character.die == true)//내가 죽으면
        {
            aiStart = true;  //인공지능 코루틴이 실행되지 않도록 인공지능 코루틴이 시작했다고 설정
            stopCoroutine(); //인공지능 코루틴 중지
            Reset();
            return;
        }

        if (aiStart == false) //인공지능 코루틴이 시작되지 않았으면
        {
            aiStart = true; //인공지능 시작 bool값 true로 변경
            dummyAICoroutine = null;
            dummyAICoroutine = DummyAI(); //인공지능 코루틴 할당
            StartCoroutine(dummyAICoroutine); //인공지능 코루틴 시작
        }


        if (enemyCharacter != null && enemyCharacter.die == true) //적이 죽으면
        {
            Reset();
        }

    }
    /// <summary> 시작 세팅 </summary>
    public void Setting()
    {
        aiStart = false;//aiStart를 false로 맞춰 인공지능이 시작할 수 있게 한다.
        dummyAICoroutine = DummyAI(); //코루틴 변수에 AI 코루틴 할당
        searchCoroutine = UpdatePath();
    }

    /// <summary> 코루틴 중지 </summary>
    public void stopCoroutine()
    {
        if (dummyAICoroutine != null)
            StopCoroutine(dummyAICoroutine);
        if (searchCoroutine != null)
            StopCoroutine(searchCoroutine);
    }

    /// <summary> 더미 인공지능 </summary>
    IEnumerator DummyAI()
    {
        character.Casting(Define.SkillState.Combat);//일단 스킬을 기본공격으로 설정
        skillNum = Random.Range(0, 4);  //스킬 고름
        if (enemyCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //스킬 시전

            yield return new WaitForSeconds(Random.Range(1f, 3.0f));   // 1초부터 3초 대기

            switch (skillNum)
            {
                case (int)Define.SkillState.Combat:
                    character.SetTarget(enemyCharacter);  //선공스킬이면 공격
                    break;
                case (int)Define.SkillState.Defense:
                    {
                        int patrolTime = Random.Range(200, 500);
                        float randomDistance = Random.Range(8.0f, 12.0f);
                        for(int i= 0; i< patrolTime && character.GetloadedSkill().type == Define.SkillState.Defense && enemyCharacter != null; i++)
                        {
                            Vector3 currentPos = new Vector3(Mathf.Sin(progress), 0, Mathf.Cos(progress));
                            currentPos *= randomDistance;
                            character.MoveTo(enemyCharacter.transform.position + currentPos);
                            progress += 0.015f;
                            yield return new WaitForSeconds(0.02f);
                        }
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        break;
                    }                    
                case (int)Define.SkillState.Smash:
                    character.SetTarget(enemyCharacter);  //선공스킬이면 공격
                    break;
                case (int)Define.SkillState.Counter:
                    break;
            }
        }       
        yield return new WaitForSeconds(Random.Range(1f, 3.0f)); //1초부터 3초 대기
        character.SetTarget(null); //타겟 해제
        aiStart = false; //코루틴 재실행 준비
    }



    /// <summary> 플레이어 사망시 AI 스킬 기본값으로 만드는 코루틴 </summary>
    public void Reset()
    {
        enemyCharacter = null; //적 캐릭터를 비운다
        character.SetTarget(null); //타겟 비운다
        character.Casting(Define.SkillState.Combat); //컴벳 스킬로 전환
        character.SetOffensive(false); //일상모드로 전환
    }

    /// <summary> 순찰 </summary>
    void Patrol(float range)
    {
        Vector3 movePos = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)); //랜덤하게 이동할 범위
        character.MoveTo(character.spawnPos + movePos); //부활 기준점 주위 이동    
    }

    /// <summary> 5초마다 반복실행되는 플레이어를 찾는 코루틴 </summary>
    private IEnumerator UpdatePath()
    {      
        while (!character.die )//이 캐릭터(Enemy)가 살아 있으면
        {
            if(enemyCharacter == null)
            {
                List<Character> enemyList = GetEnemyInRange(30f);//반지름 30의 구 안에 적 캐릭터만 리스트에 담아옴
                if (enemyList.Count > 0 ) //적이 있으면
                {                     
                    enemyCharacter = enemyList[Random.Range(0,enemyList.Count)]; //적 리스트의 i번째 적을 enemyCharacter에 할당함
                    character.TargetLookAt(enemyCharacter); // 보게 만듦
                }
                else
                {
                    Reset();
                    Patrol(5.0f);
                }
            };
            yield return new WaitForSeconds(Random.Range(1.0f,4.0f)); //1에서 4초 사이 반복 실행            
        }
    }
}
