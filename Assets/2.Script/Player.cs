using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 7f;       // 점프 힘
    public float maxSpeed = 6f;
    private bool isGrounded = true;    // 플레이어가 땅에 닿아 있는지 확인
    public float Speed = 5f;    //움직이는 힘
    Rigidbody2D rb;
    SpriteRenderer SpriteRenderer;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {


        /*if (Input.GetKeyDown(KeyCode.Z) && isGrounded) //Jump
        {
            Jump();
        }

        //max Speed
        if(rb.velocity.x > maxSpeed) //Right Max Speed
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if(rb.velocity.x < maxSpeed*(-1)) //Left Max Speed
            rb.velocity = new Vector2(maxSpeed*(-1), rb.velocity.y);*/
    }

    private void Update()
    {

        //Move Speed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            anim.SetBool("Run", true);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
            anim.SetBool("Run", true);

        }
        else
        {
            anim.SetBool("Run", false);
        }

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }

        //Direction Sprite
        if (Input.GetKey(KeyCode.LeftArrow))
            SpriteRenderer.flipX = true;
        if (Input.GetKey(KeyCode.RightArrow))
            SpriteRenderer.flipX = false;
    }

    //땅에 닿았는지 확인하기 위해 충돌 처리
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true; //땅에 닿으면 다시 점프 가능
        }
    }
    void Jump()
    {
        //Rigidbody에 위로 힘을 가해 점프 처리
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //isGrounded = false; //점프 후에는 땅에 있지 않음
    }*/

}
