using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>캐릭터 머리 위에 달린 스킬 말풍선</summary>
public class SkillUI : MonoBehaviour
{
    Character character; //플레이어거나 적이거나 상관없이 모든 캐릭터
    public Image[] skillImages;//준비된 스킬 이미지들
    public Canvas skillCanvas; //스킬 이미지를 보여줄 조그마한 캔버스
    IEnumerator skillCastingCoroutine; //스킬 시전 말풍선 움직여줄 코루틴
    bool coroutineFlag = false;//코루틴 중복실행 방지
    void Start()
    {
        character = GetComponentInParent<Character>(); //부모 오브젝트의 캐릭터 찾아봄
        if (character==null) //캐릭터 못 찾으면
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //플레이어 캐릭터 대입
        }
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }

    
    void OnUpdate()
    {
        if (character.GetreservedSkill() == null) //준비중인 스킬이 없으면
        {
            switch (character.GetloadedSkill().type)
            {
                case Define.SkillState.Combat:  
                    Reset();
                    //컴벳은 말풍선을 활성화하지 않는다

                    break;
                case Define.SkillState.Defense:
                    Reset();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true); //디펜스 이미지 활성화

                    break;
                case Define.SkillState.Smash:
                    Reset();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true); //스매시 이미지 활성화

                    break;
                case Define.SkillState.Counter:
                    Reset();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true); //카운터 이미지 활성화

                    break;
                case Define.SkillState.Windmill:
                    Reset();
                    skillImages[(int)Define.SkillState.Windmill].gameObject.SetActive(true); //윈드밀 이미지 활성화

                    break;
                case Define.SkillState.Icebolt:
                    Reset();
                    skillImages[(int)Define.SkillState.Icebolt].gameObject.SetActive(true); //아이스볼트 이미지 활성화

                    break;
            }
        }
        else //준비중인 스킬이 있으면
        {
            switch (character.GetreservedSkill().type)
            {
                case Define.SkillState.Combat:
                    ImageOff(); //말풍선 이미지 꺼줌

                    break;
                case Define.SkillState.Defense:
                    Casting();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true); //디펜스 이미지 활성화

                    break;
                case Define.SkillState.Smash:
                    Casting();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true); //스매시 이미지 활성화

                    break;
                case Define.SkillState.Counter:
                    Casting();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true); //카운터 이미지 활성화

                    break;
                case Define.SkillState.Windmill:
                    Casting();
                    skillImages[(int)Define.SkillState.Windmill].gameObject.SetActive(true); //윈드밀 이미지 활성화

                    break;
                case Define.SkillState.Icebolt:
                    Casting();
                    skillImages[(int)Define.SkillState.Icebolt].gameObject.SetActive(true); //아이스볼트 이미지 활성화

                    break;
            }
        }
    }
    /// <summary> 스킬 시전 애니메이션 코루틴 실행용 메서드 </summary>
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

    /// <summary> 스킬 시전 애니메이션 코루틴 중지 </summary>
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

    /// <summary> 스킬 아이콘 싹 다 끄기 </summary>
    void ImageOff()
    {
        for (int i = 0; i < skillImages.Length; i++)
        {
            skillImages[i].gameObject.SetActive(false);
        }
    }

    /// <summary> 스킬 말풍선 애니메이션 코루틴 </summary>
    IEnumerator SkillCasting()
    {
        for (int i = 0; i < 10; i++)
        {  //말풍선 줄이는 구간
            skillCanvas.transform.localScale = new Vector3(0.4f - (float)i / 100, 0.4f - (float)i / 100, 0.4f - (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 0; i < 10; i++)
        {  //말풍선 키우는 구간
            skillCanvas.transform.localScale = new Vector3(0.3f + (float)i / 100, 0.3f + (float)i / 100, 0.3f + (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        coroutineFlag = false;
    }

    /// <summary> 준비중이거나 준비된 스킬 취소되면 초기화 </summary>
    public void SkillCancel()
    {
        Reset();
        character.Casting(Define.SkillState.Combat);//기본 공격으로 스킬 초기화
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_cancel, transform.position);//스킬 취소 효과음
    }
}
