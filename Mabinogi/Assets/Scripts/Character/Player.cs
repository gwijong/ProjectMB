using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    GameObject Dialog;

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //플레이어 스킬 리스트 사용
        loadedSkill = skillList[Define.SkillState.Combat].skill; //스킬 기본값인 컴벳으로 준비된 스킬 설정
        
    }

    private void Start()
    {
        Dialog = GameObject.FindGameObjectWithTag("Dialog");
        Dialog.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.emotion_success);//성공 효과음
        GameManager.itemManager.DropItem(item, 1);
    }

    public void Talk(Interactable target)
    {
        if(target == (Interactable)this)
        {
            return;
        }
        Dialog.SetActive(true);
        DialogTalk dialog = Dialog.GetComponent<DialogTalk>();
        CharacterTalkScripts scripts = target.gameObject.GetComponent<CharacterTalkScripts>();
        dialog.SetText(scripts.firstScript, scripts.chooseSciript, scripts.noteScript, scripts.shopScript);
        dialog.name.text = target.GetComponent<Character>().characterName;
    }
}
