using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : MonoBehaviour
{
    Character character;
    bool flag = false;
    int skillNum;

    private void Start()
    {
        character = gameObject.GetComponent<Character>();
    }
    void Update()
    {
        if (character.die)
        {
            return;
        }
        if(flag == false)
        {
            flag = true;
            StartCoroutine("DummyAI");
        }
    }

    IEnumerator DummyAI()
    {
        skillNum = Random.Range(0, 4);
        character.Casting((Define.SkillState)skillNum);
        yield return new WaitForSeconds(4.0f);
        if(skillNum!=1 && skillNum != 3)
        {
            character.SetTarget(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>());
        }      
        yield return new WaitForSeconds(2.0f);
        character.SetTarget(null);
        flag = false;
    }
}
