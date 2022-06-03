using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    //충돌시 몬스터 한번 스폰됨
    private void OnTriggerEnter(Collider other)
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_monster_appear1, transform.position); //몬스터 등장 효과음
        FindObjectOfType<Dungeon>().Spawn();//몬스터 스폰
        gameObject.SetActive(false); //스폰 후 스폰 게임오브젝트 비활성화
    }
}
