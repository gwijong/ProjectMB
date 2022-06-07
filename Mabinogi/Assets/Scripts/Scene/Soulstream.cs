using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 게임 최초 씬 진행</summary>
public class Soulstream : MonoBehaviour
{
    /// <summary> 나오 NPC 게임오브젝트 </summary>
    public GameObject nao;
    /// <summary> 페이드아웃용 이미지 </summary>
    public Image whiteImage;
    /// <summary> 배경음악 재생 게임오브젝트 </summary>
    public GameObject bgmPlayer;
    /// <summary> 효과음 재생 게임오브젝트 </summary>
    public GameObject sfxPlayer;
    /// <summary> 나오 등장 배경음악 </summary>
    public AudioClip naoAppear;
    /// <summary> 부엉이 소리 </summary>
    public AudioClip naoStage;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(NaoAppear());
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc키 누르면 스킵
        {
            LoadingScene.NextSceneName = "Tutorial"; //튜토리얼 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
    /// <summary> 나오 등장 </summary>
    IEnumerator NaoAppear()
    {
        yield return new WaitForSeconds(0.5f);
        sfxPlayer.GetComponent<AudioSource>().clip = naoStage;//부엉이 소리 재생
        sfxPlayer.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7.0f);
        bgmPlayer.GetComponent<AudioSource>().clip = naoAppear; //나오 등장 배경음악 재생
        bgmPlayer.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 20; i++)  //페이드아웃
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 20);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        GameObject go = Instantiate(nao);
        go.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 100; i++) //페이드인
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
    }

    /// <summary> 나오 퇴장 </summary>
    public IEnumerator NaoDisappear()
    {
        //yield return new WaitForSeconds(0f);
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100); //페이드아웃
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        LoadingScene.NextSceneName = "Tutorial"; //튜토리얼 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
