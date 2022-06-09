using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 구매, 판매 버튼 UI </summary>
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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        Inventory.store.Sell();
        transform.parent.gameObject.SetActive(false);  //판매창 꺼줌
    }
}
