using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
    protected virtual void Start()
    {
        character = GetComponent<Character>();
        ani = GetComponent<Animator>();
    }
    public abstract void SkillUse(Character enemyTarget);

}
