using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soulstream : MonoBehaviour
{
    public GameObject nao;
    public Image whiteImage;
    public GameObject bgmPlayer;
    public GameObject sfxPlayer;
    public AudioClip naoAppear;
    public AudioClip naoStage;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(NaoAppear());
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadingScene.NextSceneName = "Tutorial";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
    IEnumerator NaoAppear()
    {
        yield return new WaitForSeconds(0.5f);
        sfxPlayer.GetComponent<AudioSource>().clip = naoStage;
        sfxPlayer.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7.0f);
        bgmPlayer.GetComponent<AudioSource>().clip = naoAppear;
        bgmPlayer.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 20; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 20);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        GameObject go = Instantiate(nao);
        go.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
    }

    public IEnumerator NaoDisappear()
    {
        //yield return new WaitForSeconds(0f);
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        LoadingScene.NextSceneName = "Tutorial";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
