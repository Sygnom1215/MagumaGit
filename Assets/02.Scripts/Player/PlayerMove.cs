using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO movementDataSO;

    public int jumpCounter = 0;
    //��ø� �� �� �ߴ°�
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
    //���� ������ �� fixed update ���
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
        //�ٴڿ� ��Ҵ��� üũ�ϴ� �� ����
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
            movementDataSO._movementData.IsJumping = true;
            jumpCounter--;
            movementDataSO._movementData.JumpTimeCounter = movementDataSO._movementData.JumpTime;
            rigid.velocity = Vector2.up * movementDataSO._movementData.JumpForce;
        }
        //����Ű�� ��� ������ ���̸�
        if (Input.GetKey(KeyCode.Space) && movementDataSO._movementData.IsJumping == true)
        {
            //���� ������ �ð��� �����ִٸ�
            if (movementDataSO._movementData.JumpTimeCounter > 0)
            {
                rigid.velocity = Vector2.up * movementDataSO._movementData.JumpForce;
                movementDataSO._movementData.JumpTimeCounter -= Time.deltaTime;
            }
            //���� ������ �ð��� �������� �ʾҴٸ�
            if (movementDataSO._movementData.JumpTimeCounter <= 0)
            {
                movementDataSO._movementData.IsJumping = false;
            }
        }
        //���� �����߰� ����Ű�� ������ ���� �����̸�(������ �Ѱɷ� üũ�ϸ� ���ܻ�Ȳ �߻���)
        if (movementDataSO._movementData.IsGrounded == true && movementDataSO._movementData.IsJumping == false)
        {
            jumpCounter = movementDataSO._movementData.jumpCounter;
        }
        //����Ű���� ���� ����
        if (Input.GetKeyUp(KeyCode.Space))
        {
            movementDataSO._movementData.IsJumping = false;
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
        Debug.Log("SpeedUp");
        movementDataSO._movementData.Speed += 5f;
        movementDataSO._movementData.IsRunning = true;
        yield return new WaitForSeconds(5f);
        movementDataSO._movementData.Speed = movementDataSO.DefaultSpeed;
        yield return new WaitForSeconds(15f);
        movementDataSO._movementData.IsRunning = false;
    }

    //�ڿ������� �������� ���� �÷��̾� ����
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
        yield return new WaitForSeconds(0.4f); //��Ÿ��
        movementDataSO.IsDashOnce = false;
        movementDataSO._movementData.IsCanDash = true;
    }

   
}
