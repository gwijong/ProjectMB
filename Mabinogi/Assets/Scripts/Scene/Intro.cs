using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Intro : MonoBehaviour
{
    public string[] talk;
    public Text text;
    public Image dark;
    void Start()
    {
        StartCoroutine(Progress());
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+(0.7f*Time.deltaTime));

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("World");
        }
    }

    IEnumerator Progress()
    {
        dark.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < 100; i++)
        {
            dark.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 0);

        for (int i = 0; i<talk.Length; i++)
        {
            text.text = talk[i];
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 0f + (float)j / 10);
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 1f - (float)j / 10);
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < 100; i++)
        {
            dark.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("World");
    }
}
