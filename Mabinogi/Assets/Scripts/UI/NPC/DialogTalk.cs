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
    public string[] personalstory;

    public Text name;
    public Text textScript;
    public GameObject portrait;
    public GameObject dark;
    public GameObject outline;
    public GameObject[] buttonBackgrounds;
    public GameObject note;
    public GameObject UI_Canvas;

    bool[] progress = { false, false, false, false};

    IEnumerator Cor;
    public void StartTalk()
    {
        if (Cor != null)
        {
            StopCoroutine(Cor);
        }
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
        if(progress[2] == true && Input.anyKeyDown)
        {
            progress[2] = false;
            if (Cor != null)
            {
                StopCoroutine(Cor);
            }
            Cor = PersonalStoryNext();
            StartCoroutine(Cor);
        }
    }

    IEnumerator FirstTyping()
    {
        portrait.SetActive(false);
        dark.SetActive(true);
        outline.SetActive(true);
        yield return null;
        textScript.text = "";
        UI_Canvas.SetActive(false);
        for (int i = 0; i< firstScript.Length; i++)
        {
            textScript.text += firstScript[i];
            yield return new WaitForSeconds(0.05f);
        }
        progress[0] = true;
    }

    IEnumerator ChooseTyping()
    {
        portrait.SetActive(true);
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
    }



    public void SetText(string first, string choose, string note, string shop, string[] personalStory)
    {
        firstScript = first;
        chooseSciript = choose;
        noteScript = note;
        shopScript = shop;
        personalstory = personalStory;
    }

    public void EndTalkButton()
    {
        UI_Canvas.SetActive(true);
        progress[0] = false;
        progress[1] = false;
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "대화를 한다";
        buttonBackgrounds[1].GetComponentInChildren<Text>().text = "거래를 한다";
        buttonBackgrounds[0].SetActive(false);
        buttonBackgrounds[1].SetActive(false);
        portrait.SetActive(false);
        note.SetActive(false);
        dark.SetActive(false);
        outline.SetActive(false);

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

    public void PersonalStoryButton()
    {
        StopCoroutine(Cor);
        Cor = PersonalStory();
        StartCoroutine(Cor);
    }

    IEnumerator PersonalStory()
    {
        note.SetActive(false);
        buttonBackgrounds[0].SetActive(false);
        textScript.text = "";
        for (int i = 0; i < personalstory[0].Length; i++)
        {
            textScript.text += personalstory[0][i];
            yield return new WaitForSeconds(0.05f);
        }
        progress[2] = true;
    }

    IEnumerator PersonalStoryNext()
    {
        note.SetActive(true);
        buttonBackgrounds[0].SetActive(true);
        textScript.text = "";
        for (int i = 0; i < personalstory[1].Length; i++)
        {
            textScript.text += personalstory[1][i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}

