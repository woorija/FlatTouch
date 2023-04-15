using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class FatternManager : MonoBehaviour
{
    [SerializeField] EffectManager effectManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] FatternUI fatternUI;

    [SerializeField] Fattern[] fatterns;
    [SerializeField] Fattern dummyfattern;
    List<Fattern> usingfattern;
    List<int> usingfatternprobablilty;
    [SerializeField] Flats flats;


    Fattern current_fattern;
    Fattern next_fattern;

    public float fatterntimer { get; private set; }
    private void Awake()
    {
        usingfattern = new List<Fattern>();
        usingfatternprobablilty= new List<int>();
    }
    public void FatternInit()
    {
        effectManager.EffectInit();
        scoreManager.ScoreInit();
        UseFatternSetting();
        FirstFatternSetting();
    }
    public float GetTimer() {
        fatterntimer = current_fattern.GetTimer();
        return fatterntimer;
    }
    void UseFatternSetting() // 스테이지별 사용하는 패턴 세팅해두기
    {
        bool[] usefattern = StageDB.stage_data[GameManager.Instance.currentstage].Usefattern;
        for (int i=0;i<usefattern.Length;i++)
        {
            if (usefattern[i])
            {
                usingfattern.Add(fatterns[i]);
                usingfatternprobablilty.Add(StageDB.probability_data[GameManager.Instance.currentstage].Fattern_probability[i]);
            }
        }
        for(int i = 0; i < usingfattern.Count; i++)
        {
            usingfattern[i].GetManagers();
        }
        dummyfattern.GetManagers();
    }
    void FirstFatternSetting() // 첫패턴은 더미패턴
    {
        current_fattern = dummyfattern;
        fatternUI.SetDummyFatternUI();

        SelectNextFattern();
        fatternUI.SetNextFatternUI(next_fattern.GetIndex());
    }
    public void StartFattern()
    {
        current_fattern.StartFattern();
    }
    public void ChangeFattern() // 더미 이후 매 패턴 변경
    {
        current_fattern.ExitFattern(); // 현재 패턴 종료
        StartCoroutine(ChangeAnimationPlay());

        current_fattern = next_fattern;
        fatternUI.SetCurrentFatternUI();

        SelectNextFattern();
        fatternUI.SetNextFatternUI(next_fattern.GetIndex());

        current_fattern.StartFattern();
    }

    void SelectNextFattern() // 확률에 의거한 다음 패턴 결정 함수
    {
        int _random = Random.Range(0, 100);
        int probablilty = 0;
        for(int i=0;i<usingfattern.Count;i++)
        {
            probablilty += usingfatternprobablilty[i];
            if(_random < probablilty)
            {
                next_fattern = usingfattern[i];
                break;
            }
        }
    }
    IEnumerator ChangeAnimationPlay() // 패턴 변경때마다 전 플랫 애니메이션 플레이
    {
        GameManager.Instance.TouchLock();
        flats.PlayAnimation();
        yield return YieldCache.WaitForSeconds(0.1f);
        GameManager.Instance.TouchUnlock();
    }

    public void InputAnswer(int _num)
    {
        current_fattern.InputAnswer(_num);
    }
    #region tutorial
    public void TutorialFatternInit()
    {
        effectManager.EffectInit();
        scoreManager.ScoreInit();
        UseFatternSetting();
        TutorialFirstFatternSetting();
    }
    void TutorialFirstFatternSetting() // 첫패턴은 더미패턴
    {
        current_fattern = dummyfattern;
        fatternUI.SetDummyFatternUI();

        next_fattern = fatterns[0];
        fatternUI.SetNextFatternUI(next_fattern.GetIndex());
    }
    public void ChangeFattern(int _index)
    {
        current_fattern.ExitFattern(); // 현재 패턴 종료
        flats.PlayAnimation();

        current_fattern = fatterns[_index];
        next_fattern = fatterns[_index];
        fatternUI.SetNextFatternUI(next_fattern.GetIndex());
        fatternUI.SetCurrentFatternUI();
        current_fattern.StartFattern_tuto();
    }


    #endregion
}
