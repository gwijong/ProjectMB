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
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++)
        {
            GetItem((Define.Item)i+1, PlayerPrefs.GetInt(i.ToString()));
        }
        owner.gold = PlayerPrefs.GetInt("gold");
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
        Save();
    }
    void Save()
    {
        PlayerPrefs.SetInt("gold", owner.gold);
        int itemId = 0;
        int amount = 0;
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++) {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (infoArray[y, x].GetItemType() == (Define.Item)itemId+1)
                    {
                        amount += infoArray[y, x].amount;
                    }
                }
            }
            PlayerPrefs.SetInt(itemId.ToString(), amount);           
            itemId++;
            amount = 0;
        }
    }
}
