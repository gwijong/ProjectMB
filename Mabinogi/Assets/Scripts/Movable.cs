using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> 이동 가능한 오브젝트</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    /// <summary> 내비메시</summary>
    protected NavMeshAgent agent;
    /// <summary> 걷는가?</summary>
    protected bool walk = false;
    /// <summary> 이동 상태</summary>
    [Tooltip("이동 상태")]
    public Define.MoveState state = Define.MoveState.Runnable;

    /// <summary> 이동 속도 </summary>
    [SerializeField] [Tooltip("달리는 속도")] protected float runSpeed;  //하위 클래스의 Start메서드에서 스크립터블 오브젝트 데이터의 내비메시 스피드 할당
    [SerializeField] [Tooltip("걷는 속도")] protected float walkSpeed;  //하위 클래스의 Start메서드에서 스크립터블 오브젝트 데이터의 내비메시 스피드 할당


    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //이 캐릭터의 내비메시 컴포넌트 할당       
        agent.speed = runSpeed;  //기본은 달리는 속도
    }

    /// <summary> 내비게이션 이동 메서드 </summary>
    public virtual void MoveTo(Vector3 goalPosition, bool isWalk = false)  //입력에서 불러옴
    {
        walk = isWalk;
        agent.isStopped = false;
        agent.SetDestination(goalPosition);  //목적지는 goalPosition 좌표
    }

    /// <summary> 내비게이션 이동 정지 메서드 </summary>
    public void MoveStop(bool value) //이동 정지
    {
        agent.isStopped = value;
        if(value) agent.SetDestination(agent.transform.position);//목적지를 내 자신 위치로 갱신
    }
}