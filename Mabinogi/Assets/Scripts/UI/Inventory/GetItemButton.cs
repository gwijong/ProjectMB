using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    /// <summary> 바닥에 떨어진 아이템 종류</summary>
    public Define.Item Item = Define.Item.Fruit;
    /// <summary> 아이템 겹쳐진 개수</summary>
    public int amount = 1;

    /// <summary> 아이템 획득 버튼 메서드</summary>
    public void GetItem()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<Inventory>().
                    PutItem(Vector2Int.zero + new Vector2Int (x,y) , Item, amount)) //소지품창을 돌면서 인벤토리에 밀어넣기 시도
                {  //아이템을 소지품창에 밀어넣는데 성공했으면
                    Destroy(gameObject.transform.parent.gameObject);//주워 먹었으므로 바닥에 떨어진 아이템 삭제
                    return;//반복문 탈출
                }
            }
        }      
    }
}
