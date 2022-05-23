using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 바닥에 떨어진 아이템 정보</summary>
public class ItemInpo : MonoBehaviour
{
    /// <summary> 바닥에 떨어진 아이템 종류</summary>
    public Define.Item item = Define.Item.Fruit;
    /// <summary> 아이템 겹쳐진 개수</summary>
    public int amount = 1;
    public void GetItem()
    {
        if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<PlayerInventory>().
                      GetItem(item, amount) == 0) //소지품창을 돌면서 인벤토리에 밀어넣기 시도
        {  //아이템을 소지품창에 밀어넣는데 성공했으면
            Destroy(transform.parent.gameObject);//주워 먹었으므로 바닥에 떨어진 아이템 삭제
        }
        else
        {
            Destroy(transform.parent.gameObject);//주워 먹기 실패했지만 바닥에 떨어진 아이템 삭제
        }
    }
}
