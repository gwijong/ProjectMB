using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC 고로 타입 지정 </summary>
public class Goro : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Goro;
    }
}
