using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Animation anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D colli;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colli = GetComponent<BoxCollider2D>();
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

    //��� �Լ�
    void Think()
    {
        //Set Next Active
        //nextMove = Random.Range(-1, 2);

        //Flip Sprite
        /*if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;*/
        // ��������Ʈ �ø� ��� ȸ������ ó��
        if (nextMove != 0)
        {
            float rotationY = (nextMove == 1) ? 0f : 180f; // 0�� �Ǵ� 180�� ȸ��
            transform.rotation = Quaternion.Euler(0, rotationY, 0); // Y�� ���� ȸ��
        }


        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextMove *= -1;
        //spriteRenderer.flipX = nextMove == 1;
        float rotationY = (nextMove == 1) ? 0f : 180f; // �̵� ���⿡ ���� ȸ��
        transform.rotation = Quaternion.Euler(0, rotationY, 0); // Y�� ���� ȸ��

        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.59f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        // Collider Disable
        colli.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy
        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

}
