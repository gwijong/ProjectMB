using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 상호작용하는 오브젝트들의 최상위 부모 오브젝트</summary>
public class Interactable : MonoBehaviour
{
    public virtual Define.InteractType Interact() 
    { 
        return Define.InteractType.None; //클릭하거나 AI가 쓰는거
    }
}
