using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Decision
{
    NONE,
    PERPECT,
    GOOD,
    EARLY,
    LATE,
    MISS
}
public class DecisionManager : MonoBehaviour
{
    [SerializeField] TMP_Text decision_text;
    // Start is called before the first frame update
    public Decision current_decision { get; private set; }
    Color decisiontext_color;
    Color[] decisionColor = new Color[4] { Color.blue, Color.green, Color.yellow, Color.red };
    public void DecisionUpdate(Decision decision)
    {
        current_decision = decision;
        StartCoroutine(Co_Decision_update(decision));
    }
    IEnumerator Co_Decision_update(Decision decision)
    {
        TextColorChange(decision);
        decision_text.text = decision.ToString();
        decision_text.color = decisiontext_color;
        yield return new WaitForSeconds(0.2f);
        while (decisiontext_color.a > 0)
        {
            decisiontext_color.a -= 0.05f;
            decision_text.color = decisiontext_color;
            yield return null;
        }
    }
    void TextColorChange(Decision _dicision)
    {
        switch (_dicision)
        {
            case Decision.PERPECT:
                decisiontext_color = decisionColor[0];
                break;
            case Decision.GOOD:
                decisiontext_color = decisionColor[1];
                break;
            case Decision.EARLY:
            case Decision.LATE:
                decisiontext_color = decisionColor[2];
                break;
            case Decision.MISS:
                decisiontext_color = decisionColor[3];
                break;
        }
    }
    public void CurrentdecisionInit()
    {
        current_decision = Decision.NONE;
    }

    public void Decision_Init()
    {
        decisiontext_color.a = 0;
        decision_text.color = decisiontext_color;
    }
}
