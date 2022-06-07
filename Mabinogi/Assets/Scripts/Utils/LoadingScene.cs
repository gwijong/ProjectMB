using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> 씬 로딩 화면 스크립트</summary>
public class LoadingScene : MonoBehaviour
{
    /// <summary> 씬 로딩이 몇프로 되었는지 보여주는 게이지 바 </summary>
    public Image GaugeBar;
    /// <summary> 씬 로딩이 몇프로 되었는지 보여주는 텍스트 </summary>
    public Text text;
    /// <summary> 로드할 씬 이름 </summary>
    public static string NextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Loading");//일단 로딩 코루틴 시작
    }
    /// <summary> 씬 로딩 </summary>
    IEnumerator Loading()
    {
        if (NextSceneName == null)//씬 이름이 없으면
        {
            NextSceneName = "Soulstream";//씬 로드 최초값은 소울스트림
        }
        AsyncOperation oper = SceneManager.LoadSceneAsync(NextSceneName);
        oper.allowSceneActivation = false;

        float timer = 0.01f;
        while (!oper.isDone)//로딩이 덜 됐으면
        {
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;

            if (oper.progress >= 0.9f)//로딩 진행도가 90% 이상인 경우
            {
                GaugeBar.fillAmount = Mathf.Lerp(GaugeBar.fillAmount, 1f, timer); //게이지바의 수치를 부드럽게 늘림
                text.text = (oper.progress * 100f) + 10f + "%";//몇 퍼센트 진행됐는지 텍스트 표시

                if (GaugeBar.fillAmount == 1.0f)
                    oper.allowSceneActivation = true; //장면이 준비된 즉시 장면을 활성화 허용
            }
            else
            {
                text.text = (oper.progress * 100f) + 10f + "%"; //몇 퍼센트 진행됐는지 텍스트 표시
                GaugeBar.fillAmount = Mathf.Lerp(GaugeBar.fillAmount, oper.progress, timer);  //게이지바의 수치를 부드럽게 늘림
                if (GaugeBar.fillAmount >= oper.progress)
                {
                    timer = 0f; //타이머 초기화
                }
            }
        }
    }
}
