using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable  //이동 가능한 오브젝트
{
    NavMeshAgent _agent;
    [SerializeField] float _speed;

    bool _movable = true;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)  //입력에서 불러옴
    {
        _agent.SetDestination(goalPosition);
    }
}