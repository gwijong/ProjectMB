using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 상호작용하는 오브젝트들의 최상위 부모 오브젝트</summary>
public class Interactable : MonoBehaviour
{
    public virtual Define.InteractType Interact(Interactable other) 
    { 
        return Define.InteractType.None; //클릭하거나 AI가 쓰는거
    }

    public static bool IsEnemy(Interactable A, Interactable B)
    {
        //        ^  xor
        //동맹이다  동맹이다   대화   x
        //동맹이다  적이다     전투   o
        //적이다    동맹이다   전투   o
        //적이다    적이다     대화   x

        //둘의 성향이 다른 경우에 적이라고 간주함
        return HasGoodWill(A) ^ HasGoodWill(B);
    }

    //레이어가 enemy가 아니면 좋은 의지를 가지고 있다고 함
    public static bool HasGoodWill(Interactable target)
    {
        return target.gameObject.layer != (int)Define.Layer.Enemy;
    }
}
