using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    /// <summary> 마을에서 부활 </summary>
    public void ReviveInTown()
    {
        LoadingScene.NextSceneName = "World";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        gameObject.SetActive(false);
    }

    /// <summary> 이 자리에서 부활 </summary>
    public void ReviveHere()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<Player>().Respawn();
        gameObject.SetActive(false);
    }

    /// <summary> 프로그램 종료 </summary>
    public void EndTheProgram()
    {
        Application.Quit();
    }
}
