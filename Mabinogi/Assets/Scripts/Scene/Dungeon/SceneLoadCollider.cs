using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 플레이어가 목표지점에 도달하면 씬 로드</summary>
public class SceneLoadCollider : MonoBehaviour
{
    /// <summary> 로드할 씬 이름 </summary>
    public string SceneName;
    /// <summary> 트리거 충돌 시 씬 로딩 </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Player) //플레이어일 경우
        {
            LoadingScene.NextSceneName = SceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");//로딩 씬 후 SceneName 씬 로딩
        }
    }
}
