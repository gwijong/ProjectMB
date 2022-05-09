using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : MonoBehaviour
{
    //public LayerMask whatIsTarget;
    /// <summary> 때릴 대상 레이어 마스크 </summary>
    int layerMask = 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player;
    /// <summary> 인공지능 몬스터 나 자신 </summary>
    Character character;
    /// <summary> 적(플레이어) </summary>
    GameObject player; 
    /// <summary> 적(플레이어 캐릭터 컴포넌트) </summary>
    Character playerCharacter; 
    /// <summary> 인공지능 시작 체크 </summary>
    bool aiStart = false; 
    /// <summary> 인공지능이 시전할 랜덤 스킬 번호 </summary>
    int skillNum; 
    /// <summary> AI 코루틴 할당할 변수 </summary>
    IEnumerator coroutine;
    private void OnEnable()//오브젝트가 활성화되면
    {
        aiStart = false;//aiStart를 false로 맞춰 인공지능이 시작할 수 있게 한다.
        coroutine = DummyAI(); //코루틴 변수에 AI 코루틴 할당
    }
    private void Start()
    {
        character = gameObject.GetComponent<Character>();//이 몬스터 캐릭터 가져오기
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾아옴
        //playerCharacter = player.GetComponent<Character>();//플레이어 캐릭터 가져오기
        StartCoroutine("UpdatePath");//UpdatePath 코루틴은 한번 시작되면 0.25초 간격으로 무한 반복 실행됨 
    }
    void OnUpdate()
    {
        if(playerCharacter == null) //적이 없으면 탈출
        {
            return;
        }

        if (playerCharacter.die == true) //적이 죽으면
        {
            StartCoroutine("Reset"); //내 리셋 코루틴 실행
        }
        //자신 캐릭터 사망하면 동작하지 않음
        if (character.die == true)//내가 죽으면
        {
            aiStart = true;  //인공지능 코루틴이 실행되지 않도록 인공지능 코루틴이 시작했다고 설정
            stopCoroutine(); //인공지능 코루틴 중지
            StartCoroutine("Reset"); //내 리셋 코루틴 실행
            return;
        }
        
        if (aiStart == false) //인공지능 코루틴이 시작되지 않았으면
        {
            aiStart = true; //인공지능 시작 bool값 true로 변경
            coroutine = DummyAI(); //인공지능 코루틴 할당
            StartCoroutine(coroutine); //인공지능 코루틴 시작
        }
    }

    /// <summary> 코루틴 중지 </summary>
    public void stopCoroutine()
    {
        if(coroutine!=null)
        StopCoroutine(coroutine);
    }

    /// <summary> 더미 인공지능 </summary>
    IEnumerator DummyAI()
    {
        character.Casting(Define.SkillState.Combat);
        yield return new WaitForSeconds(Random.Range(0.5f,3.0f)); //우선 2초 대기
        skillNum = Random.Range(0, 4);  //스킬 고름
        if (playerCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //스킬 시전
        }
        yield return new WaitForSeconds(Random.Range(2f, 6.0f));   // 4초 대기

        if (skillNum == (int)Define.SkillState.Defense)//시전된 스킬이 디펜스이면
        {
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(playerCharacter); //공격 시도
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(playerCharacter); //공격 시도
            yield return new WaitForSeconds(1.5f); //1.5초 대기
            character.SetTarget(playerCharacter); //공격 시도
        }

        if (skillNum != (int)Define.SkillState.Counter)  // 카운터어택이 아니면 선공 공격이므로
        {

            if (playerCharacter != null)
            {
                character.SetTarget(playerCharacter);  //선공스킬이면 공격
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3.0f)); //2초 대기
        character.SetTarget(null); //타겟 해제
        aiStart = false; //코루틴 재실행 준비

    }

    /// <summary> 적(플레이어)를 계속 바라보는 메서드 </summary>
    void LookAt()
    {
        if (gameObject.layer == (int)Define.Layer.Player) //나 자신이 플레이어이면 상대방(몬스터)을 계속 쳐다보지 않는다
        {
            return;
        }
        if(player != null && playerCharacter.die == false) // 적(플레이어)가 존재하고 사망하지 않았으면
        {
            Vector3 look = player.transform.position;//플레이어 위치
            look.y = transform.position.y; //위 아래를 쳐다보지 않음, 회전만 함
            transform.LookAt(look); //쳐다보기
        }
    }

    /// <summary> 플레이어 사망시 AI 스킬 기본값으로 만드는 코루틴 </summary>
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3.0f); //3초 대기
        character.SetTarget(null); //타겟 비운다
        character.Casting(Define.SkillState.Combat); //컴벳 스킬로 전환
        character.SetOffensive(false); //일상모드로 전환
    }

    /// <summary> 0.25초마다 반복실행되는 플레이어를 찾는 코루틴 </summary>
    private IEnumerator UpdatePath()
    {
        while(!character.die)//이 캐릭터(Enemy)가 살아 있으면
        {
            if((player != null && (player.transform.position - gameObject.transform.position).magnitude > 30)) //플레이어가 너무 멀리 도망가면
            {
                player = null;  //  플레이어 타겟을 푼다
                playerCharacter = null; //  플레이어 타겟의 Caracter 컴포넌트를 푼다
                character.Casting(Define.SkillState.Combat); //컴벳 스킬로 전환
                character.SetOffensive(false); //일상모드로 전환
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, layerMask); //반지름 10짜리 동그란 콜라이더 만든다

            //모든 콜라이더를 순회하면서 살아 있는 Player 찾기
            for (int i = 0; i < colliders.Length; i++)
            {
                //콜라이더로부터 Character 컴포넌트 가져오기
                Character pCharacter = colliders[i].GetComponent<Character>();

                //Character 컴포넌트가 존재하며, 레이어가 플레이어나 가축이고 내가 몬스터이면
                if (pCharacter != null && pCharacter.die ==false && (pCharacter.gameObject.layer == (int)Define.Layer.Player|| pCharacter.gameObject.layer == (int)Define.Layer.Livestock) && character.tag=="Enemy")
                {
                    //추적 대상을 해당 playerCharacter 설정
                    player = pCharacter.gameObject;
                    playerCharacter = player.GetComponent<Character>();
                    LookAt();//타겟을 계속 쳐다봄

                    //for 문 루프 즉시 정지
                    break;
                }
                //Character 컴포넌트가 존재하며, 레이어가 적이고 내가 가축이면
                if (pCharacter != null && pCharacter.die == false && (pCharacter.gameObject.layer == (int)Define.Layer.Enemy) && character.tag == "Friendly")
                {
                    //추적 대상을 해당 playerCharacter 설정
                    player = pCharacter.gameObject;
                    playerCharacter = player.GetComponent<Character>();
                    LookAt();//타겟을 계속 쳐다봄

                    //for 문 루프 즉시 정지
                    break;
                }
            }
            yield return new WaitForSeconds(0.25f); //0.25초마다 반복 실행
        }
    }
}
