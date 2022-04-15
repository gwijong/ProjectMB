using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Old : Character_Old
{
    PlayerInput_Old playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput_Old>();
    }
    void Bark()
    {
        AniOff();
        ani.SetBool("Bark", true);
    }
    protected override void Update()
    {
        base.Update();
        if (playerInput.Defense)
        {
            SkillCast(GetComponent<Defense_Old>());
        }
        else if (playerInput.Smash)
        {
            SkillCast(GetComponent<Smash_Old>());
        }
        else if (playerInput.Counter)
        {
            SkillCast(GetComponent<CounterAttack_Old>());
        }
    }
}
