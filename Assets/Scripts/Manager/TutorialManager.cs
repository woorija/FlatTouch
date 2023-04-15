using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : StageManager
{
    [SerializeField] TutorialDialoguePlayer DialoguePlayer;

    public static int tutorial_index;
    public static bool tutorial_play;
    int fattern_index = 0;
    public static bool fattern_clear = false;
    private void Awake()
    {
        GameManager.Instance.ChangeStage(0);
    }
    protected override void Start()
    {
        StageInit();
    }
    protected override void Update()
    {
        IndexCheck();
        switch (tutorial_index)
        {
            case 2:
            case 4:
            case 6:
            case 8:
            case 10:
            case 12:
                base.Update();
                break;
        }
    }
    void IndexCheck()
    {
        if (tutorial_play)
        {
            tutorial_play = false;
            switch (tutorial_index)
            {
                case 0:
                    TutorialStart();
                    GameManager.Instance.TouchLock();
                    break;
                case 1:
                    DialoguePlayer.StartDialogue(0);
                    break;
                case 2:
                    Time.timeScale = 1f;
                    fatternManager.StartFattern();
                    break;
                case 3:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(5);
                    break;
                case 4:
                case 6:
                case 8:
                case 10:
                case 12:
                    GameManager.Instance.TouchUnlock();
                    fattern_clear = false;
                    break;
                case 5:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(10);
                    break;
                case 7:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(15);
                    break;
                case 9:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(21);
                    break;
                case 11:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(27);
                    break;
                case 13:
                    GameManager.Instance.TouchLock();
                    DialoguePlayer.StartDialogue(32);
                    break;
                case 14:
                    TutorialEnd();
                    break;
            }
        }
    }
    protected override void timecheck()
    {
        fatterntimer -= Time.deltaTime;
        timerbar.TimerbarUpdate(1 - (fatterntimer / fatternManager.GetTimer()));
        if (fatterntimer <= 0f)
        {
            misscount = 0;
            if (tutorial_index == 2)
            {
                tutorial_index++;
                fatternManager.ChangeFattern(0);
                fatterntimer += fatternManager.GetTimer();
                tutorial_play = true;
            }
            else {
                if (fattern_clear)
                {
                    fattern_clear = false;
                    tutorial_index++;
                    fatternManager.ChangeFattern(Mathf.Min(++fattern_index, 4));
                    fatterntimer += fatternManager.GetTimer();
                    tutorial_play = true;
                }
                else
                {
                    tutorial_index--;
                    fatternManager.ChangeFattern(fattern_index);
                    fatterntimer += fatternManager.GetTimer();
                    tutorial_play = true;
                }
            }
        }
    }

    public override void StageInit()
    {
        fatterntimer = 3f;
        misscount = 0;
        fatternManager.TutorialFatternInit();
        BGSetting();
        BGMSetting();
        tutorial_index = 0;
        tutorial_play= true;
    }
    public void TutorialStart()
    {
        tutorial_play = true;
        tutorial_index = 1;
    }
    protected override void BGSetting()
    {
        BGChanger.StageBGChange(1);
    }
    protected override void BGMSetting()
    {
        AudioManager.Instance.StopMainBGM();
        AudioManager.Instance.PlayMainBGM();
    }
    void TutorialEnd()
    {
        Time.timeScale = 0f;
        ClearUI.SetActive(true);
    }
    public void SceneMove_Title()
    {
        PlayerPrefs.SetInt("FirstPlay", 1);
        CustomSceneManager.Instance.LoadScene("02_TitleScene");
    }
}
