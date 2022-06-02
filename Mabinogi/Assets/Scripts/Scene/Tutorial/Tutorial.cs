using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    public Character Player;
    public Character Wolf;
    Vector3 characterPos;
    Vector3 wolfPos;
    public GameObject image;
    public GameObject text;
    public GameObject startText;
    public Sprite[] sprites;
    public Image whiteImage;

    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        characterPos = Player.transform.position;
        wolfPos = Wolf.transform.position;
        StartCoroutine(Progress());
    }

    // Update is called once per frame
    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadingScene.NextSceneName = "Intro";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    IEnumerator Progress()
    {
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(3.0f);
        startText.SetActive(false);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[5];
        text.SetActive(true);
        text.GetComponent<Text>().text = "마비노기의 전투 규칙은 가위바위보와 비슷합니다.";
        yield return new WaitForSeconds(3.0f);
        text.SetActive(false);
        image.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Wolf.Casting(Define.SkillState.Defense);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[0];
        text.SetActive(true);
        text.GetComponent<Text>().text = "디펜스는 기본 공격을 막고 상대방을 멈추게 만듭니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Player.transform.position = characterPos;
        Wolf.transform.position = wolfPos;
        Player.MoveStop(true);
        image.SetActive(false);
        text.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash);
        Wolf.Casting(Define.SkillState.Defense);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[1];
        text.SetActive(true);
        text.GetComponent<Text>().text = "스매시는 디펜스를 깨고 상대방을 다운시킵니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Player.transform.position = characterPos;
        Wolf.transform.position = wolfPos;
        Player.MoveStop(true);
        image.SetActive(false);
        text.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Combat);
        Wolf.Casting(Define.SkillState.Smash);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[2];
        text.SetActive(true);
        text.GetComponent<Text>().text = "스매시는 동작이 커서 기본 공격에 막힙니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        Wolf.SetTarget(Player);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Player.transform.position = characterPos;
        Wolf.transform.position = wolfPos;
        Wolf.MoveStop(true);
        Player.MoveStop(true);
        image.SetActive(false);
        text.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash);
        Wolf.Casting(Define.SkillState.Counter);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[3];
        text.SetActive(true);
        text.GetComponent<Text>().text = "카운터 어택은 근접 공격을 완벽하게 회피 후 반격합니다.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Player.transform.position = characterPos;
        Wolf.transform.position = wolfPos;
        Wolf.MoveStop(true);
        Player.MoveStop(true);
        image.SetActive(false);
        text.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Icebolt);
        Wolf.Casting(Define.SkillState.Counter);
        image.SetActive(true);
        image.GetComponent<Image>().sprite = sprites[4];
        text.SetActive(true);
        text.GetComponent<Text>().text = "그러나 카운터 어택은 움직일 수 없습니다.";
        yield return new WaitForSeconds(3.5f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        LoadingScene.NextSceneName = "Intro";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
