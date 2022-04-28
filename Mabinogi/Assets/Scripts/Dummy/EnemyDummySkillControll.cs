using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummySkillControll : MonoBehaviour
{
    Character character;//적 캐릭터

    void Start()
    {
        character = GetComponent<Character>();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        SkillInput();
    }


    /// <summary> 7컴벳  8디펜스  9스매시  0카운터 </summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            character.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            character.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            character.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            character.Casting(Define.SkillState.Combat);
        }


    }
}
