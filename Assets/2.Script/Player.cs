using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 7f;       // ���� ��
    public float maxSpeed = 5f;
    private bool isGrounded = true;    // �÷��̾ ���� ��� �ִ��� Ȯ��

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //Move Speed
        float x = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * x, ForceMode2D.Impulse);

        //max Speed
        if(rb.velocity.x > maxSpeed) //Right Max Speed
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if(rb.velocity.x < maxSpeed*(-1)) //Left Max Speed
            rb.velocity = new Vector2(maxSpeed*(-1), rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded) //Jump
        {
            Jump();
        }
    }

    private void Update()
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }
    }

    //���� ��Ҵ��� Ȯ���ϱ� ���� �浹 ó��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true; //���� ������ �ٽ� ���� ����
        }
    }
    void Jump()
    {
        //Rigidbody�� ���� ���� ���� ���� ó��
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; //���� �Ŀ��� ���� ���� ����
    }

}
