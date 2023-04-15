using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spr_renderer;
    Animator anim;
    int speed;

    private void Awake()
    {
        speed = 700;
        rigid = GetComponent<Rigidbody2D>();
        spr_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public void Move_Left()
    {
        spr_renderer.flipX = true;
        rigid.velocity = Vector2.left * speed;
    }
    public void Move_Right()
    {
        spr_renderer.flipX = false;
        rigid.velocity = Vector2.right * speed;
    }
    public void Stop_Move()
    {
        rigid.velocity = Vector2.zero;
    }
    private void Update()
    {
        Player_animationCheck();
    }

    void Player_animationCheck()
    {
        if (rigid.velocity.Equals(Vector2.zero))
        {
            anim.SetBool("IsWalk", false);
        }
        else
        {
            anim.SetBool("IsWalk", true);
        }
    }
}
