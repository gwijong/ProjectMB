using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : Interactable
{
    //공격이 실패한 경우에는 공격 대상자가 리턴값을 받아서 경직에 스스로 걸리게 해야해
    
    public virtual bool TakeDamage(Pawn from)//매개변수를 필요시 추가하세요
    {
        return true;
    }
}
