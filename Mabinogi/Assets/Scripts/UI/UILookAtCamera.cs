using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    static Transform _mainCamera;
    static Transform mainCamera
    {
        get
        {
            if (_mainCamera == null) _mainCamera = Camera.main.transform;

            return _mainCamera;
        }
    }

    GameObject UI;
    Vector3 cameraPos;
    void Start()
    {
        UI = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UI.transform.rotation = mainCamera.rotation;
    }
}
