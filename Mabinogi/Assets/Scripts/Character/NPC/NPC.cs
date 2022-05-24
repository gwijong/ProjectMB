using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 대화에 필요한 각종 요소들 가진 클래스 </summary>
public class NPC : Character
{
    /// <summary> 대화 처음 시작 시 외형묘사 대화 </summary>
    [SerializeField] Dialog[] appearanceDialogArray;
    /// <summary> 메인 선택지 대화</summary>
    [SerializeField] Dialog[] mainDialogArray;
    /// <summary> 여행수첩 대화 </summary>
    [SerializeField] Dialog[] noteDialogArray;
    /// <summary> 상점 대화 </summary>
    [SerializeField] Dialog[] shopDialogArray;
    /// <summary> 개인적인 이야기(여행수첩) 대화 </summary>
    [SerializeField] Dialog[] personalStoryDialogArray;
    /// <summary> 근처의 소문(여행수첩) 대화 </summary>
    [SerializeField] Dialog[] rumorsNearbyDialogArray;
    /// <summary> 아르바이트에 대하여(여행수첩) 대화 </summary>
    [SerializeField] Dialog[] partTimeJobDialogArray;
    /// <summary> 스킬에 대하여(여행수첩) 대화 </summary>
    [SerializeField] Dialog[] skillDialogArray;
    /// <summary> 작별 대화 </summary>
    [SerializeField] Dialog[] farewellDialogArray;
    /// <summary> NPC 초상인물사진 </summary>
    public Sprite portrait;


    /// <summary> 대화 처음 시작 시 외형묘사 대화 </summary>
    public Dialog AppearanceDialog { get; private set; }
    /// <summary> 메인 선택지 대화 </summary>
    public Dialog MainDialog { get; private set; }
    /// <summary> 여행수첩 대화 </summary>
    public Dialog NoteDialog { get; private set; }
    /// <summary> 상점 대화 </summary>
    public Dialog ShopDialog { get; private set; }
    /// <summary> 상점 대화 </summary>

    /// <summary> 개인적인 이야기(여행수첩) 대화 </summary>
    public Dialog PersonalStoryDialog { get; private set; }
    /// <summary> 근처의 소문(여행수첩) 대화 </summary>
    public Dialog RumorsNearbyDialog { get; private set; }
    /// <summary> 아르바이트에 대하여(여행수첩) 대화</summary>
    public Dialog PartTimeJobDialog { get; private set; }
    /// <summary> 스킬에 대하여(여행수첩) 대화 </summary>
    public Dialog SkillDialog { get; private set; }
    /// <summary> 작별 대화 </summary>
    public Dialog FarewellDialog { get; private set; }

    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Talk; //대화 리턴
    }

    private void Start()
    {
        AppearanceDialog = Dialog.CreateDialogList(portrait, characterName, appearanceDialogArray);//외형 묘사 대사
        MainDialog = Dialog.CreateDialogList(portrait, characterName, mainDialogArray); //메인 선택지 대사
        NoteDialog = Dialog.CreateDialogList(portrait, characterName, noteDialogArray); //여행수첩 대사
        ShopDialog = Dialog.CreateDialogList(portrait, characterName, shopDialogArray); //상점 호객 대사

        PersonalStoryDialog = Dialog.CreateDialogList(portrait, characterName, personalStoryDialogArray); //개인적인 이야기
        RumorsNearbyDialog = Dialog.CreateDialogList(portrait, characterName, rumorsNearbyDialogArray);  //근처의 소문
        PartTimeJobDialog = Dialog.CreateDialogList(portrait, characterName, partTimeJobDialogArray);  //아르바이트에 대해
        SkillDialog = Dialog.CreateDialogList(portrait, characterName, skillDialogArray); //스킬에 대해
        FarewellDialog = Dialog.CreateDialogList(portrait, characterName, farewellDialogArray); //작별 인사
    }

    /// <summary> npc들마다 개인적인 이야기 근처의 소문 따로 가지고 있는거 wantText를 스위치 돌려서 본인만의 다이얼로그 켜게 함 </summary>
    public virtual Dialog NoteTalk(string wantText) 
    {
        Dialog dialog;
        switch (wantText)
        {
            default:
                dialog = PersonalStoryDialog;
                break;
            case "개인적인 이야기":
                dialog = PersonalStoryDialog;
                break;
            case "근처의 소문":
                dialog = RumorsNearbyDialog;
                break;
            case "스킬에 대하여":
                dialog = SkillDialog;
                break;
            case "아르바이트에 대하여":
                dialog = PartTimeJobDialog;
                break;
        }
        return dialog;
    }
}
[System.Serializable]
/// <summary> 대화하는 모든 내용 하나 </summary>
public class Dialog
{
    /// <summary> NPC 초상인물사진 </summary>
    public Sprite portrait;
    /// <summary> NPC 이름</summary>
    public string npcName;
    /// <summary> 현재 대사 텍스트 </summary>
    public string currentText;
    /// <summary> 다음 대화 내용 </summary>
    public Dialog next;
    /// <summary> 버튼 선택지들 </summary>
    public DialogButtonInfo[] buttonArray;
    /// <summary> 초상인물사진 활성화 여부 </summary>
    public bool portraitActive;
    /// <summary> 대화하는 모든 내용 하나 들어감 </summary>
    public Dialog(string wantCurrentText, Sprite wantPortrait = null, string wantNpcName = null,  DialogButtonInfo[] wantButtonArray = null, Dialog wantNext = null)
    {
        currentText = wantCurrentText; //원하는 대사
        portrait = wantPortrait; //NPC 스프라이트
        npcName = wantNpcName; //NPC 이름
        buttonArray = wantButtonArray; //버튼 선택지들
        next = wantNext; //다음 대화
    }

    /// <summary> 다이어로그 배열 생성 </summary>
    public static Dialog CreateDialogList(Sprite wantPortrait, string wantNpcName, Dialog[] dialogArray)
    {
        if(dialogArray.Length <= 0) // dialogArray 배열의 길이가 0이면 리턴
        {
            return null;
        }

        int i = 0;
        for (; i< dialogArray.Length -1; i++)  // dialogArray 배열의 길이-1 만큼 반복
        {
            if(dialogArray[i].portrait == null) //초상인물사진이 비어있으면
            {
                dialogArray[i].portrait = wantPortrait; //기본 초상인물사진 대입
            }
            if (dialogArray[i].npcName == null || dialogArray[i].npcName == "") //NPC이름이 비어있으면
            {
                dialogArray[i].npcName = wantNpcName; //기본 NPC 이름 대입
            }
            dialogArray[i].next = dialogArray[i + 1];//현재 대화내용의 다음 대화내용 설정;
        }

        //dialogArray의 마지막 번째 다이어로그에도 기본값들 대입. 마지막이므로 next는 없음
        if (dialogArray[i].portrait == null)
        {
            dialogArray[i].portrait = wantPortrait;
        }
        if (dialogArray[i].npcName == null || dialogArray[i].npcName == "")
        {
            dialogArray[i].npcName = wantNpcName;
        }
        return dialogArray[0];//첫번째 대화 반환
    }
}

/// <summary> 버튼 정보 </summary>
[System.Serializable] //해당 클래스가 인스펙터 창에 노출됨
public class DialogButtonInfo
{
    /// <summary> 대화중에 버튼 텍스트에 표시될 버튼 이름 </summary>
    public string buttonName;
    /// <summary> 버튼 누르면 어디로 넘어갈지 정하는 버튼 타입 </summary>
    public Define.TalkButtonType type;
}

