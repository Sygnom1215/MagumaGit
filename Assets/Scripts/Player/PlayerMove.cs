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
    //���� ������ �� fixed update ���
    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInput * movementDataSO.Speed, rigid.velocity.y);
    }

    private void Update()
    {
        //�ٴڿ� ��Ҵ��� üũ�ϴ� �� ����
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


    //�÷��̾� �ٴڿ� ��Ҵ��� �����ϴ� ������ �����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRdius);
    }
    /// <summary>
    ///�÷��̾ �Է��� ���� ���� ���¿� ���� ������ �ൿ�� ���ϴ� �Լ�
    /// </summary>
    private void JudgmentInput()
    {
        //���� ���� Ű�� ���Ȱ� ���� ���� Ƚ���� �����ִٸ�
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
        {
            movementDataSO.IsJumping = true;
            jumpCounter--;
            movementDataSO.JumpTimeCounter = movementDataSO.JumpTime;
            rigid.velocity = Vector2.up * movementDataSO.JumpForce;
        }
        //����Ű�� ��� ������ ���̸�
        if (Input.GetKey(KeyCode.Space) && movementDataSO.IsJumping == true)
        {
            //���� ������ �ð��� �����ִٸ�
            if (movementDataSO.JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * movementDataSO.JumpForce;
                movementDataSO.JumpTimeCounter -= Time.deltaTime;
            }
            //���� ������ �ð��� �������� �ʾҴٸ�
            if (movementDataSO.JumpTimeCounter <= 0)
            {
                movementDataSO.IsJumping = false;
            }
        }
        //���� �����߰� ����Ű�� ������ ���� �����̸�(������ �Ѱɷ� üũ�ϸ� ���ܻ�Ȳ �߻���)
        if (movementDataSO.IsGrounded == true && movementDataSO.IsJumping == false)
        {
            jumpCounter = movementDataSO.jumpCounter;
        }
        //����Ű���� ���� ����
        if (Input.GetKeyUp(KeyCode.Space))
        {
            movementDataSO.IsJumping = false;
        }
    }
    //������ ���� �� �� ���� �߷� ����
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

    //�ڿ������� �������� ���� �÷��̾� ����
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
