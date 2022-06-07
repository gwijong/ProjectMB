using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
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
    }

    /// <summary> 따라다닐 타겟 오브젝트로 이 오브젝트 계속 이동</summary>
    public void Follow()
    {
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector3.Lerp(gameObject.transform.position, followPos, 4f*Time.deltaTime);

        if((followPos - gameObject.transform.position).magnitude < 1) //거리가 1 이내이면 
        {
            Destroy(gameObject);
            GameManager.update.UpdateMethod -= OnUpdate;
        }
    }
}
