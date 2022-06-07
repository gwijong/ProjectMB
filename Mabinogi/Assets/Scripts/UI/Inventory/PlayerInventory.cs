using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 플레이어 캐릭터의 인벤토리 </summary>
public class PlayerInventory : Inventory
{
    public Text goldText;

    protected override void Start()
    {
        base.Start();
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
            goldText.text = owner.gold + " G"; //골드 수치 텍스트를 계속 업데이트 해줌
        }
    }
}
