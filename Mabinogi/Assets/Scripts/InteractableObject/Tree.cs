using UnityEngine.AI; // 내비메시 관련 코드
using UnityEngine;

public class Tree : Hitable
{
    public GameObject[] items; //생성할 아이템
    public float maxDistance = 3f; // 플레이어 위치에서 아이템이 배치될 최대 반경

    public override bool TakeDamage(Character from)
    {
        //플레이어 근처에서 내비메시 위의 랜덤 위치 가져오기
        Vector3 spawnPosition = GetRandomPointOnNavMesh(from.transform.position, maxDistance);//매개변수 2개
        //바닥에서 0.5만큼 위로 올리기
        spawnPosition += Vector3.up * 0.5f;

        //아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        //생성된 아이템을 5초 뒤에 파괴
        Destroy(item, 5f);
        return true;      
    }

    private void Spawn()
    {

    }

    //내비메시 위의 랜덤한 위치를 반환하는 메서드
    //center를 중심으로 distance 반경 안에서의 랜덤한 위치를 찾음
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
