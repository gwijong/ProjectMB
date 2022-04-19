using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character player;  //Player 캐릭터 딱 하나
    float distance;
    Vector3 dest;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    void Update()
    {
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

            if (Physics.Raycast(ray, out hit, 100f, ~LayerMask.NameToLayer("Tree")))
            {
                Character target = hit.collider.GetComponent<Character>();

                //캐릭터 대상을 삼을 수가 없어!          근데 땅 찍음
                if(!player.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    player.MoveTo(hit.point);
                };

                if(target != null)
                {
                    player.Attack(target);
                }
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
}




