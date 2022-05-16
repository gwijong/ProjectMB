using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogTalk : MonoBehaviour
{
    public string firstScript;
    public string chooseSciript;
    public string noteScript;
    public string shopScript;

    public Text name;
    public Text textScript;

    public GameObject[] buttonBackgrounds;
    public GameObject note;

    bool[] progress = { false, false, false, false};

    IEnumerator Cor;

    void OnEnable()
    {
        Cor = FirstTyping();
        StartCoroutine(Cor);
    }

    private void Update()
    {
        if(progress[0] ==true && progress[1] == false && Input.anyKeyDown)
        {
            if (Cor != null)
            {
                StopCoroutine(Cor);
            }
            Cor = ChooseTyping();
            StartCoroutine(Cor);
        }
    }

    IEnumerator FirstTyping()
    {
        yield return null;
        textScript.text = "";
        for (int i = 0; i< firstScript.Length; i++)
        {
            textScript.text += firstScript[i];
            yield return new WaitForSeconds(0.05f);
        }
        progress[0] = true;
    }

    IEnumerator ChooseTyping()
    {
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[1].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화를 한다";
        buttonBackgrounds[1].GetComponentInChildren<Text>().text = "거래를 한다";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(EndTalkButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.RemoveListener(ShopButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.AddListener(ShopButton);
        progress[1] = true;
        yield return null;
        textScript.text = "";
        for (int i = 0; i < chooseSciript.Length; i++)
        {
            textScript.text += chooseSciript[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator NoteTyping()
    {
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[1].SetActive(false);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화 끝내기";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        note.SetActive(true);
        yield return null;
        textScript.text = "";
        for (int i = 0; i < noteScript.Length; i++)
        {
            textScript.text += noteScript[i];
            yield return new WaitForSeconds(0.05f);
        }
        progress[0] = true;
    }

    IEnumerator ShopTyping()
    {
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[1].SetActive(false);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화 끝내기";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        yield return null;
        textScript.text = "";
        for (int i = 0; i < shopScript.Length; i++)
        {
            textScript.text += shopScript[i];
            yield return new WaitForSeconds(0.05f);
        }
        progress[0] = true;
    }



    public void SetText(string first, string choose, string note, string shop)
    {
        firstScript = first;
        chooseSciript = choose;
        noteScript = note;
        shopScript = shop;
}

    public void StartTalk()
    {

    }

    public void EndTalkButton()
    {
        progress[0] = false;
        progress[1] = false;
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화를 한다";
        buttonBackgrounds[1].GetComponentInChildren<Text>().text = "거래를 한다";
        buttonBackgrounds[0].SetActive(false);
        buttonBackgrounds[1].SetActive(false);
        note.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShopButton()
    {
        StopCoroutine(Cor);
        Cor = ShopTyping();
        StartCoroutine(Cor);
    }

    public void NoteButton()
    {
        StopCoroutine(Cor);
        Cor = NoteTyping();
        StartCoroutine(Cor);
    }
}

/*
 * [System.Serializable]
public class Dialogue
{
    public List<string> sentences;
}

public class DialogueSystem : MonoBehaviour
{
    public Text txtSentence;
    public Dialogue info;
    Queue<string> sentences = new Queue<string>();

    private void Start()
    {
        Begin(info);
    }
    //대화 시작
    public void Begin(Dialogue info)
    {
        sentences.Clear();

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }
    //버튼 클릭 시 다음 대화로 넘어감
    public void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }

        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }
    //타이핑 모션 함수
    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
    //대화 끝
    private void End()
    {
        if (sentences != null)
        {
            Debug.Log("end");
        }
    }
}

 * 
 */