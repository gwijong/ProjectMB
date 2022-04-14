using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Character
{
    PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }
    void Bark()
    {
        AniOff();
        ani.SetBool("Bark", true);
    }
    protected override void Update()
    {
        if (playerInput.Defense)
        {
            SkillCast(GetComponent<Defense>());
        }
        else if (playerInput.Smash)
        {
            SkillCast(GetComponent<Smash>());
        }
        else if (playerInput.Counter)
        {
            SkillCast(GetComponent<CounterAttack>());
        }
    }
}
