using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteAnimate : MonoBehaviour
{
    [SerializeField] Image image;

    Sprite[] Emote_sprite; // P, G, M
    RectTransform emote_transform; // 중심맞추기 용도
    Vector3 emote_position;
    Vector2 emote_size;
    float stop_pos;
    float emote2_stop_pos;
    float x_pos;

    Color EmoteAlpha;

    Coroutine Co_Anim;
    Coroutine Co_Alpha;


    private void Awake()
    {
        Emote_sprite = new Sprite[3];
        emote_transform = image.GetComponent<RectTransform>();
    }
    public void EmoteInit(int _stage)
    {
        Emote_sprite[0] = ResourceManager.GetSprite_to_atlas("Emote", "Player_emote6");
        Emote_sprite[1] = ResourceManager.GetSprite_to_atlas("Emote", "Player_emote4");
        switch (_stage)
        {
            case 1:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Player_emote4");
                x_pos = 0f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(350f, 705f);
                break;
            case 2:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Rabbit_emote4");
                x_pos = 0f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(328f, 755f);
                break;
            case 3:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Raccoon_emote1");
                x_pos = -48f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(410f, 550f);
                break;
            case 4:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "King_emote1");
                x_pos = -16f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(560f, 700f);
                break;
            case 5:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Witch_emote1");
                x_pos = -48f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(500f, 760f);
                break;
            case 6:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Witch_emote8");
                x_pos = -48f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(500f, 760f);
                break;
            case 7:
                Emote_sprite[2] = ResourceManager.GetSprite_to_atlas("Emote", "Witch_emote4");
                x_pos = -8f;
                emote2_stop_pos = -200f;
                emote_size = new Vector2(460f, 770f);
                break;
        }
    }

    public void EmoteSetting(int _num)
    {
        image.sprite = Emote_sprite[_num];
        switch (_num)
        {
            case 0:
            case 1:
                emote_position = new Vector3(0f, -600f, 0f);
                emote_transform.sizeDelta = new Vector2(350f, 705f);
                stop_pos = -200f;
                break;
            case 2:
                emote_position = new Vector3(x_pos, -600f, 0f);
                emote_transform.sizeDelta = emote_size;
                stop_pos = emote2_stop_pos;
                break;
        }
        EmoteAlpha = Color.white;
        emote_transform.anchoredPosition= emote_position;
        image.color = EmoteAlpha;
    }

    public void Start_emoteanimation()
    {
        if (Co_Alpha != null)
        {
            StopCoroutine(Co_Alpha);
        }
        if (Co_Anim != null)
        {
            StopCoroutine(Co_Anim);
        }
        Co_Anim = StartCoroutine(Co_EmoteAnimate());
        Co_Alpha = StartCoroutine(Co_EmoteAlphaAnimate());
    }

    IEnumerator Co_EmoteAnimate()
    {
        while (emote_position.y < stop_pos)
        {
            emote_position.y *= 0.7f;
            emote_transform.anchoredPosition = emote_position;
            yield return null;
        }
        emote_position.y = stop_pos;
        emote_transform.anchoredPosition = emote_position;
    }

    IEnumerator Co_EmoteAlphaAnimate()
    {
        yield return YieldCache.WaitForSeconds(0.5f);
        while (EmoteAlpha.a > 0)
        {
            EmoteAlpha.a -= 0.04f;
            image.color = EmoteAlpha;
            yield return null;
        }
    }
}
