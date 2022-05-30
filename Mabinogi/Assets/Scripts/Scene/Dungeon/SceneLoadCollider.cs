using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadCollider : MonoBehaviour
{
    public string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            LoadingScene.NextSceneName = SceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
}
