using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //사용자 입력에 따라 플레이어 캐릭터를 움직이는 스크립트
    public float moveSpeed = 5f;  //앞뒤 움직임의 속도
    public float rotateSpeed = 180f;  //좌우 회전 속도

    private PlayerInput playerInput;  //플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody;  //플레이어 캐릭터의 리지드바디
    private Animator playerAnimator;   //플레이어 캐릭터의 애니메이터

    void Start()
    {//사용할 컴포넌트들의 참조 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    //FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    void FixedUpdate()
    {//물리 갱신 주기마다 움직임 실행
        Move();
        PlayerAni();
    }

    //입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        Vector3 dir = new Vector3(0,0,0);
        if (playerInput.Front)
        {
            dir = new Vector3 (0, 0, -1);
        }
        else if (playerInput.Back)
        {
            dir = new Vector3(0, 0, 1);
        }
        else if (playerInput.Right)
        {
            dir = new Vector3(-1, 0, 0);
        }
        else if (playerInput.Left)
        {
            dir = new Vector3(1, 0, 0);
        }
        else
        {
            dir = new Vector3(0, 0, 0);
        }

        //상대적으로 이동할 거리 계산
        Vector3 MoveDistance = dir * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + MoveDistance);
    }

    void PlayerAni()
    {
        if (playerInput.Front == true || playerInput.Back == true || playerInput.Left == true || playerInput.Right == true)
        {
            playerAnimator.SetBool("Move", true);
        }
        else
        {
            playerAnimator.SetBool("Move", false);
        }
    }

}
