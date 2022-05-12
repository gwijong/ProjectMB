using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    /// <summary> 인공지능 몬스터 나 자신 </summary>
    protected Character character;

    protected virtual void Start()
    {
        character = GetComponent<Character>();
    }

    /// <summary> 범위 안의 캐릭터 가져오기</summary>
    public List<Character> GetCharactersInRange(float range)
    {
        List<Character> result = new List<Character>();//범위 안의 캐릭터 리스트
        //반지름 range짜리 동그란 콜라이더 만들어서 충돌하는 콜라이더들 colliders 배열에 다 넣는다.
        Collider[] colliders = Physics.OverlapSphere(transform.position, range); 
        foreach (Collider current in colliders) //colliders배열 크기만큼 반복하면서 모든 콜라이더들 검사한다.
        {
            Character currentCharacter = current.GetComponent<Character>(); //현재 콜라이더의 Character스크립트 컴포넌트 할당 시도
            if (currentCharacter != null) result.Add(currentCharacter);//현재 캐릭터에 Character컴포넌트가 있으면 result리스트에 추가
        }
        return result; //캐릭터 리스트 반환
    }

    /// <summary> 범위 안의 적 리스트 리턴</summary>
    public List<Character> GetEnemyInRange(float range)
    {
        List<Character> result = new List<Character>(); //이 리스트엔 적(Enemy) 캐릭터만 추가됨
        List<Character> from = GetCharactersInRange(range);//범위 안의 모든 캐릭터

        foreach(Character current in from)//범위 안의 모든 캐릭터 숫자만큼 반복
        {
            if(Interactable.IsEnemy(character, current)) //내 캐릭터 자신과 상대방의 상호작용이 적이면(IsEnemy가 true면)
            {
                result.Add(current);//적 리스트에 추가
            };
        };
        return result;
    }
}
