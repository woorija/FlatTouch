using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    Animator _anim;
    BoxCollider2D _collider;
    [SerializeField] Vector3 stand_pos;
    [SerializeField] int index;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider= GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        IndexCheck();
        SelectStandingImage();
    }
    void IndexCheck()
    {
        if (index == 0)
        {
            index = Mathf.Clamp(GameManager.Instance.currentstage, 1, 4);
        }
    }
    void SelectStandingImage()
    {
        Vector2 collider_size = Vector2.zero;
        _anim.SetInteger("NPC", index);
        switch (index)
        {
            case 1:
                collider_size = new Vector2(320f, 740f);
                break;
            case 2:
                collider_size = new Vector2(430f, 605f);
                break;
            case 3:
                collider_size = new Vector2(500f, 700f);
                break;
            case 4:
                collider_size = new Vector2(460f, 640f);
                break;
        }
        _anim.gameObject.transform.position = stand_pos;
        _collider.size = collider_size;
        _collider.transform.position = stand_pos;
        _collider.offset = new Vector2(0, collider_size.y * 0.5f);
    }
}
