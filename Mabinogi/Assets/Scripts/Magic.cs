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
        Follow();
    }

    public void Follow()
    {
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z);
        //따라다닐 플레이어 오브젝트로 이 오브젝트 계속 이동
        transform.position = Vector3.Lerp(gameObject.transform.position, followPos, 4f*Time.deltaTime);

        if((followPos - gameObject.transform.position).magnitude < 2)
        {
            gameObject.SetActive(false);
        }
    }
}
