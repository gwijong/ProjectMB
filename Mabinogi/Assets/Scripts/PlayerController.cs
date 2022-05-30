using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary> 입력값 받아서 플레이어 이동시키는 스크립트</summary>
public class PlayerController : MonoBehaviour
{
    public static PlayerController controller;
    /// <summary>플레이어 캐릭터 딱 하나</summary>
    public GameObject player;
    public Character playerCharacter { get; private set; }
    /// <summary>마우스로 클릭한 타겟</summary>
    public Interactable target { get;private set; }
    /// <summary>레이어마스크</summary>
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player | 1 << (int)Define.Layer.Item | 1 << (int)Define.Layer.NPC;

    public GameObject talkCanvasOutline;//대화 캔버스 아웃라인(대화중인지 체크용)
    private void Awake()
    {   //게임 시작할때 캐릭터 플레이어 설정하는 구간
        PlayerSetting();
        controller = this;
        //업데이트 매니저의 Update메서드에 몰아주기
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
        FindObjectOfType<PlayerInventory>().owner = playerCharacter;
    }

    void OnUpdate()
    {
        if (playerCharacter.die == true) //캐릭터 사망시 리턴
        {
            return;
        }
        if (talkCanvasOutline.activeSelf) //대화중이면 리턴
        {
            return;
        }    
        SkillInput();
        MouseInput();        
        KeyMove();
        SpaceOffensive();
    }

    /// <summary>스페이스바 입력받아 일상, 전투모드 전환</summary>
    void SpaceOffensive()
    {
        if (Input.GetKeyDown(KeyCode.Space))//스페이스바 입력하면
        {
            playerCharacter.SetOffensive();//전투, 일상모드 전환
        };
    }


