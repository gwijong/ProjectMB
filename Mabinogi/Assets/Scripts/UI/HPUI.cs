using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image hpGauge;
    float maxHP;
    float currentHP;
    void Start()
    {
        maxHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();        
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();
        hpGauge.fillAmount = currentHP / maxHP;
    }
}
