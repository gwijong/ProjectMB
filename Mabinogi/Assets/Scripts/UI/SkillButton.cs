using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 화면 좌측 상단 스킬 UI 버튼을 누르면 스킬이 캐스팅되도록 하는 스크립트 </summary>
public class SkillButton : MonoBehaviour
{
    public int skillNumber = 0; //Define의 스킬 enum

    private Character player;//플레이어

    public void Casting() //스킬 아이콘 누를 때 마다 활성화
    {
        player = GameManager.manager.GetComponent<PlayerController>().player.GetComponent<Character>(); //조작중인 캐릭터 찾아오기
        player.Casting((Define.SkillState)skillNumber);  //해당 아이콘의 번호 스킬 시전
    }
}
