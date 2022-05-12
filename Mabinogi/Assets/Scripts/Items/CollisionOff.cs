using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  아이템과 플레이어간의 물리 충돌을 못하게 함 </summary>
public class CollisionOff : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Off());//아이템 생성 시 코루틴 시작
    }

/// <summary> 아이템 생성 3초뒤에 필드 아이템의 리지드바디와 콜라이더를 조작해 충돌 방지 </summary>
    IEnumerator Off()
    {
        yield return new WaitForSeconds(3.0f); //3초 대기
        gameObject.GetComponent<Rigidbody>().useGravity = false; //리지드바디의 중력 비활성화
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //리지드바디의 속도 초기화
        gameObject.GetComponent<BoxCollider>().isTrigger = true; //박스콜라이더의 트리거 체크
    }
}
