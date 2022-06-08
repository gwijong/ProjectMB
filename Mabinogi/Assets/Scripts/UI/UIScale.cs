using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    /// <summary> 원래 크기</summary>
    Vector3 scale;
    /// <summary> 카메라와의 거리</summary>
    float distance;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        scale = gameObject.transform.localScale;
    }

    void OnUpdate()
    {
        distance = (gameObject.transform.position - Camera.main.transform.position).magnitude;
        if (distance / 20 < 1)//거리가 가까워서 1보다 작아질 경우
        {
            distance = 20; //나눌 값인 20으로 보정
        }
        gameObject.transform.localScale = scale;//일단 크기 초기화
        gameObject.transform.localScale = gameObject.transform.localScale * distance / 20;//새 크기 설정
    }
}
