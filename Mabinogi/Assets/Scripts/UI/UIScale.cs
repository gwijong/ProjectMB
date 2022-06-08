using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    Vector3 scale;
    float distance;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        scale = gameObject.transform.localScale;
    }
    // Update is called once per frame
    void OnUpdate()
    {
        distance = (gameObject.transform.position - Camera.main.transform.position).magnitude;
        if (distance / 20 < 1)
        {
            distance = 20;
        }
        gameObject.transform.localScale = scale;
        gameObject.transform.localScale = gameObject.transform.localScale * distance / 20;
    }
}
