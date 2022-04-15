using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 맞는게 가능한 오브젝트들</summary>
public class Hitable : Interactable 
{

    
    public virtual bool TakeDamage(Character from)//매개변수를 필요시 추가하세요
    {
        return true;
    }
}
