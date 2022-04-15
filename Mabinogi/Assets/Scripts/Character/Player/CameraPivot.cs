using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform following_object;

    private void Start()
    {
        following_object = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = Vector3.Lerp(pos, following_object.position, 0.4f);
    }
}
