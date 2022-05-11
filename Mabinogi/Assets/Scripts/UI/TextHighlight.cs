using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextHighlight : MonoBehaviour
{
    GameObject ui;
    private void Start()
    {
        //업데이트 매니저의 Update메서드에 몰아주기
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
        ui = GetComponentInChildren<Canvas>().gameObject;
    }

    void OnUpdate()
    {
        if (ui != null)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                ui.SetActive(true);
            }
            else
            {
                ui.SetActive(false);
            }
        }
    }
}
