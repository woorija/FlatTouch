using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectFade : MonoBehaviour
{
    Image image;
    TMP_Text text;
    bool isIncrease;
    float alpha;
    Color Fade_color;

    float variation;
    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
        alpha = 1f;
        variation = 0.03f;
        isIncrease = false;
        Fade_color = Color.red;
    }
    private void Update()
    {
        Fading();
    }
    void Fading()
    {
        if (isIncrease)
        {
            IncreaseFade();
        }
        else
        {
            decreaseFade();
        }
        Fade_color.a = alpha;
        if(image != null) image.color = Fade_color;
        if(text!=null) text.color = Fade_color;
    }
    void IncreaseFade()
    {
        alpha += variation;
        if (alpha > 1f)
        {
            alpha = 1f;
            isIncrease = false;
        }
    }
    void decreaseFade()
    {
        alpha -= variation;
        if (alpha < 0f)
        {
            alpha = 0f;
            isIncrease = true;
        }
    }
}
