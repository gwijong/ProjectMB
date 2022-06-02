using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tin : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Tin;
    }
}
