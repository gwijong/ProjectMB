using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 모리안 등장 씬 진행</summary>
public class Intro : MonoBehaviour
{
    /// <summary> 화면 하단 모리안이 말할 대사들 </summary>
    public string[] talk;
    /// <summary> 대사를 출력할 텍스트 컴포넌트 </summary>
    public Text text;
    /// <summary> 화면 하단의 검은 그라데이션</summary>
    public Image dark;
    void Start()
    {
        StartCoroutine(Progress()); //씬이 시작되면 코루틴 한번 시작
    }

    private void Update()
    {
        //카메라 위치를 조금씩 z축으로 땡김
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+(0.7f*Time.deltaTime));

        if(Input.GetKeyDown(KeyCode.Escape)) //ESC키 누르면 스킵
        {
            LoadingScene.NextSceneName = "World";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    IEnumerator Progress()
    {
        dark.color = new Color(1, 1, 1, 1); //페이드 인 효과
        for (int i = 0; i < 100; i++)
        {
            dark.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 0);

        for (int i = 0; i<talk.Length; i++) //반복 대사 출력
        {
            text.text = talk[i];
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 0f + (float)j / 10);//텍스트를 투명에서 불투명으로 조금씩 변경
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);//텍스트를 1초간 온전한 색상으로 출력
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 1f - (float)j / 10);//텍스트를 투명으로 조금씩 변경
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);//텍스트가 투명한 상태에서 0.5초 대기를 줘 여운을 남김
        }

        for (int i = 0; i < 100; i++) //페이드아웃 효과
        {
            dark.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 1); 
        yield return new WaitForSeconds(0.5f);
        LoadingScene.NextSceneName = "World"; //월드 씬으로 전환
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
