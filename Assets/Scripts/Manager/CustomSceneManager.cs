using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : SingletonBehaviour<CustomSceneManager>
{
    [SerializeField]
    CanvasGroup FadeImage;
    void Start()
    {
        FadeImage.alpha = 0;
        FadeImage.blocksRaycasts = true;
    }
    public void LoadScene(string scenename)
    {
        StartCoroutine(Fade_scene(scenename));
    }

    IEnumerator Fade_In()
    {
        GameManager.Instance.TouchLock();
        FadeImage.blocksRaycasts = true;
        Time.timeScale = 0;
        while (FadeImage.alpha < 1)
        {
            FadeImage.alpha += 0.02f;
            yield return null;
        }
    }

    IEnumerator Fade_Out()
    {
        while (FadeImage.alpha > 0)
        {
            FadeImage.alpha -= 0.02f;
            yield return null;
        }
        Time.timeScale = 1;
        GameManager.Instance.TouchUnlock();
        FadeImage.blocksRaycasts = false;
    }

    IEnumerator Fade_scene(string scenename)
    {
        yield return StartCoroutine(Fade_In());
        SceneManager.LoadScene(scenename);
        ResourceManager.UnloadAsset();
        yield return new WaitForSecondsRealtime(0.5f);
        if (scenename.Equals("04_StageScene"))
        {
            AudioManager.Instance.StopMainBGM();
        }
        else
        {
            AudioManager.Instance.PlayMainBGM();
        }
        yield return StartCoroutine(Fade_Out());
    }

    public IEnumerator Fade_event(IEnumerator temp_coroutine)
    {
        yield return StartCoroutine(Fade_In());
        yield return StartCoroutine(temp_coroutine);
        yield return new WaitForSecondsRealtime(0.5f);
        yield return StartCoroutine(Fade_Out());
    }

    public IEnumerator EXIT_APP()
    {
        yield return StartCoroutine(Fade_In());
        Application.Quit();
    }
}
