using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ¶¥¿¡ ¶³¾îÁø ¾ÆÀÌÅÛ</summary>
public class FieldItem : Interactable
{
    public override Define.InteractType Interact()
    {
        Debug.Log("ItemGether");
        return Define.InteractType.Get;
    }
}
