using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inven_Cell : MonoBehaviour
{
    public GameObject cell;
    public GameObject parent;
    int ypos = 0;
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                GameObject currentCell = Instantiate(cell) ;
                currentCell.transform.SetParent(parent.transform);
                Vector3 pos = new Vector3(currentCell.transform.position.x + (48 * j), currentCell.transform.position.y + ypos , currentCell.transform.position.z);
                currentCell.GetComponent<RectTransform>().position = pos;
            }
            ypos = ypos + 48;
        }
    }
}
