using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 내비메시 관련 코드

public class ItemManager : MonoBehaviour
{
    /// <summary> 아이템 데이터 스크립터블 오브젝트 </summary>
    public ItemData[] data;
    /// <summary> 플레이어 위치에서 아이템이 배치될 최대 반경 </summary>
    public float maxDistance = 3f;
    /// <summary> 아이템이 생성될 높이 </summary>
    public float yPos = 2f;

    /// <summary> 아이템 버리기 </summary>
    public void DropItem(Define.Item item, int currentAmount)
    {
        GameObject dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ItemPrefab"));//버릴 아이템

        if (dropitem == null) return; //프리팹 불러오기 실패하면 탈출

        dropitem.GetComponent<CreateItem>().amount = currentAmount; //바닥에 떨어진 아이템의 개수는 매개변수 currentAmount의 개수이다.
        dropitem.GetComponent<CreateItem>().item = item; //CreateItem 스크립트의 item 종류에 매개변수 item을 대입

        //플레이어 근처에서 내비메시 위의 랜덤 위치 가져오기
        Vector3 spawnPosition = GetRandomPointOnNavMesh(GameObject.FindGameObjectWithTag("Player").transform.position, maxDistance);
        //바닥에서 yPos만큼 y좌표 위로 올리기
        spawnPosition += Vector3.up * yPos;
        dropitem.transform.position = spawnPosition;//플레이어 좌표를 중심으로 maxDistance범위 안에 yPos 높이 좌표로 아이템 생성
    }

    //내비메시 위의 랜덤한 위치를 반환하는 메서드
    /// <summary> center를 중심으로 distance 반경 안에서의 랜덤한 위치를 찾음 </summary>
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center를 중심으로 반지름이 maxDistance인 구 안에서의 랜덤한 위치 하나를 저장
        // Random.insideUnitSphere는 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
        Vector3 randomPos = (Random.insideUnitSphere * distance) + center;

        //내비메시 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;
        //maxDistance 반경 안에서 randomPos에 가장 가까운 내비메시 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);//out = 출력전용 매개변수
        //찾은 점 반환
        return hit.position;
    }
}
