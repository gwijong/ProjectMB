using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    Character character;
    public Image[] skillImages;
    public Canvas skillCanvas;
    IEnumerator skillCastingCoroutine;
    bool coroutineFlag = false;
    void Start()
    {
        character = GetComponent<Character>();
    }

    
    void Update()
    {
        skillCanvas.transform.LookAt(Camera.main.transform);
        if (character.GetreservedSkill() == null)
        {
            switch (character.GetloadedSkill().type)
            {
                case Define.SkillState.Combat:
                    Reset();
                    //skillImages[(int)Define.SkillState.Combat].gameObject.SetActive(true);

                    break;
                case Define.SkillState.Defense:
                    Reset();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true);
                    
                    break;
                case Define.SkillState.Smash:
                    Reset();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true);
                    
                    break;
                case Define.SkillState.Counter:
                    Reset();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true);
                    
                    break;
            }
        }
        else
        {
            switch (character.GetreservedSkill().type)
            {
                case Define.SkillState.Combat:
                    
                    //skillImages[(int)Define.SkillState.Combat].gameObject.SetActive(true);
                    
                    break;
                case Define.SkillState.Defense:
                    Casting();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true);

                    break;
                case Define.SkillState.Smash:
                    Casting();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true);

                    break;
                case Define.SkillState.Counter:
                    Casting();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true);

                    break;
            }
        }
    }
    void Casting()
    {
        ImageOff();
        if (coroutineFlag == false)
        {
            coroutineFlag = true;
            skillCastingCoroutine = SkillCasting();
            StartCoroutine(skillCastingCoroutine);
        }
    }
    private void Reset()
    {
        coroutineFlag = false;
        ImageOff();
        if (skillCastingCoroutine != null)
        {
            StopCoroutine(skillCastingCoroutine);
        }
        skillCanvas.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    void ImageOff()
    {
        for (int i = 0; i < skillImages.Length; i++)
        {
            skillImages[i].gameObject.SetActive(false);
        }
    }

    IEnumerator SkillCasting()
    {
        for (int i = 0; i < 10; i++)
        {
            skillCanvas.transform.localScale = new Vector3(0.4f-(float)i/100, 0.4f - (float)i / 100, 0.4f - (float)i / 100);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < 10; i++)
        {
            skillCanvas.transform.localScale = new Vector3(0.3f + (float)i / 100, 0.3f + (float)i / 100, 0.3f + (float)i / 100);
            yield return new WaitForSeconds(0.02f);
        }       
        coroutineFlag = false;
    }
}
