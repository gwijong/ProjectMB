using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nao : NPC
{

    public override void PersonalTalk(string wantText) //npc들마다 개인적인 이야기 근처의 소문 따로 가지고 있는거 wantText를 스위치 돌려서 본인만의 다이얼로그 켜게 함
    {
        switch (wantText)
        {
            case "":
            break;
        }
    }


}
