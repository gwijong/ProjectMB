using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 맞는게 가능한 오브젝트들</summary>
public class Hitable : Interactable 
{

    public override Define.InteractType Interact(Interactable other)  //때릴 수 있으므로 InteractType.Attack 반환 
    {
        return Define.InteractType.Attack;
    }
    /// <summary> 공격하는 상대방이 호출하는 얻어 맞는 메서드</summary>
    public virtual bool TakeDamage(Character from)//매개변수를 필요시 추가하세요
    {
        return true;
    }
}
