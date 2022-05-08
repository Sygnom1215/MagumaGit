using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpCounter = movementDataSO.JumpCounter;
        TurnPlayer();
    }
    //물리 판정할 땐 fixed update 사용
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInput * movementDataSO.Speed, rigid.velocity.y);
    }

    private void Update()
    {
        //바닥에 닿았는지 체크하는 원 생성
        movementDataSO.IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRdius, whatIsGround);
        JudgmentInput();
        SmoothFalling();
        TurnPlayer();
    }
    public void SpeedUp()
    {
        if(movementDataSO.IsCanRunning && !movementDataSO.IsRunning)
        {
            StartCoroutine(SpeedUpIE());
        }
    }
    public void Dash()
    {
        if (movementDataSO.IsCanDash && !movementDataSO.IsDash)
        {
            StartCoroutine(DashIE());
        }
    }


    //플레이어 바닥에 닿았는지 판정하는 범위의 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRdius);
    }
    /// <summary>
    ///플레이어가 입력한 값과 현재 상태에 따라서 실행할 행동을 정하는 함수
    /// </summary>
    private void JudgmentInput()
    {
        //만약 점프 키가 눌렸고 점프 가능 횟수가 남아있다면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
        {
            movementDataSO.IsJumping = true;
            jumpCounter--;
            movementDataSO.JumpTimeCounter = movementDataSO.JumpTime;
            rigid.velocity = Vector2.up * movementDataSO.JumpForce;
        }
        //점프키를 계속 누르는 중이면
        if (Input.GetKey(KeyCode.Space) && movementDataSO.IsJumping == true)
        {
            //점프 가능한 시간이 남아있다면
            if (movementDataSO.JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * movementDataSO.JumpForce;
                movementDataSO.JumpTimeCounter -= Time.deltaTime;
            }
            //점프 가능한 시간이 남아있지 않았다면
            if (movementDataSO.JumpTimeCounter <= 0)
            {
                movementDataSO.IsJumping = false;
            }
        }
        //땅에 착지했고 점프키를 누르지 않은 상태이면(착지만 한걸로 체크하면 예외상황 발생함)
        if (movementDataSO.IsGrounded == true && movementDataSO.IsJumping == false)
        {
            jumpCounter = movementDataSO.jumpCounter;
        }
        //점프키에서 손을 떼면
        if (Input.GetKeyUp(KeyCode.Space))
        {
            movementDataSO.IsJumping = false;
        }
    }
    //떨어질 때는 좀 더 강한 중력 적용
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

    private IEnumerator SpeedUpIE()
    {
        float defaultSpeed = movementDataSO.Speed;
        movementDataSO.Speed += 5f;
        movementDataSO.IsRunning = true;
        yield return new WaitForSeconds(5f);
        movementDataSO.Speed = defaultSpeed;
        movementDataSO.IsRunning = false;
    }

    //자연스러운 움직임을 위해 플레이어 반전
    public void TurnPlayer()
    {
        //turn
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            movementDataSO.PlayerDir = 1;
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movementDataSO.PlayerDir = -1;
        }
    }
    private IEnumerator DashIE()
    {
        Debug.Log("doDash");
        movementDataSO.IsDash = true;
        moveInput += 50;
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = Vector2.zero;
        moveInput = 0;
        yield return new WaitForSeconds(0.4f);
        movementDataSO.IsDash = false;
    }
    private void DoDash()
    {
        movementDataSO.IsDash = true;
        Debug.Log("doDash");
        rigid.AddForce(Vector2.right *50f * movementDataSO.PlayerDir, ForceMode2D.Impulse);
        //rigid.velocity = new Vector2(500f * movementDataSO.PlayerDir,0);
    }
}
