using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveState
{  //속도만 다르게 해줌
   //걷고 뛰는건 Character 에서 해줌
    Rooted,
    Walkable,
    Runnable,
}

/// <summary> 이동 가능한 오브젝트</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;

    public MoveState state = MoveState.Runnable;

    /// <summary> 이동 속도 </summary>
    [SerializeField] float runSpeed;  //Start메서드에 내비메시 스피드 할당
    [SerializeField] float walkSpeed;  //Start메서드에 내비메시 스피드 할당


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        agent.speed = runSpeed;
    }

    /// <summary> 내비게이션 이동 메서드 </summary>
    public virtual void MoveTo(Vector3 goalPosition)  //입력에서 불러옴
    {
        agent.isStopped = false;
        agent.SetDestination(goalPosition);
    }

    /// <summary> 내비게이션 이동 정지 메서드 </summary>
    public void MoveStop(bool value) //이동 정지
    {
        agent.isStopped = value;
        if(value) agent.SetDestination(agent.transform.position);
    }
}