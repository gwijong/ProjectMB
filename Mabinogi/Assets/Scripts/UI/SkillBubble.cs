using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SkillUICanvas 에 달림
/// <summary> 캐릭터 머리 위에 달리는 스킬 말풍선 </summary>
public class SkillBubble : MonoBehaviour
{
    /// <summary> 플레이어거나 적이거나 상관없이 모든 캐릭터</summary>
    Character character; 
    /// <summary> 준비된 스킬 이미지 스프라이트들</summary>
    public Sprite[] skillSprites;
    /// <summary> 스킬 스프라이트가 담길 이미지 컴포넌트</summary>
    Image skillImage; 
    /// <summary> 스킬 스프라이트 뒤의 흰색 말풍선</summary>
    Image backGroundImage; 
    /// <summary> 스킬 시전 말풍선 움직여줄 코루틴</summary>
    IEnumerator skillCastingCoroutine; 
    /// <summary> 코루틴 중복실행 방지</summary>
    bool coroutineFlag = false;
    /// <summary> 카메라와 거리</summary>
    float distance;
    void Start()
    {
        backGroundImage = GetComponentsInChildren<Image>()[0];
        skillImage = GetComponentsInChildren<Image>()[1]; //캔버스의 자식 오브젝트의 이미지 컴포넌트 찾아옴
        character = GetComponentInParent<Character>(); //부모 오브젝트의 캐릭터 찾아봄
        if (character == null) //캐릭터 못 찾으면
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //플레이어 캐릭터 대입
        }
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        backGroundImage.enabled = false; //스킬 말풍선 이미지 비활성화
        skillImage.enabled = false; //스킬 말풍선 이미지 비활성화
    }


    public void OnUpdate()
    {
        distance = (gameObject.transform.position - Camera.main.transform.position).magnitude;
        if (distance / 20 < 1)
        {
            distance = 20;
        }

        if (character.GetloadedSkill().type == Define.SkillState.Combat) //스킬이 기본공격이면
        {
            backGroundImage.enabled = false; //스킬 말풍선 이미지 비활성화
            skillImage.enabled = false; //스킬 말풍선 이미지 비활성화
        }

        if (character.die == true) //캐릭터가 죽으면 스킬 말풍선 비활성화
        {
            Reset();
            skillImage.sprite = skillSprites[(int)character.GetloadedSkill().type]; //스킬 스프라이트 설정
            backGroundImage.enabled = false; //스킬 말풍선 이미지 비활성화
            skillImage.enabled = false; //스킬 말풍선 이미지 비활성화
        }

        if (character.GetreservedSkill() == null) //준비중인 스킬이 없으면
        {
            Reset();
            skillImage.sprite = skillSprites[(int)character.GetloadedSkill().type]; //스킬 스프라이트 설정
            
        }
        else //준비중인 스킬이 있으면
        {
            Casting();
            skillImage.sprite = skillSprites[(int)character.GetreservedSkill().type]; //스킬 스프라이트 설정
            if(character.GetreservedSkill().type != Define.SkillState.Combat)//스킬이 기본공격이 아니면
            {
                backGroundImage.enabled = true; //스킬 말풍선 이미지 활성화
                skillImage.enabled = true; //스킬 말풍선 이미지 활성화
            }
        }
    }

    /// <summary> 스킬 시전 애니메이션 코루틴 중지 </summary>
    private void Reset()
    {
        coroutineFlag = false;
        if (skillCastingCoroutine != null)
        {
            StopCoroutine(skillCastingCoroutine);
        }
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * distance / 20; //스킬 말풍선 크기를 최초값으로 변경
    }


    /// <summary> 스킬 시전 애니메이션 코루틴 실행용 메서드 </summary>
    void Casting()
    {
        if (character.magicBolt != null) //시전한 마법 게임오브젝트가 존재하면
        {
            Destroy(character.magicBolt); //마법 삭제
        }
        if (coroutineFlag == false)
        {
            coroutineFlag = true;
            skillCastingCoroutine = SkillCasting();
            StartCoroutine(skillCastingCoroutine);  //스킬 말풍선 애니메이션 코루틴 실행
        }
    }


    /// <summary> 스킬 말풍선 애니메이션 코루틴 </summary>
    IEnumerator SkillCasting()
    {
        for (int i = 0; i < 10; i++)
        {  //말풍선 줄이는 구간
            transform.localScale = new Vector3(0.5f - (float)i / 100, 0.5f - (float)i / 100, 0.5f - (float)i / 100) * distance/20;
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 0; i < 10; i++)
        {  //말풍선 키우는 구간
            transform.localScale = new Vector3(0.4f + (float)i / 100, 0.4f + (float)i / 100, 0.4f + (float)i / 100) * distance / 20;
            yield return new WaitForSeconds(0.04f);
        }
        coroutineFlag = false;
    }

    /// <summary> 준비중이거나 준비된 스킬 취소되면 초기화 </summary>
    public void SkillCancel()
    {
        Reset();
        if (character.magicBolt != null) //시전한 마법 게임오브젝트가 있으면
        {
            Destroy(character.magicBolt); //마법 게임오브젝트 파괴
        }
        backGroundImage.enabled = false; //스킬 말풍선 이미지 비활성화
        skillImage.enabled = false; //스킬 말풍선 이미지 비활성화
        character.Casting(Define.SkillState.Combat); //스킬을 기본공격으로 세팅
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_cancel, transform.position);//스킬 취소 효과음
        character.PlayAnim("Reset"); //애니메이션을 아이들 상태로 전환
    }

    /// <summary> 사망처리 </summary>
    public void Die()
    {
        GameManager.update.UpdateMethod -= OnUpdate;
    }
}
