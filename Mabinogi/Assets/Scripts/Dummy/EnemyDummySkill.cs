using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummySkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkillInput();
    }

    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("적: 디펜스 시전");
            this.GetComponent<Character>().Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("적: 스매시 시전");
            this.GetComponent<Character>().Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("적: 카운터 시전");
            this.GetComponent<Character>().Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("적: 스킬 취소, 컴벳으로 전환");
            this.GetComponent<Character>().Casting(Define.SkillState.Combat);
        }


    }
}
