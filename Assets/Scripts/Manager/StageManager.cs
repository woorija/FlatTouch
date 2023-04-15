using System.Collections;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    [SerializeField] protected FatternManager fatternManager;

    [SerializeField] protected Timerbar timerbar;
    [SerializeField] protected BGChanger BGChanger;

    [SerializeField] GameObject PauseUI;
    [SerializeField] protected GameObject ClearUI;
    [SerializeField] TMP_Text Score_text;
    [SerializeField] GameObject GameoverUI;
    public static float fatterntimer { get; protected set; }
    public static int misscount = 0;
    Coroutine Co_clearcheck;
    protected virtual void Start()
    {
        StageInit();
    }
    public virtual void StageInit()
    {
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        ClearUI.SetActive(false);
        GameoverUI.SetActive(false);
        fatterntimer = 3f;
        misscount = 0;
        fatternManager.FatternInit();
        fatternManager.StartFattern();
        BGSetting();
        if (Co_clearcheck!= null)
        {
            StopCoroutine(Co_clearcheck);
        }
        Co_clearcheck = StartCoroutine(ClearCheck());

    }
    protected virtual void Update()
    {
        timecheck();
    }
    protected virtual void timecheck()
    {
        fatterntimer -= Time.deltaTime;
        timerbar.TimerbarUpdate(1 - (fatterntimer / fatternManager.GetTimer()));
        if (fatterntimer <= 0f)
        {
            fatternManager.ChangeFattern();
            fatterntimer += fatternManager.GetTimer();
        }
    }
    protected virtual float LateBGMTime() //싱크 맞추기
    {
        float time = 0;
        switch (GameManager.Instance.currentstage)
        {
            case 1:
                time = -0.25f;
                break;
            case 2:
                time = -0.1f;
                break;
            case 3:
                break;
            case 4:
                time = -0.37f;
                break;
            case 5:
                time = -0.2f;
                break;
            case 6:
                time = -0.16f;
                break;
            case 7:
                time = -0.06f;
                break;

        }
        return time;
    }
    protected virtual void BGMSetting()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayBgm($"Stage{GameManager.Instance.currentstage}BGM");
    }
    protected virtual void BGSetting()
    {
        BGChanger.StageBGChange(GameManager.Instance.currentstage);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.PauseBgm();
        PauseUI.SetActive(true);
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.TouchUnlock();
        AudioManager.Instance.UnpauseBgm();
        PauseUI.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        GameManager.Instance.Get_score(FindObjectOfType<ScoreManager>().GetScore());
        AudioManager.Instance.StopBgm();
        GameoverUI.SetActive(true);
    }
    public void SceneMove_Menu()
    {
        CustomSceneManager.Instance.LoadScene("03_MenuScene");
    }
    IEnumerator ClearCheck()
    {
        yield return YieldCache.WaitForSeconds(fatterntimer + LateBGMTime());
        BGMSetting();
        yield return YieldCache.WaitForSeconds(AudioManager.Instance.GetBGMLength());
        GameClear();
    }
    protected virtual void GameClear()
    {
        Time.timeScale = 0f;
        GameManager.Instance.TouchLock();
        AudioManager.Instance.StopBgm();
        ClearUI.SetActive(true);
        int score = FindObjectOfType<ScoreManager>().GetScore();
        GameManager.Instance.Get_score(score);
        Score_text.text = score.ToString();
    }
}
