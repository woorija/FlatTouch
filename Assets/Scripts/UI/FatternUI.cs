using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatternUI : MonoBehaviour
{
    [SerializeField] Sprite[] fatternSprites;
    [SerializeField] Sprite DummyfatternSprite;

    [SerializeField] Image CurrentFatternImage;
    [SerializeField] Image NextFatternImage;
    public void SetDummyFatternUI()
    {
        CurrentFatternImage.sprite = DummyfatternSprite;
    }
    public void SetNextFatternUI(int _index)
    {
        NextFatternImage.sprite = fatternSprites[_index];
    }
    public void SetCurrentFatternUI()
    {
        CurrentFatternImage.sprite = NextFatternImage.sprite;
    }
}
