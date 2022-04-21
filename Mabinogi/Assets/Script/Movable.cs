using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;

    public Define.MoveState state = Define.MoveState.Runnable;

    /// <summary> �̵� �ӵ� </summary>
    [SerializeField] protected float runSpeed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�
    [SerializeField] protected float walkSpeed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        agent.speed = runSpeed;
    }

    /// <summary> ������̼� �̵� �޼��� </summary>
    public virtual void MoveTo(Vector3 goalPosition)  //�Է¿��� �ҷ���
    {
        agent.isStopped = false;
        agent.SetDestination(goalPosition);
    }

    /// <summary> ������̼� �̵� ���� �޼��� </summary>
    public void MoveStop(bool value) //�̵� ����
    {
        agent.isStopped = value;
        if(value) agent.SetDestination(agent.transform.position);
    }
}