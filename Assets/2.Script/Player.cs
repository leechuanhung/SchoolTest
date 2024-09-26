using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;       // 점프 힘
    public float maxSpeed = 6f;
    private bool isGrounded = true;    // 플레이어가 땅에 닿아 있는지 확인
    public float Speed = 5f;    //움직이는 힘
    public GameManager gm;
    public Collider2D col;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;


    Rigidbody2D rb;
    SpriteRenderer SpriteRenderer;
    Animator anim;
    AudioSource audio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "Jumap":
                audio.clip = audioJump;
                break;
            case "Attack":
                audio.clip = audioAttack;
                break;
            case "Damaged":
                audio.clip = audioDamaged;
                break;
            case "Item":
                audio.clip = audioItem;
                break;
            case "Die":
                audio.clip = audioDie;
                break;
            case "Finish":
                audio.clip = audioFinish;
                break;
        }
    }

    private void FixedUpdate()
    {



        //max Speed
        /*if(rb.velocity.x > maxSpeed) //Right Max Speed
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if(rb.velocity.x < maxSpeed*(-1)) //Left Max Speed
            rb.velocity = new Vector2(maxSpeed*(-1), rb.velocity.y);*/
    }

    private void Update()
    {
        bool isJumping = !isGrounded && rb.velocity.y > 0;

        Move();

        //Jump
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded) //Jump
        {
            Jump();
            anim.SetTrigger("Jump");
            PlaySound("Jump");
            //anim.SetBool("isJump", !isGrounded);
        }
        /*if(Input.GetKeyDown(KeyCode.Z))
        {
            transform.Translate(Vector2.up * jumpForce);
            anim.SetTrigger("Jump");
        }*/
    }

    //땅에 닿았는지 확인하기 위해 충돌 처리
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true; //땅에 닿으면 다시 점프 가능
        }
        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if (rb.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
                PlaySound("Attack");
            }
            else
            {
                //Damaged
                OnDamaged(collision.transform.position);
                PlaySound("Damaged");

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            gm.stagePoint += 100;

            //Deactive Item
            collision.gameObject.SetActive(false);
            PlaySound("Item");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            //Next stage
            gm.NextStage();
            PlaySound("Finish");
        }
    }

    void Jump()
    {
        //Rigidbody에 위로 힘을 가해 점프 처리
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; //점프 후에는 땅에 있지 않음
    }


    void Move()
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

    void OnDamaged(Vector2 targetPos)
    {
        //Health Down
        gm.HealthDown();

        gameObject.layer = 11;

        SpriteRenderer.color = new UnityEngine.Color(1, 1, 1, 0.59f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        anim.SetTrigger("Damage");
        Invoke("OffDamaged", 3);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        SpriteRenderer.color = new UnityEngine.Color(1, 1, 1, 1);
    }

    void OnAttack(Transform enemy)
    {
        //Point
        gm.stagePoint += 100;
        //Reaction Force
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        //Enemy Die
        Monster enemyMove = enemy.GetComponent<Monster>();
        enemyMove.OnDamaged();
    }

    public void OnDie()
    {
        //Sprite Alpha
        SpriteRenderer.color = new UnityEngine.Color(1, 1, 1, 0.59f);
        //Sprite Flip Y
        SpriteRenderer.flipY = true;
        // Collider Disable
        col.enabled = false;
        //Die Effect Jump
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

    public void VelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}
