using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 해당 버튼을 누르면 버튼과 플레이어의 상호작용</summary>
public class NameButton : MonoBehaviour
{
    /// <summary> 상호작용 버튼 메서드</summary>
    public void GetItem()
    {
        GameManager.manager.GetComponent<PlayerController>().SetTarget(GetComponentInParent<Interactable>());        
    }
}
