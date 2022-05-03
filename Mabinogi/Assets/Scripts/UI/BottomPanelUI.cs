using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomPanelUI : MonoBehaviour
{
    public Image hpGauge; //생명력 게이지
    public Image mpGauge; //마나 게이지
    public Image spGauge; //스태미나 게이지

    public Text hpText; //화면 하단 UI에 출력되는 생명력 텍스트
    public Text mpText; //화면 하단 UI에 출력되는 마나 텍스트
    public Text spText; //화면 하단 UI에 출력되는 스태미나 텍스트

    Character character; //플레이어 캐릭터 단 하나

    float currentHP; //현재 생명력
    float maxHP; //최대 생명력
    float currentMP; //현재 마나
    float maxMP; //최대 마나
    float currentSP; //현재 스태미나
    float maxSP; // 최대 스태미나

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //플레이어 캐릭터 찾아옴
        setPoint();//맥스값 세팅
        setCurrentPoint(); //현재값하고 게이지 비율 세팅
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick) && Input.GetKey(KeyCode.LeftControl))
        {
            StartCoroutine("OneTickWait");//한 프레임 쉬고 세팅
        }

        setCurrentPoint();
        hpText.text = (int)currentHP + "/" + maxHP; //생명력 텍스트 갱신
        mpText.text = (int)currentMP + "/" + maxMP; //마나 텍스트 갱신
        spText.text = (int)currentSP + "/" + maxSP; //스태미나 텍스트 갱신
    }

    /// <summary> 처음 시작하거나 캐릭터가 바뀐경우 맥스값 새로 세팅 </summary>
    void setPoint()
    {
        maxHP = character.hitPoint.Max;
        maxMP = character.manaPoint.Max;
        maxSP = character.staminaPoint.Max;
    }

    /// <summary> 현재 게이지 상태 업데이트 </summary>
    void setCurrentPoint()
    {
        currentHP = character.hitPoint.Current;
        currentMP = character.manaPoint.Current;
        currentSP = character.staminaPoint.Current;
        hpGauge.fillAmount = currentHP / maxHP;
        mpGauge.fillAmount = currentMP / maxMP;
        spGauge.fillAmount = currentSP / maxSP;
    }

    /// <summary> 한 프레임 쉬고 나서 플레이어 찾아 게이지 정보 갱신 </summary>
    IEnumerator OneTickWait()
    {
        yield return null;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        setPoint();
        setCurrentPoint();
    }
}
