using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> 구매 버튼 </summary>
    public void Buy()
    {
        Inventory[] invenArray = FindObjectsOfType<Inventory>();  //상점 인벤토리 포함 모든 인벤토리들 싹 다 가져옴
        List<Inventory> invenList = new List<Inventory>(); //배열을 리스트로 변환할 리스트
        StoreInventory store = FindObjectOfType<StoreInventory>(); //상점 인벤토리
        for(int i = 0; i< invenArray.Length; i++)//배열을 리스트로 변환
        {
            invenList.Add(invenArray[i]);
        }

        for (int i = 0; i < invenList.Count; i++)
        {
            if(invenList[i] == store)//인벤토리 리스트에서 상점 인벤토리만 빼줌
            {
                invenList.Remove(invenList[i]);
            }
        }
        if(FindObjectOfType<Gold>().gold>= store.item.GetItemData().SalePrice)//남은 돈이 물품 가격보다 크면
        {
            FindObjectOfType<Gold>().gold -= store.item.GetItemData().SalePrice;//물품 가격만큼 빼주기
            //invenList에는 상점 인벤토리를 제외한 개인 소지품창만 남음
            invenList[0].GetItem(store.item, 1); //상점에서 마지막으로 마우스 클릭한 아이템 생성해서 인벤토리에 추가
            transform.parent.gameObject.SetActive(false); //구매창 꺼줌
        }

    }
    /// <summary> 판매 버튼</summary>
    public void Sell()
    {
        FindObjectOfType<Gold>().gold += Inventory.mouseItem.GetItemType().GetItemData().SalePrice* Inventory.mouseItem.amount;//남은 돈에 판매가격 더함
        Inventory.mouseItem.Clear(); //마우스가 집고있는 아이템 비움
        transform.parent.gameObject.SetActive(false);  //판매창 꺼줌
    }
}
