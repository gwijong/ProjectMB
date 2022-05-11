using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    /// <summary> 아이템 획득 버튼 메서드</summary>
    public void GetItem()
    {
        GetComponentInParent<ItemInpo>().GetItem();
    }
}
