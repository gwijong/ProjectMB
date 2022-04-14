using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //사용자 입력에 따라 플레이어 캐릭터를 움직이는 스크립트
    public float moveSpeed = 5f;  //앞뒤 움직임의 속도
    public float rotateSpeed = 180f;  //좌우 회전 속도

    private PlayerInput playerInput;  //플레이어 입력을 알려주는 컴포넌트
    private Rigidbody rigid;  //플레이어 캐릭터의 리지드바디
    private Animator ani;   //플레이어 캐릭터의 애니메이터
    private Character character;

    void Start()
    {//사용할 컴포넌트들의 참조 가져오기
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();
    }

    //FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    void FixedUpdate()
    {//물리 갱신 주기마다 움직임 실행
        if (character.stiffnessCount != 0 || character.State == Define.State.Die)
        {
			movePos = Vector3.zero;
			return;
        }
        KeyMove();
        PlayerAni();
        MouseMove();

    }

    //입력값에 따라 캐릭터를 앞뒤로 움직임
    void KeyMove()
    {
        Vector3 dir = new Vector3(0,0,0);

        if (playerInput.Front)
        {           
            dir = transform.forward;
        }
        else if (playerInput.Back)
        {
            dir = -transform.forward;
        }
        else if (playerInput.Right)
        {           
            dir = transform.right;
        }
        else if (playerInput.Left)
        {
            dir = -transform.right;
        }
        else
        {
            dir = new Vector3(0, 0, 0);
        }

        //상대적으로 이동할 거리 계산
        Vector3 MoveDistance = dir * moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + MoveDistance);     

    }
    void PlayerAni()
    {
        if (playerInput.Front == true || playerInput.Back == true || playerInput.Left == true || playerInput.Right == true)
        {
            character.AniOff();
            ani.SetBool("Move", true);
        }
        else
        {
            ani.SetBool("Move", false);
        }
    }

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    void MouseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit raycastHit;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // 이동 지점
                movePos = raycastHit.point;
                if (raycastHit.collider.gameObject.layer == 6)
                {
					character.target = raycastHit.collider.GetComponent<Character>();
				}			

			}
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        }
        if (movePos != Vector3.zero)
        {
            character.AniOff();
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            // 방향을 구한다. 
            Vector3 dir = movePos - transform.position;

            // 방향을 이용해 회전각을 구한다.
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            // 회전 및 이동 
            transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        }
        // 현재위치와 목표위치 사이의 거리를 구한다.
        float dis = Vector3.Distance(transform.position, movePos);

        // 목표지점 도달시 이동지점을 초기화해 추가적인 움직임을 제한한다. 
        if (dis <= 2f)
        {			
            if (character.target != null&& character.target.State!=Define.State.Die&&Input.GetMouseButtonDown(0))
			{
				character.Attack();
            }
			gameObject.GetComponent<Animator>().SetBool("Move", false);
            movePos = Vector3.zero;
        }

		

	}
}
/*
  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

	PlayerStat _stat;
	bool _stopSkill = false;


	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;

		_stat = gameObject.GetComponent<PlayerStat>();

		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
		}
		
			
	}


	protected override void UpdateMoving()
	{
		// 몬스터가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= 1)
			{
				State = Define.State.Skill;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{

			Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized*100, Color.green);
			if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
			{
				if (Input.GetMouseButton(0) == false)
					State = Define.State.Idle;
				return;
			}
			float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}


	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

	void OnHitEvent()
	{


		if(_lockTarget != null)
        {
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			targetStat.OnAttacked(_stat);
		}

		if (_stopSkill)
		{
			State = Define.State.Idle;
		}
		else
		{
			State = Define.State.Skill;
		}
	}

	



	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (State)
		{
			case Define.State.Idle:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Moving:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Skill:
				{
					if (evt == Define.MouseEvent.PointerUp)
						_stopSkill = true;
				}
				break;
		}
	}

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
				{
					if (raycastHit)
					{
						_destPos = hit.point;
						State = Define.State.Moving;
						_stopSkill = false;

						if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
							_lockTarget = hit.collider.gameObject;
						else
							_lockTarget = null;
					}
				}
				break;
			case Define.MouseEvent.Press:
				{
					if (_lockTarget == null && raycastHit)
						_destPos = hit.point;
				}
				break;
			case Define.MouseEvent.PointerUp:
				_stopSkill = true;
				break;
		}
	}
 */