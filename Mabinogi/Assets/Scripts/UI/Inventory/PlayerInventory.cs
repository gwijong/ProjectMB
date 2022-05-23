using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : Inventory
{
    public Text goldText;
    protected override void Start()
    {
        base.Start();
        GetItem(Define.Item.LifePotion, 10); //상점 인벤토리 실험용 기본 아이템
        GetItem(Define.Item.ManaPotion, 10); //상점 인벤토리 실험용 기본 아이템
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (owner == null || goldText == null)
        {
            return;
        }
        else
        {
            goldText.text = owner.gold + " G";
        }
    }

}
