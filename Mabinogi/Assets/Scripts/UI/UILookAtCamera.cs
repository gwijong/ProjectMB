using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> UI가 카메라를 향해 바라보도록 하는 스크립트</summary>
public class UILookAtCamera : MonoBehaviour
{
    /// <summary> 메인 카메라</summary>
    Transform mainCamera;
    /// <summary> 카메라 각도에 따라 회전시킬 오브젝트</summary>
    GameObject UI;



    void Start()
    {
        UI = gameObject; //나 자신 게임오브젝트
        mainCamera = Camera.main.transform;//메인카메라의 트랜스폼
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        
    }


    public void OnUpdate()
    {

        if (UI == null) //회전시킬 게임오브젝트가 파괴되면 실행 안함
        {
            return;
        }
        //UI의 회전값을 카메라 회전값으로 계속 갱신
        UI.transform.rotation = mainCamera.rotation;
    }
}
