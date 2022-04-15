using UnityEngine;

public abstract class Skill_Old : MonoBehaviour
{
    public SkillData skillData;
    protected Character_Old character;
    protected Animator ani;
    protected virtual void Start()
    {
        character = GetComponent<Character_Old>();
        ani = GetComponent<Animator>();
    }
    public abstract void SkillUse(Character_Old enemyTarget);

}
