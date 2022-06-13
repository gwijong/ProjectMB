using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 화면 상단 UI 버튼 사용 </summary>
public class UIUsePotion : MonoBehaviour
{
    /// <summary> 생명력 포션 사용 </summary>
    public void HPUse()
    {
        Inventory.EatItem(Define.Item.LifePotion);
    }

    /// <summary> 마나 포션 사용 </summary>
    public void MPUse()
    {
        Inventory.EatItem(Define.Item.ManaPotion);
    }
}
