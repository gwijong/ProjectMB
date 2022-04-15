using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//이건 딱 하나만 만들어
//캐릭터에 제발 넣지 말고
public class PlayerController : MonoBehaviour
{
    [SerializeField] Character target;  //Player 캐릭터 딱 하나
    [SerializeField] Define.mouseKey mouse;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    void Update()
    {
        if (target == null) return;

        if(Input.GetMouseButtonDown((int)mouse))
        {
            target.MoveTo(Vector3.zero);
        };
    }
}
