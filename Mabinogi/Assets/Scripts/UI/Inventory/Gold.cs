using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gold : MonoBehaviour
{
    public int gold;
    public Text text;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        text = GetComponent<Text>();
    }

    void OnUpdate()
    {
        text.text = gold.ToString() + " G";
    }
}
