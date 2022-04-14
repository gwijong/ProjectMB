using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : Interactable
{
    public override InteractType Interact()
    {
        Debug.Log("ItemGether");
        return InteractType.Get;
    }
}
