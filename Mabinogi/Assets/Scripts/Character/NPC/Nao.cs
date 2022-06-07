using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC 나오 타입 지정 </summary>
public class Nao : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Nao;
    }
}
