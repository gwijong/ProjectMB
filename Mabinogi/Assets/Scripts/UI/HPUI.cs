using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image hpGauge;
    float maxHP;
    float currentHP;
    Character character;
    void Start()
    {
        maxHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        character = gameObject.GetComponentInParent<Character>();//부모 오브젝트의 캐릭터 컴포넌트 가져오기
    }

    // Update is called once per frame
    void OnUpdate()
    {
        currentHP = character.GetCurrentHP();//생명력 가져옴
        hpGauge.fillAmount = currentHP / maxHP; //생명력 이미지 채워진 비율 갱신
    }
}
