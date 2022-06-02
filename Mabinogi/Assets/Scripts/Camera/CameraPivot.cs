using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform following_object; //따라다닐 플레이어 트랜스폼
    public float Ypos = 2;
    private void Start()
    {
        following_object = GameObject.FindGameObjectWithTag("Player").transform; //플레이어 트랜스폼 할당
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }
    private void OnUpdate()
    {
        Vector3 pos = transform.position;//현재 위치
        Vector3 followPos = new Vector3 (following_object.position.x, following_object.position.y+ Ypos, following_object.position.z);
        //따라다닐 플레이어 오브젝트로 이 오브젝트 계속 이동
        transform.position = Vector3.Lerp(pos, followPos, 0.4f); //현재 위치에서 플레이어 위치로 부드럽게 이동
    }
}
