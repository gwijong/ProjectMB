using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput_Old : MonoBehaviour
{
    //플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
    //감지된 입력값을 다른 컴포넌트가 사용할 수 있도록 제공
    public string front = "Front";  //앞 움직임을 위한 입력축 이름
    public string back = "Back";  // 뒤 움직임을 위한 입력축 이름
    public string right = "Right";  //오른쪽 움직임을 위한 입력축 이름
    public string left = "Left";  // 왼쪽 움직임을 위한 입력축 이름
    public string defenseButtonName = "Defense";  // 디펜스 스킬을 위한 입력 버튼 이름
    public string smashButtonName = "Smash";  // 스매시 스킬을 위한 입력 버튼 이름
    public string counterButtonName = "CounterAttack"; // 카운터어택 스킬을 위한 입력 버튼 이름
    public bool Front { get; private set; }  //감지된 앞 움직임 입력값
    public bool Back { get; private set; }  //감지된 뒤 움직임 입력값
    public bool Right { get; private set; }  //감지된 오른쪽 움직임 입력값
    public bool Left { get; private set; }  //감지된 왼쪽 움직임 입력값
    public bool Defense { get; private set; } //감지된 디펜스 버튼 입력값
    public bool Smash { get; private set; }  //감지된 스매시 버튼 입력값
    public bool Counter { get; private set; }  //감지된 카운터어택 버튼 입력값

    // 매프레임 사용자 입력을 감지
    void Update()
    {
        //게임오버 상태에서는 사용자 입력을 감지하지 않음
        if (Manager.manager != null && Manager.manager.isGameover)
        {
            Front = false;
            Back = false;
            Right = false;
            Left = false;
            Defense = false;
            Smash = false;
            Counter = false;
            return;
        }
        //앞 입력 감지
        Front = Input.GetButton(front);
        //뒤 입력 감지
        Back = Input.GetButton(back);
        //오른쪽 입력 감지
        Right = Input.GetButton(right);
        //왼쪽 입력 감지
        Left = Input.GetButton(left);
        //디펜스 입력 감지
        Defense = Input.GetButtonDown(defenseButtonName);
        //스매시 입력 감지
        Smash = Input.GetButtonDown(smashButtonName);
        //카운터 입력 감지
        Counter = Input.GetButtonDown(counterButtonName);
    }
}