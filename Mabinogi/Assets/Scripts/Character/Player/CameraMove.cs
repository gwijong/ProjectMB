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
            cameraRotator.y = Mathf.Clamp(cameraRotator.y, -25, 70);
            cameraPivot.rotation = Quaternion.Euler(cameraRotator.y, cameraRotator.x, 0);
        };

        cameraRotator.z += -Input.GetAxis("Mouse ScrollWheel") * speed;
        mainCamera.fieldOfView = cameraRotator.z;
    }
}