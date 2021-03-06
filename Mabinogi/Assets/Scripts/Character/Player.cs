using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 플레이어 캐릭터 </summary>
public class Player : Character
{
    public GameObject dieCanvas;
    public GameObject diePanel;
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //플레이어 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
        anim = GetComponentInChildren<Animator>();
        GameObject Effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SpawnSimpleBlue"));
        Effect.transform.position = gameObject.transform.position + Vector3.up;
        Destroy(Effect, 2f);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> 양털 채집 </summary>
    public void Sheeping()
    {
        PlayAnim("Sheeping");
        StartCoroutine(Wait(5f));
        StartCoroutine(DropItem(Define.Item.Wool));
    }

    /// <summary> 달걀 채집 </summary>
    public void Egg()
    {
        PlayAnim("Egg");
        StartCoroutine(Wait(5f));
        StartCoroutine(DropItem(Define.Item.Egg));
    }

    /// <summary> 3초뒤 아이템 생성 </summary>
    IEnumerator DropItem(Define.Item item) 
    {
        yield return new WaitForSeconds(2.8f);
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.emotion_success, transform.position);//성공 효과음
        GameManager.itemManager.DropItem(item, 1);
    }

    /// <summary> NPC와 대화 시작 </summary>
    public void Talk(NPC target)
    {
        if(target == this)
        {
            return;
        }

        DialogTalk dialog = FindObjectOfType<DialogTalk>();//대화 캔버스 가져옴
        dialog.SetTarget(this, target);//대화 대상 세팅
    }

    /// <summary> 플레이어 사망 시 처리 </summary>
    public override void PlayerDie()
    {
        GameManager.soundManager.PlayBgmPlayer(Define.Scene.Die); //사망 배경음악 재생
        dieCanvas.SetActive(true);
        StartCoroutine(Die());//사망 캔버스 활성화
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        diePanel.SetActive(true);
    }
}
