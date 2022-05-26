using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

/// <summary> NPC 대화 캔버스에 달림 </summary>
public class DialogTalk : MonoBehaviour
{
    /// <summary> 대화중인 플레이어 </summary>
    public Character currentPlayer;
    /// <summary> 대화중인 NPC </summary>
    public NPC currentNPC;
    /// <summary> 대화 한 장면 데이터 컨테이너 </summary>
    Dialog currentDialog;

    /// <summary> NPC 이름 텍스트 </summary>
    public Text textName;
    /// <summary> 대화지문 UI 텍스트 </summary>
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
    /// <summary> 상점 인벤토리 </summary>
    InvenOpen inven;

    public string nextScene;

    private void Start()
    {
        inven = GameObject.FindGameObjectWithTag("Inventory").gameObject.GetComponent<InvenOpen>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && outline.activeInHierarchy) //대화창이 활성화 된 중에 아무 키 입력이 있으면
        {
            if (currentDialog == null)
            {
                return;
            }
            if(textScript.text.Length < currentDialog.currentText.Length)
            {
                textScript.text = currentDialog.currentText;
            }
            else
            {
                DialogNext();// 다음 텍스트로 넘김
            }
        }
    }

    /// <summary> 다이어로그 세팅(스프라이트,대사)이후 대사 출력 코루틴 실행 </summary>
    public void SetDialog(Dialog wantDialog)
    {
        currentDialog = wantDialog;
        portrait.sprite = currentDialog.portrait;
        textName.text = currentDialog.npcName;
        CoroutineStart(TextOutput(currentDialog));
    }

    /// <summary> 현재 다이어로그에서 다음 다이어로그로 넘김 </summary>
    public void DialogNext()
    {
        if(currentDialog.next == null) //다음 다이어로그가 비어있으면 리턴
        {
            return;
        }
        else
        {
            SetDialog(currentDialog.next); //다음 다이어로그로 세팅
        }
    }
    /// <summary> 텍스트 한글자씩 출력 코루틴 </summary>
    IEnumerator TextOutput(Dialog wantDialog)
    {
        SelectButtonOff();//일단 버튼 싹 다 끔
        textScript.text = ""; //대화 텍스트 비워줌
        portrait.sprite = wantDialog.portrait; //NPC 초상인물사진 대입
        portrait.gameObject.SetActive(wantDialog.portraitActive); //초상인물사진 활성화 여부
        int i = 0;
        wantDialog.currentText = wantDialog.currentText.Replace("@", "" + System.Environment.NewLine);//골뱅이 만나면 줄바꿈
        while (textScript.text.Length< wantDialog.currentText.Length) //텍스트가 덜 출력되었으면
        {
            textScript.text += wantDialog.currentText[i++]; //텍스트 출력
            yield return new WaitForSeconds(0.05f);          
        }
        if(wantDialog.buttonArray.Length <= 0) //버튼을 따로 만들지 않았으면
        {
            SetButton(0, "다음", Define.TalkButtonType.Next); //다음 대화로 넘기는 기본 버튼 세팅
        }
        else//따로 준비한 버튼이 있으면
        {
            for(int j = 0; j< wantDialog.buttonArray.Length; j++)//버튼의 숫자만큼 반복
            {
                //j번째 버튼의 버튼 이름, 버튼 타입 설정
                SetButton(j, wantDialog.buttonArray[j].buttonName, wantDialog.buttonArray[j].type);
            }
        }
    }

    /// <summary> 해당 버튼 텍스트와 누를 시 실행되는 이벤트 세팅 </summary>
    public void SetButton(int current, string wantText, Define.TalkButtonType wantType)
    {
        buttonBackgrounds[current].SetActive(true); //current번째 버튼 활성화
        buttonBackgrounds[current].GetComponentInChildren<Text>().text = wantText; //버튼 안의 텍스트 지정
        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.RemoveAllListeners(); //버튼의 기존 이벤트들 지움
        UnityEngine.Events.UnityAction targetFuntion;//현재 버튼에 새로 넣을 메서드를 담는 액션
        switch (wantType)
        {
            default:
                targetFuntion = EndTalkButton; //대화 끝내기
                break;
            case Define.TalkButtonType.Next:
                targetFuntion = DialogNext;  //다음 대화로 넘기기
                break;
            case Define.TalkButtonType.Note:
                targetFuntion = NoteButton;  //여행수첩 대화
                break;
            case Define.TalkButtonType.Shop:
                targetFuntion = ShopButton;  //상점 대화
                break;
            case Define.TalkButtonType.EndTalk:
                targetFuntion = EndTalkButton;  //대화창 닫기
                break;
            case Define.TalkButtonType.ToMain: // 메인 대화로
                targetFuntion = MainButton;  //대화, 거래 등이 다 있는 처음 선택지로
                break;
            case Define.TalkButtonType.Farewell: // 메인 대화로
                targetFuntion = FarewellButton;  //대화, 거래 등이 다 있는 처음 선택지로
                break;
        }

        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.AddListener(targetFuntion); //버튼에 targetFuntion 이벤트 추가
    }

    /// <summary> 플레이어 스크립트에서 실행하는, 대화를 할 플레이어와 NPC를 설정하는 메서드 </summary>
    public void SetTarget(Character wantPlayer, NPC wantNPC)
    {
        currentPlayer = wantPlayer; //플레이어 대입
        currentNPC = wantNPC; // NPC 대입
        OpenTalkCanvas();//대화창 열기
        SetDialog(currentNPC.AppearanceDialog);//NPC 최초 대화 시작
        GameManager.soundManager.PlayBgmPlayer(currentNPC.npc);//NPC 배경음악;
        GameManager.soundManager.effectPlayer.volume = 0.15f;
    }

    /// <summary> 대화 끝내기 버튼 </summary>
    public void EndTalkButton()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        GameManager.soundManager.effectPlayer.volume = 0.5f;
        SelectButtonOff(); //모든 버튼 꺼줌
        CloseTalkCanvas(); //대화 캔버스 꺼줌
        UI_Canvas.SetActive(true); //전투 UI 켜줌
        inven.StoreClose();
        inven.Close();
        if (FindObjectOfType<Soulstream>() != null)
        {
            StartCoroutine(FindObjectOfType<Soulstream>().NaoDisappear());
        }      
    }

    /// <summary> 대화 캔버스의 구성요소들을 전부 끔 </summary>
    public void CloseTalkCanvas()
    {
        portrait.gameObject.SetActive(false); //초상인물사진 끔
        note.SetActive(false); //여행수첩 끔
        dark.SetActive(false); //암막 끔
        outline.SetActive(false); //대화창 끔
    }
    /// <summary> 대화 캔버스 열기 </summary>
    public void OpenTalkCanvas()
    {
        dark.SetActive(true); //암막 켬
        outline.SetActive(true); //대화창 켬
        UI_Canvas.SetActive(false);  //전투 UI 끔
    }
    /// <summary> 상점 대화지문으로 진입 버튼 </summary>
    public void ShopButton()
    {
        if (inven.isOpen == false)
        {
            inven.Open();
        }
        if (inven.isStoreOpen == false)
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
            inven.StoreOpen();
        }
        SetDialog(currentNPC.ShopDialog);
    }

    /// <summary> 대화, 거래 등이 다 있는 처음 선택지 </summary>
    public void MainButton()
    {
        SetDialog(currentNPC.MainDialog);
    }

    /// <summary> 여행수첩 대화지문으로 진입 </summary>
    public void NoteButton()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
        note.SetActive(true); //여행수첩 켜줌
        SetDialog(currentNPC.NoteDialog);
    }

    /// <summary> 여행수첩의 각종 버튼들 누를때 </summary>
    public void NoteKeywordButton()
    {                                                               //방금 누른키워드 버튼의 텍스트 추출함 
        SetDialog(currentNPC.NoteTalk(EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text));
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
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

    /// <summary> 작별인사 </summary>
    public void FarewellButton()
    {
        note.SetActive(false);
        SetDialog(currentNPC.FarewellDialog);
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//버튼 다운 효과음
    }

    /// <summary> 선택지문용 버튼들 전부 끔 </summary>
    void SelectButtonOff()
    {
        for(int i =0; i< buttonBackgrounds.Length; i++)
        {
            buttonBackgrounds[i].SetActive(false);
        }
    }
}

