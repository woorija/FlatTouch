using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBFattern : Fattern
{
    [SerializeField] DecisionObject decisionObject;
    protected override void Start()
    {
        base.Start();
        fatterntimer = 60f / StageDB.stage_data[GameManager.Instance.currentstage].bpm;
        answer = new List<int>(3);
        right_answer = new List<int>(3) { 0, 0, 0 };
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
        right_answer[0] = 2;
        right_answer[1] = 3;
        right_answer[2] = 7;
        ColorChange();
    }
    public override void ExitFattern()
    {
        decisionObject.ColorChange(Color.white);
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        colorDB.RGBShuffle();
        int count = 0;
        int colorcount = 1;
        decisionObject.ColorChange(colorDB.RGBColorList(0));
        for (int i = 0; i < flats.flat.Length; i++)
        {
            if (IsRightAnswer(i))
            {
                flats.flat[i].ColorChange(colorDB.RGBColorList(0));
            }
            else
            {
                flats.flat[i].ColorChange(colorDB.RGBColorList(colorcount));
                count++;
                colorcount = (count / 3) + 1;
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
            flats.ChangeAllColor(colorDB.Miss_color);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            TutorialManager.fattern_clear = true;
            PlayRGBFlatEffect();
            if (StageManager.fatterntimer / fatterntimer >= 0.4f)
            {
                SetDecision(Decision.PERPECT);
                flats.ChangeAllColor(colorDB.Perfect_color);
            }
            else if (StageManager.fatterntimer / fatterntimer >= 0.1f)
            {
                SetDecision(Decision.GOOD);
                flats.ChangeAllColor(colorDB.Good_color);
            }
            else
            {
                SetDecision(Decision.LATE);
                flats.ChangeAllColor(colorDB.Miss_color);
            }
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
                scoreManager.IncreaseScore(3);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(2);
                break;
            case Decision.LATE:
                scoreManager.IncreaseScore(0);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
