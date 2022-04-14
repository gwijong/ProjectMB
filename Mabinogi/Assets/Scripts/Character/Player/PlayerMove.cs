using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //사용자 입력에 따라 플레이어 캐릭터를 움직이는 스크립트
    public float moveSpeed = 5f;  //앞뒤 움직임의 속도
    public float rotateSpeed = 180f;  //좌우 회전 속도

    private PlayerInput playerInput;  //플레이어 입력을 알려주는 컴포넌트
    private Rigidbody rigid;  //플레이어 캐릭터의 리지드바디
    private Animator ani;   //플레이어 캐릭터의 애니메이터
    private Character character;

    void Start()
    {//사용할 컴포넌트들의 참조 가져오기
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();
    }

    //FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    void FixedUpdate()
    {//물리 갱신 주기마다 움직임 실행
        if (character.stiffnessCount != 0 || character.die)
        {
            return;
        }
        KeyMove();
        PlayerAni();
        MouseMove();

    }

    //입력값에 따라 캐릭터를 앞뒤로 움직임
    void KeyMove()
    {
        Vector3 dir = new Vector3(0,0,0);

        if (playerInput.Front)
        {           
            dir = transform.forward;
        }
        else if (playerInput.Back)
        {
            dir = -transform.forward;
        }
        else if (playerInput.Right)
        {           
            dir = transform.right;
        }
        else if (playerInput.Left)
        {
            dir = -transform.right;
        }
        else
        {
            dir = new Vector3(0, 0, 0);
        }

        //상대적으로 이동할 거리 계산
        Vector3 MoveDistance = dir * moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + MoveDistance);     

    }
    void PlayerAni()
    {
        if (playerInput.Front == true || playerInput.Back == true || playerInput.Left == true || playerInput.Right == true)
        {
            character.AniOff();
            ani.SetBool("Move", true);
        }
        else
        {
            ani.SetBool("Move", false);
        }
    }

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    void MouseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit raycastHit;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // 이동 지점
                movePos = raycastHit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        }
        if (movePos != Vector3.zero)
        {
            character.AniOff();
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            // 방향을 구한다. 
            Vector3 dir = movePos - transform.position;

            // 방향을 이용해 회전각을 구한다.
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            // 회전 및 이동 
            transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        }
        // 현재위치와 목표위치 사이의 거리를 구한다.
        float dis = Vector3.Distance(transform.position, movePos);

        // 목표지점 도달시 이동지점을 초기화해 추가적인 움직임을 제한한다. 
        if (dis <= 2f)
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            movePos = Vector3.zero;
        }
    }
}
