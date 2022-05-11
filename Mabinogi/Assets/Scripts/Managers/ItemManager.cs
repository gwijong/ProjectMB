using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /// <summary> 아이템 데이터 스크립터블 오브젝트 </summary>
    public ItemData[] data;

    /// <summary> 아이템 버리기 </summary>
    public void DropItem(Define.Item item, int mouseAmount)
    {
        GameObject dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ItemPrefab"));//버릴 아이템

        if (dropitem == null) return;

        dropitem.GetComponent<CreateItem>().amount = mouseAmount; //바닥에 떨어진 아이템의 개수는 마우스로 집고 있던 아이템의 개수이다.
        dropitem.GetComponent<CreateItem>().item = item;
        dropitem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 3, 0);//플레이어 좌표에서 y축 3 높이에서 생성
    }
}
