using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase : MonoBehaviour
{
    public abstract void OnUpdate();//매니저의 Update 메서드용 Action 델리게이트에 사용됨 
    public abstract void Patrol(); //주위를 순찰한다
    public abstract void NormalAttack(GameObject target); //적을 기본 공격한다
    public abstract void Prowl(GameObject target); //적 주위를 맴돈다

    public abstract void Defence();

    /*
    [조혁진] [오후 3:26] 변수부터 짜라고 했지
[조혁진] [오후 3:26] 스킬이라는 클래스를 만들어
[조혁진] [오후 3:27] 스킬에는 준비 시간이라던지
[조혁진] [오후 3:27] 스킬 ID라던지
[조혁진] [오후 3:27] 그런 것들이 있을 거 아냐
    [조혁진] [오후 3:28] 아니
[조혁진] [오후 3:28] 큰 것부터 묶으라고
[조혁진] [오후 3:28] 스킬
[조혁진] [오후 3:28] 아이템
[조혁진] [오후 3:28] 캐릭터
[조혁진] [오후 3:28] 있을 거 아냐
[조혁진] [오후 3:28] 눈에 보이는 큼직큼직한 것들
[조혁진] [오후 3:28] 그것부터 좀 묶어
[조혁진] [오후 3:29] 스킬이라고 하는 큰 클래스가 있으면
[조혁진] [오후 3:29] 서로의 스킬 아이디같은 걸 비교해서
[조혁진] [오후 3:29] 그냥 스킬마다
[조혁진] [오후 3:29] 오버라이딩해서
[조혁진] [오후 3:29] 상대가 이런 아이디면
[조혁진] [오후 3:29] 내가 이기고 들어감
[조혁진] [오후 3:29] 이런 한 줄만 짜도
[조혁진] [오후 3:29] 작동할 거 아냐


    */
}
