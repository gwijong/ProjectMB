using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : MonoBehaviour
{
    Character character;//인공지능 몬스터 나 자신
    GameObject player; //적(플레이어)
    Character playerCharacter; //적(플레이어 캐릭터 컴포넌트)
    bool aiStart = false; //인공지능 시작 체크
    int skillNum; //인공지능이 시전할 랜덤 스킬 번호
    IEnumerator coroutine;

    private void OnEnable()
    {
        aiStart = false;
        coroutine = DummyAI();
    }
    private void Start()
    {
        character = gameObject.GetComponent<Character>();//이 몬스터 캐릭터 가져오기
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾아옴
        playerCharacter = player.GetComponent<Character>();//플레이어 캐릭터 가져오기       
    }
    void OnUpdate()
    {
        if (character.die)//자신 캐릭터 사망시 동작하지 않음
        {
            aiStart = true;
            stopCoroutine(); //인공지능 코루틴 중지
            return;
        }
        if (aiStart == false) //인공지능 코루틴이 시작되지 않았으면
        {
            aiStart = true;
            coroutine = DummyAI();
            StartCoroutine(coroutine); //인공지능 코루틴 시작
        }
        LookAt();//타겟을 계속 쳐다봄
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
        yield return new WaitForSeconds(2.0f); //우선 2초 대기
        skillNum = Random.Range(0, 4);  //스킬 고름
        character.Casting((Define.SkillState)skillNum);  //스킬 시전
        yield return new WaitForSeconds(4.0f);   // 4초 대기
        if (skillNum != 1 && skillNum != 3)  // 선공스킬인지 체크
        {
            character.SetTarget(playerCharacter);  //선공스킬이면 공격
        }
        yield return new WaitForSeconds(2.0f); //2초 대기
        character.SetTarget(null); //타겟 해제
        aiStart = false; //코루틴 재실행 준비

    }

    /// <summary> 적(플레이어)를 계속 바라보는 메서드 </summary>
    void LookAt()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾아옴
        playerCharacter = player.GetComponent<Character>();//플레이어 캐릭터 가져오기
        Vector3 look = player.transform.position;//플레이어 위치
        look.y = transform.position.y; //위 아래를 쳐다보지 않음, 회전만 함
        transform.LookAt(look);
    }
}
