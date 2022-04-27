using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public int skillNumber = 0;

    public void Casting()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().Casting((Define.SkillState)skillNumber);
    }
}
