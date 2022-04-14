using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Character
{
    void Bark()
    {
        AniOff();
        ani.SetBool("Bark", true);
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Hit(5, 2, 2, 2, 2, 30);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Groggy(0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Die();
        }
    }
}
