using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 플레이어 사망 UI </summary>
public class PlayerDie : MonoBehaviour
{
    public GameObject diePanel;
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
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);//현재 씬의 배경음악으로 전환
        FindObjectOfType<Player>().Respawn();//플레이어 부활
        diePanel.SetActive(false);
        gameObject.SetActive(false);
    }

    /// <summary> 프로그램 종료 </summary>
    public void EndTheProgram()
    {
        Application.Quit();
    }
}
