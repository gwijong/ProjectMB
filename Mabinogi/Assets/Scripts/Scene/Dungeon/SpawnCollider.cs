using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    //충돌시 몬스터 한번 스폰됨
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Dungeon>().Spawn();
        gameObject.SetActive(false);
    }
}
