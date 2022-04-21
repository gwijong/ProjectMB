using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character player;  //Player 캐릭터 딱 하나
    Character target;
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy;

    private void Start()
    {      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    void Update()
    {
        SkillInput();
        MouseInput();        
        KeyMove();
        SpaceOffensive();
    }

    void SpaceOffensive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetOffensive();
        };
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                target = hit.collider.GetComponent<Character>();

                //캐릭터 대상을 삼을 수가 없어!          근데 땅 찍음
                if(!player.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    player.MoveTo(hit.point);
                };
            };
        };
    }

    void KeyMove()
    {
        Vector2 keyInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (keyInput.magnitude > 1.0f)
        {
            keyInput.Normalize();
        };

        if(keyInput.magnitude > 0.1f)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();
        
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            cameraForward *= keyInput.y;
            cameraRight *= keyInput.x;

            Vector3 calculatedLocation = cameraForward + cameraRight;
            calculatedLocation += player.transform.position;

            player.SetTarget(null);
            player.MoveTo(calculatedLocation);
        };
    }
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("플레이어: 디펜스 시전");
            player.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("플레이어: 스매시 시전");
            player.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("플레이어: 카운터 시전");
            player.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("플레이어: 스킬 취소, 컴벳으로 전환");
            player.Casting(Define.SkillState.Combat);
        }


    }
}


