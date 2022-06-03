using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBgmCollider : MonoBehaviour
{
    /// <summary> 보스방 배경음악으로 변경해주는 트리거 </summary>
    private void OnTriggerEnter(Collider other)
    {
        GameManager.soundManager.PlayBgmPlayer(Define.Scene.Boss);
    }
}
