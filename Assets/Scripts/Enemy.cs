using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fSpeed = 2.5f; // 기본값. CSV 파일에 작성된 값에 따라 바뀜
    public float fHealth;
    public float fMaxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriter;

    public int iAtk = 10;
    public int iHp = 10;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isLive) return;

        Vector2 dirVec = target.position - rigid.position;
        // 프레임의 영향으로 결과가 달라지지 않도록
        // Time.DeltaTime 아닌 Time.fixedDeltaTime 사용
        Vector2 nextVec = dirVec.normalized * fSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        isLive = true;
        fHealth = fMaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        if (fHealth > 0)
        {
            // Live, Hit Action
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

}
