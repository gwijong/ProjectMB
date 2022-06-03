using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    /// <summary> 왼쪽의 인간 캐릭터 </summary>
    public Character Player;
    /// <summary> 오른쪽의 늑대 캐릭터 </summary>
    public Character Wolf;
    /// <summary> 인간 캐릭터의 최초 위치</summary>
    Vector3 characterPos;
    /// <summary> 늑대 캐릭터의 최초 위치 </summary>
    Vector3 wolfPos;
    /// <summary> 우측 위 튜토리얼 설명 이미지 </summary>
    public GameObject image;
    /// <summary> 튜토리얼 설명 텍스트 </summary>
    public GameObject text;
    /// <summary> 화면 최초 "튜토리얼" 이라고 대문짝만하게 쓴 텍스트 </summary>
    public GameObject startText;
    /// <summary> 우측 위 설명 이미지 스프라이트들 </summary>
    public Sprite[] sprites;
    /// <summary> 페이드인 페이드아웃 용 이미지 </summary>
    public Image whiteImage;

    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        characterPos = Player.transform.position; //인간 최초 위치 입력
        wolfPos = Wolf.transform.position; //늑대 최초 위치 입력
        StartCoroutine(Progress()); // 이벤트 진행 코루틴 실행
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESC키 누르면 스킵
        {
            LoadingScene.NextSceneName = "Intro";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    /// <summary> 인간과 늑대를 다시 세팅 </summary>
    void Reset()
    {
        Player.transform.position = characterPos; //원래 위치로 이동
        Wolf.transform.position = wolfPos;//원래 위치로 이동
        Wolf.MoveStop(true); //늑대 이동 정지
        Player.MoveStop(true); //인간 이동 정지
        image.SetActive(false); //전투 설명 이미지 비활성화
        text.SetActive(false); //전투 설명 텍스트 비활성화
    }

    /// <summary> 전투 규칙 설명 이미지와 텍스트 활성화 </summary>
    void ImageActive(int spriteNumber)
    {
        image.SetActive(true); //전투 설명 이미지 활성화
        image.GetComponent<Image>().sprite = sprites[spriteNumber];//전투 설명 이미지의 스프라이트를 세팅
        text.SetActive(true); //전투 설명 텍스트 활성화
    }
    IEnumerator Progress()
    {
        for (int i = 0; i < 100; i++)//화면 전체를 채운 흰색 이미지를 투명으로 점차 변경, 페이드인
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(3.0f);
        startText.SetActive(false);
        ImageActive(5);
        text.GetComponent<Text>().text = "마비노기의 전투 규칙은 가위바위보와 비슷합니다.";
        yield return new WaitForSeconds(3.0f);
        text.SetActive(false);
        image.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Wolf.Casting(Define.SkillState.Defense); //늑대 디펜스 시전
        ImageActive(0);
        text.GetComponent<Text>().text = "디펜스는 기본 공격을 막고 상대방을 멈추게 만듭니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash); //인간 스매시 시전
        Wolf.Casting(Define.SkillState.Defense); //늑대 디펜스 시전
        ImageActive(1);
        text.GetComponent<Text>().text = "스매시는 디펜스를 깨고 상대방을 다운시킵니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Combat); //인간 평타 시전
        Wolf.Casting(Define.SkillState.Smash); //늑대 스매시 시전
        ImageActive(2);
        text.GetComponent<Text>().text = "스매시는 동작이 커서 기본 공격에 막힙니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        Wolf.SetTarget(Player);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash); //인간 스매시 시전
        Wolf.Casting(Define.SkillState.Counter); //늑대 카운터 시전
        ImageActive(3);
        text.GetComponent<Text>().text = "카운터 어택은 근접 공격을 완벽하게 회피 후 반격합니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Icebolt); //인간 아이스볼트 시전
        Wolf.Casting(Define.SkillState.Counter); //늑대 카운터 시전
        ImageActive(4);
        text.GetComponent<Text>().text = "그러나 카운터 어택은 움직일 수 없습니다.";
        yield return new WaitForSeconds(3.5f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 100; i++) //화면을 채운 투명 이미지를 점차 흰색으로 변경, 페이드아웃
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);

        LoadingScene.NextSceneName = "Intro";//인트로 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
