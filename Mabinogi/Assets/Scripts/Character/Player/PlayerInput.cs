using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
    //감지된 입력값을 다른 컴포넌트가 사용할 수 있도록 제공
    public string forthBack = "Vertical";  //앞뒤 움직임을 위한 입력축 이름
    public string leftRight = "Horizontal";  // 좌우 움직임을 위한 입력축 이름
    public string defenseButtonName = "Defense";  // 디펜스 스킬을 위한 입력 버튼 이름
    public string smashButtonName = "Smash";  // 스매시 스킬을 위한 입력 버튼 이름
    public string counterButtonName = "CounterAttack"; // 카운터어택 스킬을 위한 입력 버튼 이름
    public float wsMove { get; private set; }  //감지된 앞뒤 움직임 입력값
    public float adMove { get; private set; }  //감지된 좌우 회전 입력값
    public bool defense { get; private set; } //감지된 디펜스 버튼 입력값
    public bool smash { get; private set; }  //감지된 스매시 버튼 입력값
    public bool counter { get; private set; }  //감지된 카운터애택 버튼 입력값

    // 매프레임 사용자 입력을 감지
    void Update()
    {
        //게임오버 상태에서는 사용자 입력을 감지하지 않음
        if (Manager.manager != null && Manager.manager.isGameover)
        {
            wsMove = 0;
            adMove = 0;
            defense = false;
            smash = false;
            counter = false;
            return;
        }
        //앞뒤 입력 감지
        wsMove = Input.GetAxis(forthBack);
        //좌우 입력 감지
        adMove = Input.GetAxis(leftRight);
        //디펜스 입력 감지
        defense = Input.GetButton(defenseButtonName);
        //스매시 입력 감지
        smash = Input.GetButtonDown(smashButtonName);
        //카운터 입력 감지
        counter = Input.GetButtonDown(counterButtonName);
    }
}