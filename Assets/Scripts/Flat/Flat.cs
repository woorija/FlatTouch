using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Flat : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] int flat_number;
    Image flat_image;
    RectTransform flat_size;
    Animation _anim;
    FatternManager fatternManager;


    Coroutine check;
    public int Get_flatnumber()
    {
        return flat_number;
    }
    public void SetFatternManager(FatternManager _fatternManager)
    {
        fatternManager = _fatternManager;
    }
    private void Awake()
    {
        flat_image = transform.GetChild(0).GetComponent<Image>();
        flat_size = GetComponent<RectTransform>();
        _anim = GetComponent<Animation>();
    }
    public void ColorChange(Color _color)
    {
        flat_image.color = _color;
    }
    public void PlayChangeAnimation()
    {
        _anim.Play("Change");
    }

    public void OnPointerDown(PointerEventData eventData) // 패턴 매니저에 더해줄것
    {
        FlatTouch();
    }

    public void FlatTouch()
    {
        if (GameManager.Instance.b_touchable)
        {
            if (check == null)
            {
                check = StartCoroutine(Co_Flattouch());
            }
            fatternManager.InputAnswer(flat_number);
        }
    }
    IEnumerator Co_Flattouch()
    {
        flat_size.localScale = Vector3.one;
        while (flat_size.localScale.x < 1.3f)
        {
            flat_size.localScale *= 1.05f;
            yield return null;
        }
        while (flat_size.localScale.x > 1.0f)
        {
            flat_size.localScale *= 0.95f;
            yield return null;
        }
        flat_size.localScale = Vector3.one;
        check = null;
    }
}
