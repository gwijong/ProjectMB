using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{

    public void ReviveInTown()
    {
        LoadingScene.NextSceneName = "World";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        gameObject.SetActive(false);
    }

    public void ReviveHere()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<Player>().Respawn();
        gameObject.SetActive(false);
    }

    public void EndTheProgram()
    {
        Application.Quit();
    }
}
