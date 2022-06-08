using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    /// <summary> ���� ũ��</summary>
    Vector3 scale;
    /// <summary> ī�޶���� �Ÿ�</summary>
    float distance;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        scale = gameObject.transform.localScale;
    }

    void OnUpdate()
    {
        distance = (gameObject.transform.position - Camera.main.transform.position).magnitude;
        if (distance / 20 < 1)//�Ÿ��� ������� 1���� �۾��� ���
        {
            distance = 20; //���� ���� 20���� ����
        }
        gameObject.transform.localScale = scale;//�ϴ� ũ�� �ʱ�ȭ
        gameObject.transform.localScale = gameObject.transform.localScale * distance / 20;//�� ũ�� ����
    }
}