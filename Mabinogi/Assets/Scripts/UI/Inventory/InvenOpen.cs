using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 인벤토리 창 열고 닫음</summary>
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

    /// <summary> 인벤토리 열기</summary>
    public void Open()
    {
        if (isOpen) //소지품창이 열려있으면 닫기
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//닫기 효과음
            inven.GetComponent<Inventory>().use.SetActive(false);
            inven.transform.position += new Vector3(2000, 0, 0);
            isOpen = false;
        }
        else //소지품창이 닫혀있으면 열기
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_open);//열기 효과음
            inven.transform.position += new Vector3(-2000, 0, 0);
            isOpen = true;
        }
    }
}
