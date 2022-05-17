using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogTalk : MonoBehaviour
{
    public Character currentPlayer;
    public NPC currentNPC;
    Dialog currentDialog;

    /// <summary> NPC 이름 텍스트 </summary>
    public Text textName;
    /// <summary> 대화지문 텍스트 </summary>
    public Text textScript;
    /// <summary> 초상인물사진 </summary>
    public Image portrait;
    /// <summary> 대화창 뒤의 암막 </summary>
    public GameObject dark;
    /// <summary> 대화창 배경그림의 외곽선 </summary>
    public GameObject outline;
    /// <summary> 선택 버튼들 4개 </summary>
    public GameObject[] buttonBackgrounds;
    /// <summary> 여행수첩 </summary>
    public GameObject note;
    /// <summary> 캐릭터 스킬, 상태 UI </summary>
    public GameObject UI_Canvas;
    /// <summary> 현재 실행중인 코루틴 </summary>
    IEnumerator Cor;

    private void Update()
    {
        if (Input.anyKeyDown && outline.activeInHierarchy)
        {
            DialogNext();
        }
    }

    public void SetDialog(Dialog wantDialog)
    {
        currentDialog = wantDialog;
        portrait.sprite = currentDialog.portrait;
        textName.text = currentDialog.npcName;
        CoroutineStart(TextOutput(currentDialog));
    }

    public void DialogNext()
    {
        if(currentDialog.next == null)
        {
            return;
        }
        else
        {
            SetDialog(currentDialog.next);
        }
    }
    /// <summary> 텍스트 한글자씩 출력 코루틴 </summary>
    IEnumerator TextOutput(Dialog wantDialog)
    {
        SelectButtonOff();
        textScript.text = "";
        portrait.sprite = wantDialog.portrait;
        portrait.gameObject.SetActive(wantDialog.portraitActive);
        int i = 0;
        while (textScript.text.Length< wantDialog.currentText.Length)
        {
            textScript.text += wantDialog.currentText[i++];
            yield return new WaitForSeconds(0.05f);          
        }
        if(wantDialog.buttonArray.Length <= 0)
        {
            SetButton(0, "다음", Define.TalkButtonType.Next);
        }
        else
        {
            for(int j = 0; j< wantDialog.buttonArray.Length; j++)
            {
                SetButton(j, wantDialog.buttonArray[j].buttonName, wantDialog.buttonArray[j].type);
            }
        }
    }
    
    public void SetButton(int current, string wantText, Define.TalkButtonType wantType)
    {
        buttonBackgrounds[current].SetActive(true);
        buttonBackgrounds[current].GetComponentInChildren<Text>().text = wantText;
        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction targetFuntion;
        switch (wantType)
        {
            default:
                targetFuntion = EndTalkButton;
                break;
            case Define.TalkButtonType.Next:
                targetFuntion = DialogNext;
                break;
            case Define.TalkButtonType.Note:
                targetFuntion = NoteButton;
                break;
            case Define.TalkButtonType.Personal:
                targetFuntion = PersonalStoryButton;
                break;
            case Define.TalkButtonType.Shop:
                targetFuntion = ShopButton;
                break;
            case Define.TalkButtonType.EndTalk:
                targetFuntion = EndTalkButton;
                break;
            case Define.TalkButtonType.ToMain:
                targetFuntion = MainButton;
                break;
        }

        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.AddListener(targetFuntion);
    }
    
    public void SetTarget(Character wantPlayer, NPC wantNPC)
    {
        currentPlayer = wantPlayer;
        currentNPC = wantNPC;
        OpenTalkCanvas();
        SetDialog(currentNPC.AppearanceDialog);
    }

    /// <summary> 대화 끝내기 버튼 </summary>
    public void EndTalkButton()
    {
        SelectButtonOff();
        CloseTalkCanvas();
        UI_Canvas.SetActive(true);

    }

    /// <summary> 대화 캔버스의 구성요소들을 전부 끔 </summary>
    public void CloseTalkCanvas()
    {
        portrait.gameObject.SetActive(false);
        note.SetActive(false);
        dark.SetActive(false);
        outline.SetActive(false);
    }
    public void OpenTalkCanvas()
    {
        dark.SetActive(true);
        outline.SetActive(true);
        UI_Canvas.SetActive(false);
    }
    /// <summary> 상점 대화지문으로 진입 버튼 </summary>
    public void ShopButton()
    {
        SetDialog(currentNPC.ShopDialog);
    }

    public void MainButton()
    {
        SetDialog(currentNPC.MainDialog);
    }

    /// <summary> 여행수첩 대화지문으로 진입 </summary>
    public void NoteButton()
    {
        note.SetActive(true);
        SetDialog(currentNPC.NoteDialog);
    }

    /// <summary> 여행수첩의 개인적인 이야기 실행 </summary>
    public void PersonalStoryButton()
    {
        SetDialog(currentNPC.PersonalStoryDialog);
    }

    /// <summary> 여행수첩의 근처의 소문 실행 </summary>
    public void RumorsNearbyButton()
    {
        SetDialog(currentNPC.RumorsNearbyDialog);
    }


    /// <summary> 여행수첩의 스킬에 대하여 실행 </summary>
    public void SkillButton()
    {
        SetDialog(currentNPC.SkillDialog);
    }


    /// <summary> 여행수첩의 아르바이트에 대하여 실행 </summary>
    public void PartTimeJobButton()
    {
        SetDialog(currentNPC.PartTimeJobDialog);
    }


    /// <summary> 코루틴 실행 메서드 </summary>
    void CoroutineStart(IEnumerator cor)
    {
        if (Cor != null)
        {
            StopCoroutine(Cor);
        }
        Cor = cor;
        StartCoroutine(Cor);
    }


    /// <summary> 선택지문용 버튼 전부 끔 </summary>
    void SelectButtonOff()
    {
        for(int i =0; i< buttonBackgrounds.Length; i++)
        {
            buttonBackgrounds[i].SetActive(false);
        }
    }
}

