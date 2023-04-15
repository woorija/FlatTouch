using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFattern : Fattern
{
    protected override void Start()
    {
        base.Start();
        fatterntimer = 3f;
    }
    public override void StartFattern()
    {
        scoreManager.Set_text("3");
        ColorChange();
        StartCoroutine(Count());
    }
    public override void ExitFattern()
    {
        scoreManager.Set_text("0");
    }

    public override void ColorChange()
    {
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[i].ColorChange(colorDB.Start_color);
            effectManager.flatEffect[i].PlayEffect();
        }
    }
    void ColorChange(Color _color)
    {
        for (int i = 0; i < flats.flat.Length; i++)
        {
            flats.flat[i].ColorChange(_color);
        }
    }
    public override void AnswerChange()
    {

    }
    public override void InputAnswer(int _num)
    {
    }
    IEnumerator Count()
    {
        yield return YieldCache.WaitForSeconds(1.0f);
        scoreManager.Set_text("2");
        ColorChange(colorDB.Good_color);
        yield return YieldCache.WaitForSeconds(1.0f);
        scoreManager.Set_text("1");
        ColorChange(colorDB.Perfect_color);
    }
}
