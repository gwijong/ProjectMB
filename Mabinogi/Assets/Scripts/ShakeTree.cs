using UnityEngine.AI; // 내비메시 관련 코드
using UnityEngine;
using System.Collections;
public class ShakeTree : Hitable
{
    public Define.Item[] items; //생성할 아이템들
    
    public override bool TakeDamage(Character from)
    {
        StopCoroutine("Shake");//기존 나무 흔들기 코루틴 중지
        StartCoroutine("Shake");//나무 흔들기 코루틴 실행

        //아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
        GameManager.itemManager.DropItem(items[Random.Range(0, items.Length)], 1);

        return true;      
    }


    IEnumerator Shake()//나무 흔들기
    {
        for(int i = 0; i<10; i++)
        {
            if(i%2 == 0)
            {
                transform.position = transform.position + new Vector3(0.05f, 0, 0.05f);
            }
            else
            {
                transform.position = transform.position + new Vector3(-0.05f, 0,-0.05f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}


