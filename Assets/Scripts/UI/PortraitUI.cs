using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitUI : MonoBehaviour
{
    [SerializeField] Image portrait_left;
    [SerializeField] Image portrait_right;
    RectTransform portraitL_rt;
    RectTransform portraitR_rt;

    int portrait_number;
    int emote_number;

    [SerializeField] MagicUI magicUI;

    private void Awake()
    {
        portraitL_rt = portrait_left.rectTransform;
        portraitR_rt = portrait_right.rectTransform;
    }
    public void GetPortrait(int _num)
    {
        portrait_number = _num;
    }
    public void GetEmote(int _num)
    {
        emote_number = _num;
    }
    public void PortraitSetting()
    {
        switch (portrait_number)
        {
            case 0:
                portrait_left.gameObject.transform.SetSiblingIndex(2);
                portraitR_rt.gameObject.transform.SetSiblingIndex(0);
                EmoteSetting();
                StartCoroutine(PortraitL_Animation());
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                portrait_left.gameObject.transform.SetSiblingIndex(0);
                portraitR_rt.gameObject.transform.SetSiblingIndex(2);
                EmoteSetting();
                StartCoroutine(PortraitR_Animation());
                break;
            case 5:
                portrait_left.gameObject.transform.SetSiblingIndex(2);
                portraitR_rt.gameObject.transform.SetSiblingIndex(2);
                EmoteSetting();
                StartCoroutine(PortraitL_Animation());
                StartCoroutine(PortraitR_Animation());
                break;
            case 6:
                StartCoroutine(magicUI.MagicDialogue());
                break;
        }
    }
    void EmoteSetting()
    {
        switch (portrait_number)
        {
            case 0: // 주인공
                portraitL_rt.anchoredPosition = new Vector2(-300, -350);
                portraitL_rt.sizeDelta = new Vector2(400, 800);
                portrait_left.sprite = ResourceManager.GetSprite_to_atlas("Emote", $"Player_emote{emote_number + 1}");
                break;
            case 1: // 토끼
                portraitR_rt.anchoredPosition = new Vector2(270, -350);
                portraitR_rt.sizeDelta = new Vector2(360, 830);
                portrait_right.sprite = ResourceManager.GetSprite_to_atlas("Emote", $"Rabbit_emote{emote_number + 1}");
                break;
            case 2: // 너구리
                portraitR_rt.anchoredPosition = new Vector2(250, -370);
                portraitR_rt.sizeDelta = new Vector2(440, 610);
                portrait_right.sprite = ResourceManager.GetSprite_to_atlas("Emote", $"Raccoon_emote{emote_number + 1}");
                break;
            case 3: // 왕
                portraitR_rt.anchoredPosition = new Vector2(270, -300);
                portraitR_rt.sizeDelta = new Vector2(670, 860);
                portrait_right.sprite = ResourceManager.GetSprite_to_atlas("Emote", $"King_emote{emote_number + 1}");
                break;
            case 4: // 마녀
                portraitR_rt.anchoredPosition = new Vector2(250, -310);
                portraitR_rt.sizeDelta = new Vector2(500, 770);
                portrait_right.sprite = ResourceManager.GetSprite_to_atlas("Emote", $"Witch_emote{emote_number + 1}");
                break;
            case 5: //엔딩전용
                portraitL_rt.anchoredPosition = new Vector2(-280, -350);
                portraitL_rt.sizeDelta = new Vector2(330, 820);
                portraitR_rt.anchoredPosition = new Vector2(250, -370);
                portraitR_rt.sizeDelta = new Vector2(440, 610);
                portrait_left.sprite = ResourceManager.GetSprite_to_atlas("Emote", "Rabbit_emote5");
                portrait_right.sprite = ResourceManager.GetSprite_to_atlas("Emote", "Raccoon_emote3");
                break;
        }
    }
    IEnumerator PortraitL_Animation()
    {
        float increase_ypos = 5f;
        int count = 0;
        while (count < 5)
        {
            portraitL_rt.anchoredPosition = new Vector2(portraitL_rt.anchoredPosition.x, portraitL_rt.anchoredPosition.y - increase_ypos);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
        while (count < 10)
        {
            portraitL_rt.anchoredPosition = new Vector2(portraitL_rt.anchoredPosition.x, portraitL_rt.anchoredPosition.y - increase_ypos);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
    }
    IEnumerator PortraitR_Animation()
    {
        float increase_ypos = 5f;
        int count = 0;
        while (count < 5)
        {
            portraitR_rt.anchoredPosition = new Vector2(portraitR_rt.anchoredPosition.x, portraitR_rt.anchoredPosition.y - increase_ypos);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
        while (count < 10)
        {
            portraitR_rt.anchoredPosition = new Vector2(portraitR_rt.anchoredPosition.x, portraitR_rt.anchoredPosition.y - increase_ypos);
            count++;
            yield return YieldCache.WaitForSeconds(0.016f);
        }
    }
}
