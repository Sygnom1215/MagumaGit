using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    [SerializeField]
    private Transform feetPos;
    public float checkRdius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter = 1.2f;
    [Range(0f,3f)]
    [SerializeField]
    private float jumpTime = 0;
    private bool isJumping;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRdius, whatIsGround);

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if ( moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigid.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Space)&&isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }else
            {
                isJumping = false;
            }
        }
    }
}
