using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//이건 딱 하나만 만들어
//캐릭터에 제발 넣지 말고
public class PlayerController : MonoBehaviour
{
    Character player;  //Player 캐릭터 딱 하나
    Animator ani;
    float distance;
    Vector3 dest;
    Rigidbody rigid;

    private void Start()
    {
        dest = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        ani = player.GetComponent<Animator>();
        rigid = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        MouseMove();        
        KeyMove();           
        SpaceOffensive();
    }

    void SpaceOffensive()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ani.GetBool("Offensive"))
        {
            ani.SetBool("Offensive", false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !ani.GetBool("Offensive"))
        {
            ani.SetBool("Offensive", true);
        }
    }
    void MouseMove()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(ray, out hit, 100f);
            dest = hit.point;
            if (hit.collider.gameObject.layer == (int)Define.Layer.Enemy)
            {
                player.attackTarget = hit.collider.GetComponent<Character>();
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                player.attackTarget = null;

            }
        };

        if (player.attackTarget != null)
        {
            distance = Vector3.Distance(player.transform.position, player.attackTarget.transform.position);
            if (distance > 2f)
            {
                player.MoveTo(player.attackTarget.transform.position);
                ani.SetBool("Move", true);
                ani.SetBool("Offensive", true);
            }
            else
            {
                player.MoveTo(player.transform.position);
                ani.SetBool("Move", false);
                player.Attack(player.attackTarget);
            }
        }
        else
        {
            distance = Vector3.Distance(player.transform.position, dest);
            if (distance > 1f)
            {
                player.MoveTo(dest);
                ani.SetBool("Move", true);
            }
            else
            {
                player.MoveTo(player.transform.position);
                ani.SetBool("Move", false);
            }

        }
    }

    void KeyMove()
    {

        
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.transform.rotation = 
            Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.forward), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.rotation =
            Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.back), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.transform.rotation =
            Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.left), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.rotation =
            Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.right), 1f);
        }


        if (Input.GetKey(KeyCode.W))
        {
            ani.SetBool("Move", true);
            player.transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ani.SetBool("Move", true);
            player.transform.position += Vector3.back * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ani.SetBool("Move", true);
            player.transform.position += Vector3.left * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ani.SetBool("Move", true);
            player.transform.position += Vector3.right * 10 * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.None))
        {
            ani.SetBool("Move", false);
        }
    }
}


