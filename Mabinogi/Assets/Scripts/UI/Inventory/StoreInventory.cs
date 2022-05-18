using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInventory : Inventory
{
    public override void LeftClick(Vector2Int pos)
    {
        if (mouseItem.GetItemType() == Define.Item.None)
        {
            Debug.Log(CheckItemRoot(pos).GetItemType());
        }
        else
        {
            Debug.Log(mouseItem.GetItemType());
        }
    }
}
