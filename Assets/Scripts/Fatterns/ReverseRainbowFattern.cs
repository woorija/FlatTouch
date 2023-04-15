using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRainbowFattern : Fattern
{
    [SerializeField] DecisionObject decisionObject;
    int count;
    protected override void Start()
    {
        base.Start();
        fatterntimer = 120f / StageDB.stage_data[GameManager.Instance.currentstage].bpm;
        answer = new List<int>(7);
        right_answer = new List<int>(7) { 0, 1, 2, 3, 4, 5, 6 };
    }

    public override void StartFattern()
    {
        count = 6;
        base.StartFattern();
        AnswerChange();
        ColorChange();
    }
    public override void StartFattern_tuto()
    {
        count = 6;
        base.StartFattern_tuto();
        answer.Clear();
        random_index[0] = right_answer[0] = 1;
        random_index[1] = right_answer[1] = 5;
        random_index[2] = right_answer[2] = 7;
        random_index[3] = right_answer[3] = 8;
        random_index[4] = right_answer[4] = 4;
        random_index[5] = right_answer[5] = 0;
        random_index[6] = right_answer[6] = 2;
        random_index[7] = 6;
        random_index[8] = 3;
        ColorChange();
    }
    public override void ExitFattern()
    {
        decisionObject.ColorChange(Color.white);
        base.ExitFattern();
    }
    public override void ColorChange()
    {
        decisionObject.ColorChange(colorDB.RainbowColorList[count]);
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[random_index[i]].ColorChange(colorDB.RainbowColorList[i]);
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
            if (answer.Count >= 0)
            {
                answer.Add(_num);
                AnswerCheck();
                count--;
            }
        }
    }
    void AnswerCheck()
    {
        bool check = true;
        if (answer[6 - count] != right_answer[count])
        {
            check= false;
        }
        if (!check)
        {
            flats.ChangeAllColor(colorDB.Miss_color);
            SetDecision(Decision.MISS);
        }
        else // 정답인 경우
        {
            PlayFlatEffect(count);
            if (count == 0)
            {
                TutorialManager.fattern_clear = true;
                if (StageManager.fatterntimer / fatterntimer >= 0.2f)
                {
                    SetDecision(Decision.PERPECT);
                    flats.ChangeAllColor(colorDB.Perfect_color);
                }
                else if (StageManager.fatterntimer / fatterntimer >= 0.05f)
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
            else
            {
                decisionObject.ColorChange(colorDB.RainbowColorList[count-1]);
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
                scoreManager.IncreaseScore(5);
                break;
            case Decision.GOOD:
                scoreManager.IncreaseScore(3);
                break;
            case Decision.LATE:
                scoreManager.IncreaseScore(1);
                break;
            case Decision.MISS:
                scoreManager.IncreaseScore(-3);
                break;
        }
    }
}
