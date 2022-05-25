using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goro : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Goro;
    }
}
