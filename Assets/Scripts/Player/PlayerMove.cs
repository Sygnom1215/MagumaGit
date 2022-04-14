using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementDataSO;

    private float moveInput;
    public int jumpCounter = 0;
    private Rigidbody2D rigid;

    [SerializeField]
    private Transform feetPos;
    public float checkRdius;
    public LayerMask whatIsGround;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRdius);
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpCounter = movementDataSO.JumpCounter;
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInput * movementDataSO.Speed, rigid.velocity.y);
    }

    private void Update()
    {
        movementDataSO.IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRdius, whatIsGround);
        JudgmentInput();
        SmoothFalling();
    }
    private void SmoothFalling()
    {
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
        }
        else if (rigid.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * 0.9f * Time.deltaTime;
        }

    }
    //if (isGrounded == true && Input.GetKey(KeyCode.Space))
    //    {
    //        isJumping = true;
    //        jumpTimeCounter = jumpTime;
    //        buttonPressed = JUMP;
    //        rb2d.velocity = Vector2.up * jumpSpeed;
    //    }

    //    if (Input.GetKey(KeyCode.Space) && isJumping == true)
    //    {
    //        if (jumpTimeCounter > 0)
    //        {
    //            rb2d.velocity = Vector2.up * jumpSpeed;
    //            jumpTimeCounter -= Time.deltaTime;
    //        }

    //        else
    //        {
    //            isJumping = false;
    //        }
    //    }
               
    //    if (Input.GetKeyUp(KeyCode.Space))
    //            {
    //                isJumping = false; 
    //            }
    /// <summary>
    ///플레이어가 입력한 값과 현재 상태에 따라서 실행할 행동을 정하는 함수
    /// </summary>
    private void JudgmentInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && movementDataSO.IsGrounded == true && movementDataSO.IsJumping == false)
        {
            movementDataSO.IsJumping = true;
            jumpCounter--;
            movementDataSO.JumpTimeCounter = movementDataSO.JumpTime;
            rigid.velocity = Vector2.up * movementDataSO.JumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && movementDataSO.IsGrounded == true)
        {
        }
        if (Input.GetKey(KeyCode.Space) && movementDataSO.IsJumping == true)
        {
            if (movementDataSO.JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * movementDataSO.JumpForce;
                movementDataSO.JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                movementDataSO.IsJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            movementDataSO.IsJumping = false;
        }
    }
 
    public void MovePlayer()
    {
        //turn
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
}

#region 이전 코드 백업
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//public class PlayerMove : MonoBehaviour
//{
//    public UnityEvent<float> OnVelocityChange; //플레이어 속도가 바뀔 때 실행

//    void FixedUpdate()
//    {
//        moveinput = Input.GetAxisRaw("Horizontal");
//        OnVelocityChange?.Invoke(currentVelocity);
//        rigid.velocity = _movementDir * currentVelocity;
//        //플레이어의 움직임은 방향과 속도의 곱
//    }
//    private void Update()
//    {
//        if(CheckIsGround(isGround) == true && Input.GetKeyDown(KeyCode.Space))
//        {
//            Debug.Log("Jump");
//            currentVelocity *= jumpForce;
//        }

//    }
//    private bool CheckIsGround(bool isGround)
//    {
//        isGround = Physics2D.OverlapCircle(checkTransform.position, checkRadius, groundType);
//        return isGround;
//    }
//    public void MovePlayer(Vector2 inputMovement)
//    {
//        if (inputMovement.sqrMagnitude > 0) //백터의 길이가 0보다 클 때
//        {
//            if(Vector2.Dot(inputMovement,_movementDir) < 0)//들어온 백터와 현재 움직이는 백터를 내적연산이 음수이면
//            {
//                currentVelocity = 0;
//            }
//            _movementDir = inputMovement.normalized;
//        }
//        currentVelocity = CalculateSpeed(inputMovement);
//    }
//    // <summary>
//    // 속도를 계산하는 함수
//    //</summary>
//    // <param name="inputMovement">현재 입력된 움직임</param> 
//    // <returns>범위를 제한한 속도</returns>
//    private float CalculateSpeed(Vector2 inputMovement)
//    {
//        if(inputMovement.sqrMagnitude > 0)
//        {
//            currentVelocity += movementDataSO.acceleration * Time.deltaTime;
//        }
//        else
//        {
//            currentVelocity -= movementDataSO.deAcceleration * Time.deltaTime;
//        }
//        return Mathf.Clamp(currentVelocity, 0, movementDataSO.maxSpeed);
//    }
//    // <summary>
//    // 넉백 구현에 사용 
//    // </summary>
//    public void StopImmediatelly()
//    {
//        currentVelocity = 0;
//        rigid.velocity = Vector2.zero;
//    }
//}
#endregion
