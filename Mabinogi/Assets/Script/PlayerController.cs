using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character player;  //Player ĳ���� �� �ϳ�
    float distance;
    Vector3 dest;
    Character target;
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy;
    bool distanceCheckAttack;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    void Update()
    {
        if (target != null)
        {
            distance = (target.transform.position - player.transform.position).magnitude;
        }
        else
        {
            distance = 10;
        }
        

        if (distanceCheckAttack)
        {
            DistanceCheckAttack();
        }
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

                //ĳ���� ����� ���� ���� ����!          �ٵ� �� ����
                if(!player.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    player.MoveTo(hit.point);
                };
          
                if (target != null)
                {                   
                    if (distance > 2)
                    {
                        player.MoveTo(target.transform.position);
                        distanceCheckAttack = true;
                    }
                    else if(distance <= 2)
                    {                      
                        player.Attack(target);
                        distanceCheckAttack = false;
                    }

                }
            };
        };
    }

    void DistanceCheckAttack()
    {
        if(target == null)
        {
            distanceCheckAttack = false;
            return;
        }
        if(target != null)
        {
            if (distance <= 2)
            {
                player.Attack(target);
                distanceCheckAttack = false;
            }
        }
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


