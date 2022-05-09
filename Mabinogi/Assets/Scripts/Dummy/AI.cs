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
        List<Character> result = new List<Character>();
        //반지름 range짜리 동그란 콜라이더 만든다
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider current in colliders)
        {
            Character currentCharacter = current.GetComponent<Character>();
            if (currentCharacter != null) result.Add(currentCharacter);
        }
        return result;
    }

    /// <summary> 범위 안의 적 리스트 리턴</summary>
    public List<Character> GetEnemyInRange(float range)
    {
        List<Character> result = new List<Character>(); 
        List<Character> from = GetCharactersInRange(range);

        foreach(Character current in from)
        {
            if(Interactable.IsEnemy(character, current))
            {
                result.Add(current);
            };
        };

        return result;
    }
}
