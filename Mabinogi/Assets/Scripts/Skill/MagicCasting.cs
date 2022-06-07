using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    public Transform character;

    public float yPosition;
    // 반지름.
    public float radius = 2.0f;
    // 회전 속도.
    public float angularVelocity = 40.0f;
    // 위치.
    public float angle = 0.0f;

    void Update()
    {
        // 회전 각도.
        angle += angularVelocity * Time.deltaTime;
        // 오프셋 위치.
        Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * new Vector3(0.0f, 0.0f, radius);
        // 이펙트 위치.
        transform.position = new Vector3(character.transform.position.x, yPosition, character.transform.position.z) + offset;
    }
}
