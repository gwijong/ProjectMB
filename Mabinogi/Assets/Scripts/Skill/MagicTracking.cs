using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 목표물을 향해 날아가는 마법 </summary>
public class MagicTracking : MonoBehaviour
{
    public Transform target;
    float deleteTime = 1.0f;
    bool deleteWait = false;
    void Start()
    {
        //업데이트 매니저의 Update메서드에 몰아주기
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        if (target != null)
        {
            Follow();//계속 따라감
        }
        if(deleteWait)
        {
            deleteTime -= Time.deltaTime;
            if(deleteTime < 0)
            {
                Destroy(gameObject); //이 마법 게임오브젝트 파괴
            };
        };
    }

    /// <summary> 따라다닐 타겟 오브젝트로 이 오브젝트 계속 이동</summary>
    public void Follow()
    {
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z);//따라갈 대상 목표물 좌표
        transform.position = Vector3.Lerp(gameObject.transform.position, followPos, 10f*Time.deltaTime); //목표물을 향해 계속 부드럽게 따라감

        if((followPos - gameObject.transform.position).magnitude < 1) //거리가 1 이내이면 
        {
            deleteWait = true;
            GameManager.update.UpdateMethod -= OnUpdate; //추적 중지
        }
    }
}
