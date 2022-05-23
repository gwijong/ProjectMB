using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 해당 버튼을 누르면 버튼과 플레이어의 상호작용</summary>
public class NameButton : MonoBehaviour
{
    /// <summary> 상호작용 버튼 메서드</summary>
    public void GetItem()
    {
        //알트 키로 이름 강조할때 이름 버튼 누르면 상호작용하는 메서드
        GameManager.manager.GetComponent<PlayerController>().SetTarget(GetComponentInParent<Interactable>());        
    }
}
