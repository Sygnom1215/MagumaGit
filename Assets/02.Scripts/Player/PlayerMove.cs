using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementDataSO;

    public int jumpCounter = 0;
    //대시를 한 번 했는가
    private Rigidbody2D rigid;
    private HpItem hpItem;

    [SerializeField]
    private Transform feetPos;
    public float checkRdius;
    public LayerMask whatIsGround;
    public Rigidbody2D Rigid =>rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        hpItem = GetComponentInChildren<HpItem>();
    }
    private void Start()
    {
        jumpCounter = movementDataSO._movementData.JumpCounter;
        movementDataSO.Gravity = rigid.gravityScale;
        movementDataSO.DefaultSpeed = movementDataSO._movementData.Speed;
        movementDataSO.MoveReset(this);
        TurnPlayer();
    }
    //물리 판정할 땐 fixed update 사용
    private void FixedUpdate()
    {
        if (hpItem.isSliding == true) return;
        movementDataSO._movementData.MoveInput = Input.GetAxisRaw("Horizontal");
        if (movementDataSO._movementData.IsDash && !movementDataSO.IsDashOnce)
        {
            rigid.velocity += Vector2.right * movementDataSO._movementData.PlayerDir * 7f;

        }
        else if (movementDataSO._movementData.MoveInput != 0)
        {
            rigid.velocity = new Vector2(movementDataSO._movementData.MoveInput * movementDataSO._movementData.Speed, rigid.velocity.y);
            PlayerHp.Instance.HpDecrease(Time.deltaTime);
        }
    }

    private void Update()
    {
        if (hpItem.isSliding == true) return;
        //바닥에 닿았는지 체크하는 원 생성
        movementDataSO._movementData.IsGrounded = Physics2D.OverlapCircle(feetPos.position, checkRdius, whatIsGround);
        JudgmentInput();
        SmoothFalling();
        TurnPlayer();
    }
    public void SpeedUp()
    {
        if (movementDataSO._movementData.IsCanRunning && !movementDataSO._movementData.IsRunning)
        {
            StartCoroutine(SpeedUpIE());
        }
    }
    public void Dash()
    {
        if (hpItem.isSliding == true) return;
        if (hpItem.isOilBarrier == true)
        {
            hpItem.isOilBarrier = false;
            hpItem.isSliding = false;
        }
        if (movementDataSO._movementData.IsCanDash && !movementDataSO._movementData.IsDash)
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
            movementDataSO._movementData.IsJumping = true;
            jumpCounter--;
            movementDataSO._movementData.JumpTimeCounter = movementDataSO._movementData.JumpTime;
            rigid.velocity = Vector2.up * movementDataSO._movementData.JumpForce;
        }
        //점프키를 계속 누르는 중이면
        if (Input.GetKey(KeyCode.Space) && movementDataSO._movementData.IsJumping == true)
        {
            //점프 가능한 시간이 남아있다면
            if (movementDataSO._movementData.JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * movementDataSO._movementData.JumpForce;
                movementDataSO._movementData.JumpTimeCounter -= Time.deltaTime;
            }
            //점프 가능한 시간이 남아있지 않았다면
            if (movementDataSO._movementData.JumpTimeCounter <= 0)
            {
                movementDataSO._movementData.IsJumping = false;
            }
        }
        //땅에 착지했고 점프키를 누르지 않은 상태이면(착지만 한걸로 체크하면 예외상황 발생함)
        if (movementDataSO._movementData.IsGrounded == true && movementDataSO._movementData.IsJumping == false)
        {
            jumpCounter = movementDataSO._movementData.jumpCounter;
        }
        //점프키에서 손을 떼면
        if (Input.GetKeyUp(KeyCode.Space))
        {
            movementDataSO._movementData.IsJumping = false;
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
        Debug.Log("SpeedUp");
        movementDataSO._movementData.Speed += 5f;
        movementDataSO._movementData.IsRunning = true;
        yield return new WaitForSeconds(5f);
        movementDataSO._movementData.Speed = movementDataSO.DefaultSpeed;
        yield return new WaitForSeconds(15f);
        movementDataSO._movementData.IsRunning = false;
    }

    //자연스러운 움직임을 위해 플레이어 반전
    public void TurnPlayer()
    {
        //turn
        if (movementDataSO._movementData.MoveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            movementDataSO._movementData.PlayerDir = 1;
        }
        else if (movementDataSO._movementData.MoveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movementDataSO._movementData.PlayerDir = -1;
        }
    }
    private IEnumerator DashIE()
    {
        Debug.Log("doDash");
        rigid.gravityScale = 0;
        movementDataSO._movementData.IsDash = true;
        yield return new WaitForSeconds(0.15f);
        movementDataSO._movementData.IsCanDash = false;

        rigid.velocity = Vector2.zero;
        movementDataSO._movementData.IsDash = false;
        rigid.gravityScale = movementDataSO.Gravity;
        yield return new WaitForSeconds(0.4f); //쿨타임
        movementDataSO.IsDashOnce = false;
        movementDataSO._movementData.IsCanDash = true;
    }

   
}
