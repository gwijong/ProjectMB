using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetItem(Define.Item.Wool, 10); //상점 인벤토리 실험용 기본 아이템
    }
}
