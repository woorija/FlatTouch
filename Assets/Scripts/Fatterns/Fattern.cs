using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fattern : MonoBehaviour
{
    protected List<int> answer;
    protected List<int> right_answer;
    [SerializeField] protected int fattern_index;

    protected int[] random_index = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // 플랫 갯수만큼 있는 정답 플랫 랜덤을 위한 배열
    [SerializeField] protected FlatColorDB colorDB;
    [SerializeField] protected Flats flats;

    protected ScoreManager scoreManager;
    protected EffectManager effectManager;
    protected DecisionManager decisionManager;

    protected float fatterntimer;
    protected virtual void Start()
    {
    }
    public void GetManagers()
    {
        scoreManager = GetComponentInChildren<ScoreManager>();
        effectManager= GetComponentInChildren<EffectManager>();
        decisionManager= GetComponentInChildren<DecisionManager>();
    }
    protected void shuffle()
    {
        for (int index = 0; index < random_index.Length; index++)
        {
            int random_Index = Random.Range(index, random_index.Length);
            int temp = random_index[index];
            random_index[index] = random_index[random_Index];
            random_index[random_Index] = temp;
        }
    }
    protected virtual bool IsRightAnswer(int _num)
    {
        bool _answer = false;
        for (int i = 0; i < right_answer.Count; i++)
        {
            if (_num == right_answer[i])
            {
                _answer = true;
                break;
            }
        }
        return _answer;
    }
    public virtual void StartFattern() 
    { 
        decisionManager.CurrentdecisionInit();
    }
    public virtual void StartFattern_tuto()
    {
        decisionManager.CurrentdecisionInit();
    }
    public virtual void ExitFattern()
    {
        if (decisionManager.current_decision == Decision.NONE)
        {
            SetDecision(Decision.MISS);
        }
    }
    public virtual void ColorChange() { }
    public virtual void AnswerChange() { }
    public virtual void InputAnswer(int _num) { }
    protected virtual void SetDecision(Decision _decision)
    {
        GameManager.Instance.TouchLock(); // 판정 이후 리셋때까지 터치금지
        decisionManager.DecisionUpdate(_decision);
        SetDecisionScore(_decision);
        switch (_decision)
        {
            case Decision.PERPECT:
                effectManager.Play_perfect_effect();
                break;
            case Decision.GOOD:
                effectManager.Play_good_effect();
                break;
            case Decision.EARLY:
            case Decision.LATE:
                effectManager.Play_miss_effect();
                break;
            case Decision.MISS:
                StageManager.misscount++;
                if(StageManager.misscount >= 10)
                {
                    StageManager stageManager = FindObjectOfType<StageManager>();
                    stageManager.GameOver();
                }
                if (StageManager.misscount >= 3)
                {
                    effectManager.Play_Warning_effect();
                }
                else
                {
                    effectManager.Play_miss_effect();
                }
                break;
        }
    }
    protected virtual void SetDecisionScore(Decision _decision) { }
    public float GetTimer()
    {
        return fatterntimer;
    }
    public int GetIndex()
    {
        return fattern_index;
    }
    protected void PlayAllFlatEffect()
    {
        if (GameManager.Instance.b_toucheffect)
        {
            effectManager.SetEndpos();
            for (int i = 0; i < right_answer.Count; i++)
            {
                effectManager.flatEffect[right_answer[i]].ColorChange(colorDB.FlatColorList(0));
                effectManager.flatEffect[right_answer[i]].SetEndpos(effectManager.GetEndpos());
                effectManager.flatEffect[right_answer[i]].PlayEffect();
            }
        }
    }
    protected void PlayRGBFlatEffect()
    {
        if (GameManager.Instance.b_toucheffect)
        {
            effectManager.SetEndpos();
            for (int i = 0; i < right_answer.Count; i++)
            {
                effectManager.flatEffect[right_answer[i]].ColorChange(colorDB.RGBColorList(0));
                effectManager.flatEffect[right_answer[i]].SetEndpos(effectManager.GetEndpos());
                effectManager.flatEffect[right_answer[i]].PlayEffect();
            }
        }
    }
    protected void PlayFlatEffect(int _index)
    {
        if (GameManager.Instance.b_toucheffect)
        {
            effectManager.SetEndpos();
            effectManager.flatEffect[right_answer[_index]].ColorChange(colorDB.RainbowColorList[_index]);
            effectManager.flatEffect[right_answer[_index]].SetEndpos(effectManager.GetEndpos());
            effectManager.flatEffect[right_answer[_index]].PlayEffect();
        }
    }
}
