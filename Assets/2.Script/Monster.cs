using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Animation anim;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();

        Invoke("Think", 2);
    }

    void Update()
    {

        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 1f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("platform"));
        if(rayHit.collider == null)
        {
            Turn();
        }
    }

    //재귀 함수
    void Think()
    {
        //Set Next Active
        //nextMove = Random.Range(-1, 2);

        //Flip Sprite
        /*if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;*/
        // 스프라이트 플립 대신 회전으로 처리
        if (nextMove != 0)
        {
            float rotationY = (nextMove == 1) ? 0f : 180f; // 0도 또는 180도 회전
            transform.rotation = Quaternion.Euler(0, rotationY, 0); // Y축 기준 회전
        }


        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextMove *= -1;
        //spriteRenderer.flipX = nextMove == 1;
        float rotationY = (nextMove == 1) ? 0f : 180f; // 이동 방향에 따라 회전
        transform.rotation = Quaternion.Euler(0, rotationY, 0); // Y축 기준 회전

        CancelInvoke();
        Invoke("Think", 2);
    }

}
