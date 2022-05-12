using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 땅에 떨어진 아이템</summary>
public class FieldItem : Interactable
{
    //other한테 내 InteractType을 리턴한다
    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Get; //주울 수 있는 아이템
    }
}
