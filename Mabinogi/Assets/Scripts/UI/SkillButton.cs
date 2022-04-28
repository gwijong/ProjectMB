using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 UI 버튼을 누르면 스킬이 캐스팅되도록 하는 스크립트
public class SkillButton : MonoBehaviour
{
    public int skillNumber = 0; //Define의 스킬 enum

    private Character player;//플레이어

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //플레이어 찾아오기
    }
    public void Casting() //스킬 아이콘 누를 때 마다 활성화
    {
        player.Casting((Define.SkillState)skillNumber);  //해당 아이콘의 번호 스킬 시전
    }
}
