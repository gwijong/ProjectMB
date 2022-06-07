using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary> 던전 진행</summary>
public class Dungeon : MonoBehaviour
{
    /// <summary> 던전 진행도 </summary>
    int progress = 0;
    /// <summary> 문 오브젝트의 충돌 </summary>
    public GameObject[] gate;
    /// <summary> 왼쪽 문짝 </summary>
    public GameObject[] leftDoor;
    /// <summary> 오른쪽 문짝 </summary>
    public GameObject[] rightDoor;
    /// <summary> 적 몬스터들 </summary>
    public GameObject[] enemys;
    /// <summary> 몬스터 등장 좌표들 </summary>
    public Transform[] spawnPos;
    /// <summary> 현재 생성한 몬스터 </summary>
    GameObject enemy = null;
    /// <summary> 생성할 몬스터 숫자 </summary>
    public int[] spawnAmount;
    /// <summary> 한 라운드에 생성한 모든 몬스터들 </summary>
    List<GameObject> enemyList = new List<GameObject>();

    void Update()
    {
        if(progress - 1 < 0)  //진행도가 음수면 리턴
        {
            return;
        }
        for(int i = 0; i< spawnAmount[progress-1]; i++)
        {
            if(enemyList.Count == 0) //적이 0마리면 리턴
            {
                return;
            }
      
            if(enemyList[i].GetComponent<Character>().die == false) //적이 한마리라도 살아있으면 리턴
            {
                return;
            }
        }
        //적이 전멸한 경우
        gate[progress - 1].GetComponent<BoxCollider>().enabled = false; //문 열어줌
        leftDoor[progress - 1].SetActive(false); //왼쪽 문 치움
        rightDoor[progress - 1].SetActive(false); //오른쪽 문 치움
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_door, gate[progress-1].transform.position); //문 여는 효과음
        enemyList.Clear(); //적 리스트 초기화
    }

    /// <summary> 몬스터 스폰 </summary>
    public void Spawn()
    {      
        for (int i = 0; i< spawnAmount[progress]; i++)
        {
            Vector3 pos = spawnPos[progress].position; //스폰할 위치
            enemy = Instantiate(enemys[progress]); //몬스터 스폰함
            enemy.GetComponent<NavMeshAgent>().enabled = false; //몬스터를 원하는 위치로 옮기기 위해 내비메시 잠깐 끔
            //지정한 좌표에서 살짝 랜덤한 위치에 몬스터 생성함
            pos.x += Random.Range(-1.0f, 1.0f) * spawnAmount[progress];
            pos.z += Random.Range(-1.0f, 1.0f) * spawnAmount[progress];
            enemy.transform.position = pos;
            enemyList.Add(enemy); //전멸했는지 체크하는 리스트에 추가
            enemy.GetComponent<NavMeshAgent>().enabled = true; //내비메시 켬
        }
        progress++;//던전 진행도 더하기
    }

}
