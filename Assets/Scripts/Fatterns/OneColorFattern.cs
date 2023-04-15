using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneColorFattern : Fattern
{
    Coroutine Co_touched;
    protected override void Start()
    {
        base.Start();
        fatterntimer = 60f / StageDB.stage_data[GameManager.Instance.currentstage].bpm;
        answer = new List<int>(2);
        right_answer = new List<int>(2) { 0, 0 };
    }
    public override void StartFattern()
    {
        base.StartFattern();
        AnswerChange();
        ColorChange();
    }
    public override void StartFattern_tuto()
    {
        base.StartFattern_tuto();
        answer.Clear();
        right_answer[0] = 0;
        right_answer[1] = 7;
        ColorChange();
    }
    public override void ExitFattern()
    {
        StopTouchcheck();
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        colorDB.Shuffle();
        Color oneColor = colorDB.FlatColorList(0);
        float diff = 25.5f / 255.0f;
        Color collectcolor = new Color(oneColor.r - diff, oneColor.g - diff, oneColor.b - diff);
        for (int i = 0; i < flats.flat.Length; i++)
        {
            if (IsRightAnswer(i))
            {
                flats.flat[i].ColorChange(collectcolor);
            }
            else
            {
                flats.flat[i].ColorChange(oneColor);
            }
        }
    }

    public override void AnswerChange()
    {
        answer.Clear();
        shuffle();
        for (int i = 0; i < right_answer.Count; i++)
        {
            right_answer[i] = random_index[i];
        }
    }
    public override void InputAnswer(int _num)
    {
        bool check = true;
        for (int i = 0; i < answer.Count; i++)
        {
            if (_num == answer[i])
            {
                check = false;
                break;
            }
        }
        if (check) // 중복체크 통과시만 정답에 추가
        {
            if (Co_touched == null)
            {
                Co_touched = StartCoroutine(Co_touchtime());
            }
            answer.Add(_num);
            SortAnswer();
        }
    }
    void SortAnswer()
    {
        if (answer.Count == right_answer.Count)
        {
            answer.Sort();
            right_answer.Sort();
            AnswerCheck();
        }
    }
    void AnswerCheck()
    {
        bool check = true;
        for (int i = 0; i < right_answer.Count; i++)
        {
            if (answer[i] != right_answer[i]) // 정답과 플레이어가 고른 답이 다를경우
            {
                check = false;
                break;
            }
        }
        if (!check)
        {
            StopTouchcheck();
            flats.ChangeAllColor(colorDB.Miss_color);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            TutorialManager.fattern_clear = true;
            PlayAllFlatEffect();
            if (StageManager.fatterntimer / fatterntimer <= 0.25f)
            {
                SetDecision(Decision.PERPECT);
                flats.ChangeAllColor(colorDB.Perfect_color);
            }
            else if (StageManager.fatterntimer / fatterntimer <= 0.5f)
            {
                SetDecision(Decision.GOOD);
                flats.ChangeAllColor(colorDB.Good_color);
            }
            else
            {
                SetDecision(Decision.EARLY);
                flats.ChangeAllColor(colorDB.Miss_color);
            }
            StopTouchcheck();
        }
    }
    IEnumerator Co_touchtime()
    {
        yield return YieldCache.WaitForSeconds(0.2f);
        GameManager.Instance.TouchLock();
        flats.ChangeAllColor(colorDB.Miss_color);
        SetDecision(Decision.MISS);
    }
    void StopTouchcheck()
    {
        if (Co_touched != null)
        {
            StopCoroutine(Co_touched);
            Co_touched = null;
        }
    }
    protected override void SetDecision(Decision _decision)
    {
        base.SetDecision(_decision);
    }
    protected override void SetDecisionScore(Decision _decision)
    {
        switch (_decision)
        {
            case Decision.PERPECT:
                scoreManager.IncreaseScore(4);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(2);
                break;
            case Decision.EARLY:
                scoreManager.IncreaseScore(0);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
