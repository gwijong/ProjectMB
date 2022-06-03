using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 던전 최초 진입 시 한번 실행되는 자동문 </summary>
public class FirstGate : MonoBehaviour
{
    /// <summary> 문 콜라이더 </summary>
    public BoxCollider gateCollider;
    /// <summary> 문짝들 </summary>
    public GameObject[] doors;
    /// <summary> 캐릭터가 이 트리거 콜라이더에 충돌</summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)Define.Layer.Player)
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_door,doors[0].transform.position); //문 여는 효과음
            doors[0].SetActive(false);//왼쪽 문 비활성화
            doors[1].SetActive(false);//오른쪽 문 비활성화
            gateCollider.enabled = false; //문 충돌 비활성화
        }
    }
}
