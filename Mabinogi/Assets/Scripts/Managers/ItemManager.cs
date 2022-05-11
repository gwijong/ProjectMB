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
        GameObject dropitem = null; //버릴 아이템
        switch (item)
        {
            case Define.Item.None: //버릴 아이템 타입이 none이면 버릴 아이템이 없으므로 탈출
                return;
            case Define.Item.Fruit:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Fruit")); //열매 아이템 생성
                break;
            case Define.Item.Wool:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Wool")); //양털 아이템 생성
                break;
            case Define.Item.Egg:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Egg")); //달걀 아이템 생성
                break;
            case Define.Item.Firewood:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Firewood")); //장작 아이템 생성
                break;
            case Define.Item.LifePotion:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/LifePotion")); //생명력 포션 아이템 생성
                break;
            case Define.Item.ManaPotion:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ManaPotion")); //마나 포션 아이템 생성
                break;
            case Define.Item.Bottle:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Bottle")); //빈병 아이템 생성
                break;
            case Define.Item.BottleWater:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/BottleWater")); //물병 아이템 생성
                break;
        }
        dropitem.GetComponent<ItemInpo>().amount = mouseAmount; //바닥에 떨어진 아이템의 개수는 마우스로 집고 있던 아이템의 개수이다.
        dropitem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 3, 0);//플레이어 좌표에서 y축 3 높이에서 생성
    }
}
