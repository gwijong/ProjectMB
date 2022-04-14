using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
	protected Define.State _state = Define.State.Idle;

	protected Animator ani;
	public bool offensive = false;
	protected virtual void Awake()
    {
		ani = GetComponent<Animator>();
	}

	public Define.State State
	{
		get { return _state; }
		set
		{			
			_state = value;
	
			switch (_state)
			{
				case Define.State.Die:
					ani.SetBool("Die", true);
					break;
				case Define.State.Idle:
					AniOff();
					break;
				case Define.State.Moving:
					AniOff();
					ani.SetBool("Move", true);
					break;
				case Define.State.Casting:

					break;
			}
		}
	}
	public virtual void AniOff()
	{
		foreach (AnimatorControllerParameter parameter in ani.parameters)
		{
			ani.SetBool(parameter.name, false);
		}
	}
}
