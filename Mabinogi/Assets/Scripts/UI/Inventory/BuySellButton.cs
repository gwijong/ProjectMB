using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> 구매 버튼 </summary>
    public void Buy()
    {
        Inventory.store.Buy();
        transform.parent.gameObject.SetActive(false); //구매창 꺼줌
    }
    /// <summary> 판매 버튼</summary>
    public void Sell()
    {
        Inventory.store.Sell();
        transform.parent.gameObject.SetActive(false);  //판매창 꺼줌
    }
}
