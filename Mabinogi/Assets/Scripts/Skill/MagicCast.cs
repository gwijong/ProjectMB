using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 마법 시전 후 캐릭터 주위를 뱅뱅 도는 마법 </summary>
public class MagicCast : MonoBehaviour
{
    /// <summary> 뱅뱅 돌 기준점 </summary>
    public Transform character;
    /// <summary> 높이 </summary>
    public float yPosition;
    /// <summary> 반지름 </summary>
    public float radius = 2.0f;
    /// <summary> 회전 속도 </summary>
    public float angularVelocity = 40.0f;
    /// <summary> 회전 각도 </summary>
    public float angle = 0.0f;

    void Update()
    {
        // 회전 각도.
        angle += angularVelocity * Time.deltaTime;
        // 오프셋 위치.
        Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * new Vector3(radius, 0.0f, 0.0f); //Y축을 중심으로 게임오브젝트가 회전, 뱅뱅 도는데 캐릭터와의 간격을 radius만큼 줌;
        // 이펙트 위치.
        transform.position = new Vector3(character.transform.position.x, yPosition, character.transform.position.z) + offset;
    }
}
