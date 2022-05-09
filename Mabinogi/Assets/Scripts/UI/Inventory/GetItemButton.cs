using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    public Define.Item Item = Define.Item.Fruit;
    public int amount = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<Inventory>().
                    PutItem(Vector2Int.zero + new Vector2Int (x,y) , Item, amount))
                {
                    Destroy(gameObject.transform.parent.gameObject);
                    return;
                }
            }
        }      
    }
}
