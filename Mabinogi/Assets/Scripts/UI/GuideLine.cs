using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    LineRenderer line;
    public static Character targetCharacter;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.widthMultiplier = 0.01f;      
    }
    
    // 일상 전투모드 구분하고 레이어 마스크 씌우고 플레이어 캐릭터의 캐릭터 자신은 체크 안하게
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            float neardistance = 100000;
            Character nearCharacter = null;
            foreach(Character current in characters)
            {
                //여기서 지금 상태에 따라서 몬스터 태그 확인하고 필요 없으면 버림, 플레이어 캐릭터면 버림 구분해서 컨티뉴로 넘김
                float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                if(currentDistance< neardistance)
                {
                    neardistance = currentDistance;
                    nearCharacter = current;                
                }
            }
            targetCharacter = nearCharacter;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            targetCharacter = null;
        }

        Vector3[] pos = new Vector3[line.positionCount];
        if (targetCharacter == null)
        {
            line.SetPositions(pos);
            return;
        }
        Vector3 targetCenter = targetCharacter.transform.position;
        targetCenter.y += targetCharacter.nameYpos * 1f;
        Vector3 standard = Camera.main.WorldToScreenPoint(targetCenter);
        
        pos[0] = Input.mousePosition;
        float startAngle = Mathf.Atan2(pos[0].x - standard.x, pos[0].y - standard.y);

        for (int i = 1; i < pos.Length; i++)
        {
            float currentValue = Mathf.PI * 2 * (i-1) / (line.positionCount - 2);
            currentValue += startAngle;
            Vector3 currentPosition = new Vector3(Mathf.Sin(currentValue), Mathf.Cos(currentValue));
            currentPosition *= 100.0f * targetCharacter.nameYpos*0.45f;
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
