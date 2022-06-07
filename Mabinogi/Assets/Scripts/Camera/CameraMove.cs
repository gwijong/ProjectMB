using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 카메라 이동 </summary>
public class CameraMove : MonoBehaviour
{
    Camera mainCamera;//메인 카메라 자신
    public Transform cameraPivot; //플레이어 캐릭터에 종속되어있는 카메라 기준점 오브젝트
    public float speed = 20; //회전 속도
    Vector3 cameraRotator = Vector3.forward * 70; //z좌표를 기본적으로 70으로 뒤로 당겨서 멀리 보이게 함

    private void Start()
    {
        mainCamera = GetComponent<Camera>(); //자신의 카메라 컴포넌트 가져오기
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        if (Input.GetMouseButton(1)) //마우스 우클릭 입력 들어오면
        {
            cameraRotator.x += Input.GetAxis("Mouse X"); //마우스 좌우 입력 받아서 cameraRotator.x 좌표에 더함
            cameraRotator.y -= Input.GetAxis("Mouse Y"); //마우스 상하 입력 받아서 cameraRotator.y 좌표에 더함

            //카메라 각도를 정수리를 바라볼 정도로 너무 높게 하지는 못함
            cameraRotator.y = Mathf.Clamp(cameraRotator.y, (int)-25 / speed, (int)60 / speed); 

            //카메라를 회전하는게 아니라 카메라가 달려있는 피봇을 회전시킴
            cameraPivot.rotation = Quaternion.Euler(cameraRotator.y * speed, cameraRotator.x * speed, 0);
        };

        //마우스 휠 값 입력받아서 카메라 줌인 줌아웃
        cameraRotator.z += -Input.GetAxis("Mouse ScrollWheel") * speed * 2;
        cameraRotator.z = Mathf.Clamp(cameraRotator.z, 25, 80); //너무 가깝거나 멀지 않도록 제약
        mainCamera.fieldOfView = cameraRotator.z;
    }
}