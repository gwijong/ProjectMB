using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(Define.Item.Bottle.ToString(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveItemData(Define.Item item, int amount)
    {
        PlayerPrefs.SetInt(item.ToString(), amount);
    }

    public int LoadItemData(Define.Item item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }
}

/*
         PlayerPrefs.SetFloat("Volume", _value);

        if (PlayerPrefs.HasKey("Volume"))
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
 */