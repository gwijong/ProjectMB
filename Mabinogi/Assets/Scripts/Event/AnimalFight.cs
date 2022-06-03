using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AnimalFight : MonoBehaviour
{
    /// <summary> 적 리스트 </summary>
    public GameObject[] enemys;
    /// <summary> 적이 대기할 공간 </summary>
    public Transform waitPos;
    /// <summary> 적이 대기한 공간에서 돌아올 좌표 </summary>
    Vector3[] spawnPos;
    private void Start()
    {
        spawnPos = new Vector3[enemys.Length];
        for (int i = 0; i < enemys.Length; i++)
        {
            spawnPos[i] = enemys[i].transform.position;//스폰할 위치 지정
            enemys[i].GetComponent<NavMeshAgent>().enabled = false;//내비메시를 꺼줌
            enemys[i].transform.position = waitPos.position+ Vector3.up;//대기할 공간으로 보냄
        }
    }


    private void OnTriggerEnter(Collider other) //트리거 콜라이더 진입 시 여우와 늑대를 배치함
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            for(int i = 0; i< enemys.Length; i++)
            {
                enemys[i].transform.position = spawnPos[i];//스폰 좌표로 적들 배치
                enemys[i].GetComponent<NavMeshAgent>().enabled = true;   //내비메시 켜줌         
            }
        }
    }

    private void OnTriggerExit(Collider other) //트리거 콜라이더 탈출 시 여우와 늑대를 멀리 치워버림
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<NavMeshAgent>().enabled = false; //내비메시 끔
                enemys[i].transform.position = waitPos.position + Vector3.up; //대기 장소로 이동
            }
        }
    }
}
