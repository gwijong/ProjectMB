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
            flag = true;
            StopCoroutine("DummyAI");
            return;
        }
        if (flag == false)
        {
            flag = true;
            StartCoroutine("DummyAI");
        }
        LookAt();
    }

    IEnumerator DummyAI()
    {
        yield return new WaitForSeconds(2.0f);
        skillNum = Random.Range(0, 4);
        character.Casting((Define.SkillState)skillNum);
        yield return new WaitForSeconds(4.0f);
        if (skillNum != 1 && skillNum != 3)
        {
            character.SetTarget(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>());
        }
        else
        {
        }
        yield return new WaitForSeconds(2.0f);
        character.SetTarget(null);
        flag = false;
    }

    void LookAt()
    {
        Vector3 look = GameObject.FindGameObjectWithTag("Player").transform.position;
        look.y = transform.position.y;
        transform.LookAt(look);
    }
}
