using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera mainCamera;
    public Transform cameraPivot;
    public float speed = 20;
    Vector3 cameraRotator = Vector3.forward * 60;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cameraRotator.x += Input.GetAxis("Mouse X");
            cameraRotator.y -= Input.GetAxis("Mouse Y");
            cameraRotator.y = Mathf.Clamp(cameraRotator.y, (int)-25/speed, (int)60 /speed);
            cameraPivot.rotation = Quaternion.Euler(cameraRotator.y * speed, cameraRotator.x * speed, 0) ;
        };

        cameraRotator.z += -Input.GetAxis("Mouse ScrollWheel") * speed*2;
        cameraRotator.z = Mathf.Clamp(cameraRotator.z, 25 , 60);
        mainCamera.fieldOfView = cameraRotator.z;
    }
}