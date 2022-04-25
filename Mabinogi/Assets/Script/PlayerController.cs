using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>플레이어 캐릭터 딱 하나</summary>
    Character player;
    /// <summary>마우스로 클릭한 타겟</summary>
    Character target;
    /// <summary>Ground 레이어와 Enemy 레이어의 레이어마스크</summary>
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy;

    private void Start()
    {      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();  //플레이어 태그 찾아서 가져옴
    }

    void Update()
    {
        SkillInput();
        MouseInput();        
        KeyMove();
        SpaceOffensive();
    }

    /// <summary>스페이스바 입력받아 일상, 전투모드 전환</summary>
    void SpaceOffensive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetOffensive();
        };
    }

    /// <summary>마우스 입력 이동이나 타겟 지정</summary>
    void MouseInput()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))  //마우스 좌클릭 입력되면
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //카메라에서 마우스좌표로 레이를 쏨
            RaycastHit hit;  //충돌 물체 정보 받아올 데이터 컨테이너

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//레이, 충돌 정보, 레이 거리, 레이어마스크
            {
                target = hit.collider.GetComponent<Character>();  //충돌한 대상이 캐릭터면 타겟에 할당 시도

                //캐릭터 대상 할당 실패            충돌한 대상이 땅이면
                if(!player.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    player.MoveTo(hit.point);  //충돌한 좌표로 플레이어 캐릭터 이동
                };
            };
        };
    }

    /// <summary>키보드 입력 이동</summary>
    void KeyMove()
    {   //                                              가로                         세로
        Vector2 keyInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (keyInput.magnitude > 1.0f) //키 입력 벡터2 좌표 길이가 1보다 큰 경우  ex)대각선 길이가 루트2가 된 경우
        {
            keyInput.Normalize(); //1로 정규화
        };

        if(keyInput.magnitude > 0.1f)  //키 입력 벡터 2 길이가 0.1보다 큰 경우 이동
        {
            Vector3 cameraForward = Camera.main.transform.forward;  //카메라 앞 방향
            cameraForward.y = 0.0f;//높이 값 무시
            cameraForward.Normalize();//1로 정규화
        
            Vector3 cameraRight = Camera.main.transform.right;  //카메라 오른쪽 방향
            cameraRight.y = 0.0f;//높이 값 무시
            cameraRight.Normalize();//1로 정규화

            cameraForward *= keyInput.y;  //키보드 WS 입력값 받음
            cameraRight *= keyInput.x;  //키보드 AD 입력값 받음

            Vector3 calculatedLocation = cameraForward + cameraRight;  //계산된 좌표
            calculatedLocation += player.transform.position; //계산된 좌표에 플레이어의 현재 위치를 더함

            player.SetTarget(null);//키보드 이동중이므로 지정된 타겟을 비움
            player.MoveTo(calculatedLocation); //플레이어위치에서 계산된 좌표로 조금씩 이동
        };
    }

    /// <summary>키보드 입력으로 스킬 시전</summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("플레이어: 디펜스 시전");
            player.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("플레이어: 스매시 시전");
            player.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("플레이어: 카운터 시전");
            player.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("플레이어: 스킬 취소, 컴벳으로 전환");
            player.Casting(Define.SkillState.Combat);
        }


    }
}


