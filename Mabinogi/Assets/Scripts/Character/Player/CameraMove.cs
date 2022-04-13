using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform cameraPivot;
    public float speed = 20;
    float x;
    float y;
    float z = 60;
    Vector2 mousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Input.mousePosition;
        }else if (Input.GetMouseButton(1))
        {
            x = Input.mousePosition.x - mousePos.x;
            y = Input.mousePosition.y - mousePos.y;
            if (Input.mousePosition.y - mousePos.y > 70)
            {
                y = 70;
            }
            if (Input.mousePosition.y - mousePos.y < -25)
            {
                y = -25;
            }
            cameraPivot.rotation = Quaternion.Euler(y, x, 0);
        }

        z += -Input.GetAxis("Mouse ScrollWheel")*speed;
        gameObject.GetComponent<Camera>().fieldOfView = z;
    }
}