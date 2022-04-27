using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    GameObject UI;
    Vector3 cameraPos;
    void Start()
    {
        UI = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //UI.transform.LookAt(cameraPos);
        UI.transform.rotation = Quaternion.LookRotation(UI.transform.position - cameraPos);
    }
}
