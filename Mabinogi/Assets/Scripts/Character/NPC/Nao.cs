using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nao : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Nao;
    }
}
