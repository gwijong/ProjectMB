using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC 틴 타입 지정 </summary>
public class Tin : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Tin;
    }
}
