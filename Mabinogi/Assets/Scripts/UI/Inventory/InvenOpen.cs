using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenOpen : MonoBehaviour
{
    /// <summary> 인벤토리 창</summary>
    public GameObject inven;
    /// <summary> 인벤토리 창이 열려있는지 체크</summary>
    bool isOpen = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //I키를 누르면 소지품창 오픈
        {
            Open();
        }
    }

    public void Open()
    {
        if (isOpen) //소지품창이 열려있으면 닫기
        {
            inven.transform.position += new Vector3(2000, 0, 0);
            isOpen = false;
        }
        else //소지품창이 닫혀있으면 열기
        {
            inven.transform.position += new Vector3(-2000, 0, 0);
            isOpen = true;
        }
    }
}
