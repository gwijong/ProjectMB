using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateItem : MonoBehaviour
{
    public Define.Item item;
    public int amount;
    void Start()
    {
        GameObject currentItem = item.MakePrefab();
        currentItem.transform.SetParent(transform);
        currentItem.transform.localPosition = Vector3.zero;
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.transform.SetParent(currentItem.transform);
        currentItem.GetComponent<ItemInpo>().amount = amount;
        GetComponentInChildren<Text>().text = item.GetItemData().ItemName;
    }
}
