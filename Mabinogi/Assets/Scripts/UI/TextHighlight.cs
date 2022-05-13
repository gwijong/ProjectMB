using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//이 스크립트는 이름 강조 버튼용 캔버스에 달려있다.  
/// <summary> 캐릭터나 아이템 이름 텍스트의 텍스트 강조 버튼UI 조작</summary>
public class TextHighlight : MonoBehaviour
{
    /// <summary>텍스트 강조 버튼UI</summary>
    GameObject ui;
    private void Start()
    {
        //업데이트 매니저의 Update메서드에 몰아주기
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
        ui = GetComponentInChildren<Button>().gameObject; //버튼을 가지고 있는 자식 게임오브젝트
    }

    void OnUpdate()
    {
        if (ui != null)
        {
            if (Input.GetKey(KeyCode.LeftAlt)) //왼쪽 알트키를 누르면
            {
                ui.SetActive(true); //버튼 게임오브젝트 활성화
            }
            else //평소엔 버튼 게임오브젝트 비활성화
            {
                ui.SetActive(false); //버튼 게임오브젝트 비활성화
            }
        }
    }
}
