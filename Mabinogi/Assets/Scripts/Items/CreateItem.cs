using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 아이템 생성 </summary>
public class CreateItem : MonoBehaviour
{
    /// <summary> 아이템 종류 </summary>
    public Define.Item item;
    /// <summary> 아이템 개수 </summary>
    public int amount;
    void Start()
    {
        GameObject currentItem = item.MakePrefab(); //item에 맞는 프리팹 currentItem 생성
        currentItem.transform.SetParent(transform); //currentItem을  이 오브젝트의 자식으로 설정
        currentItem.transform.localPosition = Vector3.zero; //currentItem의 로컬좌표를 초기화
        Canvas canvas = GetComponentInChildren<Canvas>();  //이 오브젝트의 자식들 중에 캔버스 찾아서 할당
        canvas.transform.SetParent(currentItem.transform);  //캔버스를 들고있는 오브젝트를 currentItem의 자식으로 설정
        currentItem.GetComponent<ItemInpo>().amount = amount; //currentItem의 아이템 개수를 amount로 설정 
        GetComponentInChildren<Text>().text = item.GetItemData().ItemName; //텍스트 컴포넌트의 텍스트를 아이템데이터의 아이템이름으로 설정
    }
}
