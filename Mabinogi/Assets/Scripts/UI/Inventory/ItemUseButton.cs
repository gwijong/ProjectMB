using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseButton : MonoBehaviour
{
    /// <summary> 아이템 사용 버튼 </summary>
    public void Use()
    {
        FindObjectOfType<Inventory>().Use();
    }
    /// <summary> 아이템 나누기 버튼 </summary>
    public void Divide()
    {
        FindObjectOfType<Inventory>().Divide();
    }
    /// <summary> 아이템 버리기 버튼 </summary>
    public void Drop()
    {
        FindObjectOfType<Inventory>().Drop();
    }
}
