using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        skillList = SkillList.dog;
        loadedSkill = skillList[Define.SkillState.Combat].skill;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
