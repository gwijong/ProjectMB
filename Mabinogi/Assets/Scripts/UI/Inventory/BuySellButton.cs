using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> 구매 버튼 </summary>
    public void Buy()
    {

        Inventory[] inven = FindObjectsOfType<Inventory>(); 
        StoreInventory store = FindObjectOfType<StoreInventory>();
        inven[0].GetItem(store.item, 1); //상점에서 마지막으로 마우스 클릭한 아이템 생성해서 인벤토리에 추가
        transform.parent.gameObject.SetActive(false); //구매창 꺼줌
    }
    /// <summary> 판매 버튼</summary>
    public void Sell()
    {
        Inventory.mouseItem.Clear(); //마우스가 집고있는 아이템 비움
        transform.parent.gameObject.SetActive(false);  //판매창 꺼줌
    }
}