    /// <summary>마우스 입력 이동이나 타겟 지정</summary>
    void MouseInput()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //UI 버튼 누를 경우 리턴
        {
            return;
        }      
        //왼쪽 컨트롤 키를 누른 상태로 캐릭터를 마우스 좌클릭하면 그 캐릭터가 플레이어가 됨
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //카메라에서 마우스좌표로 레이를 쏨
            RaycastHit hit;  //충돌 물체 정보 받아올 데이터 컨테이너

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//레이, 충돌 정보, 레이 거리, 레이어마스크
            {
                target = hit.collider.GetComponent<Character>();  //충돌한 대상이 캐릭터면 타겟에 할당 시도
                
                if (target != null)
                {
                    if(target.gameObject.layer == (int)Define.Layer.NPC) //플레이어가 마을 NPC가 될 수는 없음
                    {
                        return;
                    }
                    if(player.GetComponent<EnemyDummyAI>()!=null)
                        player.GetComponent<EnemyDummyAI>().enabled = true; //기존 플레이어 캐릭터의 인공지능 켜줌
                    if(player.tag == "Enemy")
                    {
                        player.layer = (int)Define.Layer.Enemy;  //기존 플레이어 캐릭터의 레이어를 적으로 바꿈
                    }else if(player.tag == "Friendly")
                    {
                        player.layer = (int)Define.Layer.Livestock;//가축이면 캐릭터의 레이어를 가축으로 바꿈
                    }
                    player.GetComponentInChildren<SkillBubble>().GetComponent<Button>().enabled = false; //기존 캐릭터의 말풍선 눌려서 스킬취소하는 기능 꺼줌
                    player = hit.collider.gameObject;  //마우스 좌클릭으로 지정한 플레이어 캐릭터를 플레이어로 지정
                    if (player.GetComponent<EnemyDummyAI>() != null)
                        player.GetComponent<EnemyDummyAI>().stopCoroutine();//인공지능 코루틴 중지
                    PlayerSetting();
                }

            };
            return;
        }

        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))  //마우스 좌클릭 입력되면
        {
            if(playerCharacter.GetloadedSkill() == Skill.windmill)
            {
                playerCharacter.Windmill();
                return;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //카메라에서 마우스좌표로 레이를 쏨
            RaycastHit hit;  //충돌 물체 정보 받아올 데이터 컨테이너

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//레이, 충돌 정보, 레이 거리, 레이어마스크
            {
                target = hit.collider.GetComponent<Interactable>();  //충돌한 대상이 캐릭터면 타겟에 할당 시도
                //캐릭터 대상 할당 실패            충돌한 대상이 땅이면
                if(!playerCharacter.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    if(Inventory.mouseItem.GetItemType()!= Define.Item.None && Inventory.OutAllInvenBoundaryCheck())
                    {
                        playerCharacter.MoveTo(hit.point,Define.MoveType.DropItem);  //충돌한 좌표로 플레이어 캐릭터 이동
                    }
                    else
                    {
                        playerCharacter.MoveTo(hit.point);  //충돌한 좌표로 플레이어 캐릭터 이동
                    }
                    
                };
            };
        };
    }

    /// <summary>타겟 지정</summary>
    public void SetTarget(Interactable wantTarget)
    {
        target = wantTarget;
        playerCharacter.SetTarget(target);
    }

    /// <summary>플레이어 캐릭터로 전환</summary>
    void PlayerSetting()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        player.layer = (int)Define.Layer.Player;  //플레이어의 레이어를 플레이어로 변경
        playerCharacter = player.GetComponent<Character>();  //플레이어의 캐릭터 컴포넌트를 가져옴
        player.GetComponentInChildren<SkillBubble>().gameObject.GetComponentInChildren<Button>().enabled = true; //플레이어의 말풍선 눌려서 스킬취소하는 기능 켜줌
        if (player.GetComponent<EnemyDummyAI>() != null)//인공지능이 있으면
            player.GetComponent<EnemyDummyAI>().enabled = false; //인공지능 꺼줌
        GetComponentInChildren<CameraPivot>().following_object = player.transform; //카메라가 플레이어를 추적하도록 함
    }
    /// <summary>키보드 입력 이동</summary>
    void KeyMove()
    {   //                                              가로                         세로
        Vector2 keyInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (keyInput.magnitude > 1.0f) //키 입력 벡터2 좌표 길이가 1보다 큰 경우  ex)대각선 길이가 루트2가 된 경우
        {
            keyInput.Normalize(); //1로 정규화
        };

        if(keyInput.magnitude > 0.1f)  //키 입력 벡터 2 길이가 0.1보다 큰 경우 이동
        {
            Vector3 cameraForward = Camera.main.transform.forward;  //카메라 앞 방향
            cameraForward.y = 0.0f;//높이 값 무시
            cameraForward.Normalize();//1로 정규화
        
            Vector3 cameraRight = Camera.main.transform.right;  //카메라 오른쪽 방향
            cameraRight.y = 0.0f;//높이 값 무시
            cameraRight.Normalize();//1로 정규화

            cameraForward *= keyInput.y;  //키보드 WS 입력값 받음
            cameraRight *= keyInput.x;  //키보드 AD 입력값 받음

            Vector3 calculatedLocation = cameraForward*5 + cameraRight*5;  //계산된 좌표에 5를 곱해줌. 내비메시 StoppingDistance가 2이므로 2 이상이어야함
            calculatedLocation += player.transform.position; //계산된 좌표에 플레이어의 현재 위치를 더함

            playerCharacter.SetTarget(null);//키보드 이동중이므로 지정된 타겟을 비움
            playerCharacter.MoveTo(calculatedLocation); //플레이어위치에서 계산된 좌표로 조금씩 이동
        };
    }

    /// <summary>키보드 입력으로 스킬 시전</summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerCharacter.Casting(Define.SkillState.Defense);//디펜스 시전
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerCharacter.Casting(Define.SkillState.Smash);//스매시 시전
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerCharacter.Casting(Define.SkillState.Counter);//카운터 시전
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerCharacter.Casting(Define.SkillState.Windmill);//윈드밀 시전
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerCharacter.Casting(Define.SkillState.Icebolt);//윈드밀 시전
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerCharacter.GetComponentInChildren<SkillBubble>().SkillCancel();//스킬 취소, 컴벳으로 전환
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_cancel);//스킬 취소 효과음
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //playerCharacter.Casting(Define.SkillState.Combat);//스킬 취소, 컴벳으로 전환
            playerCharacter.GetComponentInChildren<SkillBubble>().SkillCancel();
        }
    }
}


