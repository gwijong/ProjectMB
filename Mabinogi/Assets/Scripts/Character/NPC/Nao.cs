using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nao : Character
{
    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Talk; //대화 리턴
    }
}
