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

    //플레이어 바닥에 닿았는지 판정하는 기즈모
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
        TurnPlayer();
        SmoothFalling();
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
    public void SpeedUp()
    {
        if(movementDataSO.IsCanRunning && !movementDataSO.IsRunning)
        {
            StartCoroutine(SpeedUpIE());
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

    //타일 셀 하나 파괴시켜보는 코드 (예은이가 테스트 한 잔해, 일단 남겨두지만 나중에도 필요 없으면 지우기)
/*    public Tilemap tilemap;
    private void OnCollisionEnter2D(Collision2D _col)
    {

        tilemap = _col.gameObject.GetComponent<Tilemap>();
        this.tilemap.RefreshAllTiles();
        int x, y;
        x = this.tilemap.WorldToCell(_col.transform.position).x;
        y = this.tilemap.WorldToCell(_col.transform.position).y;

        Vector3Int v3Int = new Vector3Int(x, y, 0);

        this.tilemap.SetTileFlags(v3Int, TileFlags.None);
        this.tilemap.SetColor(v3Int, (Color.red));

    }*/

    //자연스러운 움직임을 위해 플레이어 반전
    public void TurnPlayer()
    {
        //turn
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
