using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : AI
{
    //public LayerMask whatIsTarget;
    /// <summary> 때릴 대상 레이어 마스크 </summary>
    int layerMask = 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player;    
    /// <summary> 적(캐릭터 컴포넌트) </summary>
    Character enemyCharacter; 
    /// <summary> 인공지능 시작 체크 </summary>
    bool aiStart = false; 
    /// <summary> 인공지능이 시전할 랜덤 스킬 번호 </summary>
    int skillNum; 
    /// <summary> AI 코루틴 할당할 변수 </summary>
    IEnumerator dummyAICoroutine;
    /// <summary> 추적 코루틴 할당할 변수 </summary>
    IEnumerator searchCoroutine;
    private void OnEnable()//오브젝트가 활성화되면
    {
        aiStart = false;//aiStart를 false로 맞춰 인공지능이 시작할 수 있게 한다.
        dummyAICoroutine = DummyAI(); //코루틴 변수에 AI 코루틴 할당
        searchCoroutine = UpdatePath();
    }
    protected override void Start()
    {
        base.Start();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾아옴
        //playerCharacter = player.GetComponent<Character>();//플레이어 캐릭터 가져오기
        StartCoroutine(searchCoroutine);//UpdatePath 코루틴은 한번 시작되면 1초 간격으로 무한 반복 실행됨 
    }
    void OnUpdate()
    {

        if(enemyCharacter == null) //적이 없으면 탈출
        {
            return;
        }

        if (enemyCharacter.die == true) //적이 죽으면
        {
            Reset();
            return;
        }
        //자신 캐릭터 사망하면 동작하지 않음
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
            dummyAICoroutine = DummyAI(); //인공지능 코루틴 할당
            StartCoroutine(dummyAICoroutine); //인공지능 코루틴 시작
        }
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
        yield return new WaitForSeconds(Random.Range(0.5f,3.0f)); //우선 2초 대기
        skillNum = Random.Range(0, 4);  //스킬 고름
        if (enemyCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //스킬 시전
        }
        yield return new WaitForSeconds(Random.Range(2f, 6.0f));   // 4초 대기

        if (skillNum == (int)Define.SkillState.Defense)//시전된 스킬이 디펜스이면
        {
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(enemyCharacter); //공격 시도
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(enemyCharacter); //공격 시도
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(enemyCharacter); //공격 시도
        }

        if (skillNum != (int)Define.SkillState.Counter)  // 카운터어택이 아니면 선공 공격이므로
        {

            if (enemyCharacter != null)
            {
                character.SetTarget(enemyCharacter);  //선공스킬이면 공격
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3.0f)); //2초 대기
        character.SetTarget(null); //타겟 해제
        aiStart = false; //코루틴 재실행 준비

    }



    /// <summary> 플레이어 사망시 AI 스킬 기본값으로 만드는 코루틴 </summary>
    void Reset()
    {
        enemyCharacter = null; //적 캐릭터를 비운다
        character.SetTarget(null); //타겟 비운다
        character.Casting(Define.SkillState.Combat); //컴벳 스킬로 전환
        character.SetOffensive(false); //일상모드로 전환
    }

    /// <summary> 5초마다 반복실행되는 플레이어를 찾는 코루틴 </summary>
    private IEnumerator UpdatePath()
    {
        while(!character.die)//이 캐릭터(Enemy)가 살아 있으면
        {
            //플레이어가 너무 멀리 도망가면
            if ((enemyCharacter != null && (enemyCharacter.transform.position - gameObject.transform.position).magnitude > 30)) 
            {
                Reset();
            }
            List<Character> enemyList = GetEnemyInRange(10f);//반지름 10의 구 안에 적 캐릭터만 리스트에 담아옴
            if (enemyList.Count > 0) //적이 있으면
            {
                enemyCharacter = enemyList[0]; //적 리스트의 0번째 적을 enemyCharacter에 할당함              
            }         
             yield return new WaitForSeconds(1f); //1초마다 반복 실행
        }
    }
}
