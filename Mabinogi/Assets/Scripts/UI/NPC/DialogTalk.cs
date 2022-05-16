using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogTalk : MonoBehaviour
{
    /// <summary> 맨 처음 NPC 외형 설명하는 지문 </summary>
    public string firstScript;
    /// <summary> 대화, 거래 선택 지문 </summary>
    public string chooseSciript;
    /// <summary> 여행수첩 대화지문 </summary>
    public string noteScript;
    /// <summary> 상점 대화지문 </summary>
    public string shopScript;
    /// <summary> 개인적인 이야기 대화지문 </summary>
    public string[] personalstory;

    /// <summary> NPC 이름 텍스트 </summary>
    public Text name;
    /// <summary> 대화지문 텍스트 </summary>
    public Text textScript;
    /// <summary> 초상인물사진 </summary>
    public GameObject portrait;
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
    /// <summary> 대화 진행 상황 </summary>
    bool[] progress = { false, false, false, false};
    /// <summary> 현재 실행중인 코루틴 </summary>
    IEnumerator Cor;

    public void StartTalk()
    {
        FirstTyping();
    }

    private void Update()
    {
        if(progress[0] ==true && progress[1] == false && Input.anyKeyDown)
        {
            ChooseTyping();
        }
        if(progress[2] == true && Input.anyKeyDown)
        {
            progress[2] = false;
            PersonalStoryNext();
        }
    }
    /// <summary> 텍스트 한글자씩 출력 코루틴 </summary>
    IEnumerator TextOutput(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textScript.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary> 맨 처음 NPC 외형 설명하는 지문 </summary>
    void FirstTyping()
    {
        portrait.SetActive(false);
        dark.SetActive(true);
        outline.SetActive(true);
        textScript.text = "";
        UI_Canvas.SetActive(false);
        CoroutineStart(TextOutput(firstScript));
        progress[0] = true;
    }

    /// <summary> 대화와 거래 선택지문 </summary>
    void ChooseTyping()
    {
        portrait.SetActive(true);
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[1].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화를 한다";
        buttonBackgrounds[1].GetComponentInChildren<Text>().text = "거래를 한다";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(EndTalkButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.RemoveListener(ShopButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.AddListener(ShopButton);
        progress[1] = true;
        textScript.text = "";
        CoroutineStart(TextOutput(chooseSciript));
    }

    /// <summary> 여행수첩 대화지문 </summary>
    void NoteTyping()
    {
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화 끝내기";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        note.SetActive(true);
        textScript.text = "";
        CoroutineStart(TextOutput(noteScript));
    }

    /// <summary> 상점 대화지문 </summary>
    void ShopTyping()
    {
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화 끝내기";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        textScript.text = "";
        CoroutineStart(TextOutput(shopScript));
    }
    /// <summary> 대화 상대 NPC에게서 대화 지문들을 가져옴 </summary>
    public void SetText(string first, string choose, string note, string shop, string[] personalStory)
    {
        firstScript = first;
        chooseSciript = choose;
        noteScript = note;
        shopScript = shop;
        personalstory = personalStory;
    }

    /// <summary> 대화 끝내기 버튼 </summary>
    public void EndTalkButton()
    {
        SelectButtonOff();
        CloseTalkCanvas();
        UI_Canvas.SetActive(true);
        progress[0] = false;
        progress[1] = false;
    }

    /// <summary> 대화 캔버스의 구성요소들을 전부 끔 </summary>
    public void CloseTalkCanvas()
    {
        portrait.SetActive(false);
        note.SetActive(false);
        dark.SetActive(false);
        outline.SetActive(false);
    }

    /// <summary> 상점 대화지문으로 진입 버튼 </summary>
    public void ShopButton()
    {
        ShopTyping();
    }

    /// <summary> 여행수첩 대화지문으로 진입 </summary>
    public void NoteButton()
    {
        NoteTyping();
    }

    /// <summary> 여행수첩의 개인적인 이야기 실행 </summary>
    public void PersonalStoryButton()
    {
        PersonalStory();
    }

    /// <summary> 개인적인 이야기 텍스트 출력 </summary>
    void PersonalStory()
    {
        note.SetActive(false);
        buttonBackgrounds[0].SetActive(false);
        textScript.text = "";
        CoroutineStart(TextOutput(personalstory[0]));
        progress[2] = true;
    }

    /// <summary> 개인적인 이야기 이후 좋은 대화였던 것 같다 출력 </summary>
    void PersonalStoryNext()
    {
        note.SetActive(true);
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        textScript.text = "";
        CoroutineStart(TextOutput(personalstory[1]));
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

