using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 좌측 컨트롤 키 누르면 나오는 가이드라인 </summary>
public class GuideLine : MonoBehaviour
{
    /// <summary> 라인 렌더러 컴포넌트 </summary>
    LineRenderer line;
    /// <summary> 조준할 NPC </summary>
    public static Character targetCharacter;
    /// <summary> 플레이어 캐릭터 </summary>
    Character player;
    /// <summary> 렌더러 컴포넌트 </summary>
    Renderer rend;
    float startAngle;

    void Start()
    {
        rend = GetComponent<Renderer>(); //렌더러 할당
        line = GetComponent<LineRenderer>();//라인렌더러 할당
        line.widthMultiplier = 0.005f;//점선의 굵기       
        line.textureMode = LineTextureMode.Tile; //라인렌더러 모드를 타일로 설정해서 점선 반복
    }
    
    void LateUpdate()
    {
        Vector3[] pos = new Vector3[line.positionCount];

        if (FindObjectOfType<DialogTalk>().dark.gameObject.activeSelf == true)
        {
            line.SetPositions(pos);
            targetCharacter = null;
            return;
        }
        if (player != null)
        {
            if (player.die == true)
            {
                line.SetPositions(pos);
                targetCharacter = null;
                return;
            }
        }

        rend.material.mainTextureScale = new Vector2(Vector2.Distance(line.GetPosition(0), line.GetPosition(line.positionCount - 1)) / line.widthMultiplier, 1);
        rend.material.mainTextureScale = (rend.material.mainTextureScale * startAngle)/ rend.material.mainTextureScale*8f;

        if (Input.GetKeyDown(KeyCode.LeftControl)) // 키보드 왼쪽 컨트롤 키를 누르면
        {

            player = PlayerController.controller.playerCharacter; //플레이어 캐릭터 할당
            Character[] characters = GameObject.FindObjectsOfType<Character>();//모든 캐릭터 긁어옴
            float neardistance = 100000;//마우스 커서와 가장 가까운 NPC와의 거리
            Character nearCharacter = null; //마우스 커서와 가장 가까운 NPC 타겟;
            foreach(Character current in characters)
            {
                if (player.GetOffensive()) //플레이어가 전투모드이면
                {
                    if (current.gameObject.layer == (int)Define.Layer.Enemy && current.die == false) //레이어가 적이고 사망하지 않은 경우
                    {
                        float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                        if (currentDistance < neardistance)
                        {
                            neardistance = currentDistance;//가장 가까운 거리 갱신
                            nearCharacter = current;//가장 가까운 캐릭터 갱신
                        }
                    }
                }
                else //플레이어가 일상모드이면
                {
                    //레이어가 가축이나 NPC이고 사망하지 않았다면
                    if (current.gameObject.layer == (int)Define.Layer.Livestock || current.gameObject.layer == (int)Define.Layer.NPC && current.die == false)
                    {
                        float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                        if (currentDistance < neardistance)
                        {
                            neardistance = currentDistance;//가장 가까운 거리 갱신
                            nearCharacter = current;//가장 가까운 캐릭터 갱신
                        }
                    }
                }
            }
            targetCharacter = nearCharacter;//최종적으로 가까운 캐릭터를 타겟 캐릭터로 지정
            PlayerController.controller.target = targetCharacter;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl)) //키보드 왼쪽 컨트롤 키를 떼면
        {
            targetCharacter = null; //타겟을 비워줌
        }

        if (targetCharacter == null)//타겟 캐릭터가 없는 경우
        {
            line.SetPositions(pos);
            return;
        }
        Vector3 targetCenter = targetCharacter.transform.position;
        targetCenter.y += targetCharacter.nameYpos * 1f; //캐릭터의 이름 높이 Y좌표에 맞춰 원의 중심 잡음
        Vector3 standard = Camera.main.WorldToScreenPoint(targetCenter);
        
        pos[0] = Input.mousePosition;//선의 최초 시작 위치는 마우스 입력된 좌표
        startAngle = Mathf.Atan2(pos[0].x - standard.x, pos[0].y - standard.y);

        for (int i = 1; i < pos.Length; i++)
        {
            float currentValue = Mathf.PI * 2 * (i-1) / (line.positionCount - 2);
            currentValue += startAngle;
            Vector3 currentPosition = new Vector3(Mathf.Sin(currentValue), Mathf.Cos(currentValue));
            currentPosition *= 100.0f * targetCharacter.nameYpos*0.3f; //캐릭터의 이름 높이 Y좌표에 비례해 원의 크기 설정
            //0이면 0 9면 2PI
            pos[i] = standard + currentPosition;
        }

        for(int i = 0; i < pos.Length; i++)
        {
            pos[i].z = 1;
            pos[i] = Camera.main.ScreenToWorldPoint(pos[i]);
        }
        line.SetPositions(pos);
    }

}
