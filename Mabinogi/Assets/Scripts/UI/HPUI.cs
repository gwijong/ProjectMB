using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 캐릭터 위에 달린 HP바 </summary>
public class HPUI : MonoBehaviour
{
    /// <summary> 캐릭터 위에 달린 HP바 이미지 </summary>
    public Image hpGauge;
    /// <summary> 최대 생명력 </summary>
    float maxHP;
    /// <summary> 현재 생명력 </summary>
    float currentHP;
    /// <summary> 부모 오브젝트의 캐릭터 스크립트 컴포넌트 </summary>
    Character character;
    void Start()
    {
        maxHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();//최대 생명력 가져움
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        character = gameObject.GetComponentInParent<Character>();//부모 오브젝트의 캐릭터 컴포넌트 가져오기
    }

    // 계속 갱신
    void OnUpdate()
    {
        currentHP = character.GetCurrentHP();//현재 생명력을 계속 가져옴
        hpGauge.fillAmount = currentHP / maxHP; //생명력 이미지 채워진 비율 갱신
    }
}
