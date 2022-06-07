using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC 타르라크 타입 지정</summary>
public class Tarlach : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Tarlach;
    }
}
