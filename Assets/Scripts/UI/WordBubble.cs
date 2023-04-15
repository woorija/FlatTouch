using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordBubble : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image wordbubble;

    Color SpeechAlpha;
    Color textAlpha = new Color(1, 1, 1, 0);

    Coroutine Co_Speech;

    private void OnEnable()
    {
        SpeechAlpha = textAlpha;
        wordbubble.color = SpeechAlpha;
        text.color = textAlpha;
    }

    public void WordInit(int _stage)
    {
        switch (_stage)
        {
            case 1:
                text.text = "틀렸어";
                break;
            case 2:
                text.text = "힘내";
                break;
            case 3:
                text.text = "화이팅";
                break;
            case 4:
                text.text = "조금만 더";
                break;
            case 5:
                text.text = "메롱!";
                break;
            case 6:
                text.text = "흥!";
                break;
            case 7:
                text.text = "할수있어";
                break;
        }
    }

    public void SpeechStart()
    {
        if(Co_Speech != null) { 
            StopCoroutine(Co_Speech);
        }
        Co_Speech = StartCoroutine(Co_SpeechStart());
    }
    IEnumerator Co_SpeechStart()
    {
        yield return YieldCache.WaitForSeconds(0.1f);
        while(SpeechAlpha.a <= 1f)
        {
            SpeechAlpha.a += 0.05f;
            wordbubble.color = SpeechAlpha;
            text.color = SpeechAlpha - textAlpha;
            yield return null;
        }
        yield return YieldCache.WaitForSeconds(0.3f);
        while (SpeechAlpha.a > 0f)
        {
            SpeechAlpha.a -= 0.06f;
            wordbubble.color = SpeechAlpha;
            text.color = SpeechAlpha - textAlpha;
            yield return null;
        }
    }
}
