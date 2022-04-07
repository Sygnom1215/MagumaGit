using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private MovementDataSO movementDataSO;
    [SerializeField]
    private Transform checkTransform;
    [SerializeField]
    private float checkRadius = 0f;
    [SerializeField]
    private LayerMask groundType = 0; //���� ������/�Ϲ� ������/�̼� ������� �ִ� ������ ����
    [SerializeField]
    private float jumpForce = 0f;
    private float currentVelocity = 3;
    private Vector2 _movementDir;
    private bool isGround = false;
    float moveinput;
    public UnityEvent<float> OnVelocityChange; //�÷��̾� �ӵ��� �ٲ� �� ����
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        moveinput = Input.GetAxisRaw("Horizontal");
        OnVelocityChange?.Invoke(currentVelocity);
        rigid.velocity = _movementDir * currentVelocity;
        //�÷��̾��� �������� ����� �ӵ��� ��
    }
    private void Update()
    {
        if(CheckIsGround(isGround) == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            currentVelocity *= jumpForce;
        }
        
    }
    private bool CheckIsGround(bool isGround)
    {
        isGround = Physics2D.OverlapCircle(checkTransform.position, checkRadius, groundType);
        return isGround;
    }
    public void MovePlayer(Vector2 inputMovement)
    {
        if (inputMovement.sqrMagnitude > 0) //������ ���̰� 0���� Ŭ ��
        {
            if(Vector2.Dot(inputMovement,_movementDir) < 0)//���� ���Ϳ� ���� �����̴� ���͸� ���������� �����̸�
            {
                currentVelocity = 0;
            }
            _movementDir = inputMovement.normalized;
        }
        currentVelocity = CalculateSpeed(inputMovement);
    }
    /// <summary>
    /// �ӵ��� ����ϴ� �Լ�
    /// </summary>
    /// <param name="inputMovement">���� �Էµ� ������</param> 
    /// <returns>������ ������ �ӵ�</returns>
    private float CalculateSpeed(Vector2 inputMovement)
    {
        if(inputMovement.sqrMagnitude > 0)
        {
            currentVelocity += movementDataSO.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= movementDataSO.deAcceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0, movementDataSO.maxSpeed);
    }
    /// <summary>
    /// �˹� ������ ��� 
    /// </summary>
    public void StopImmediatelly()
    {
        currentVelocity = 0;
        rigid.velocity = Vector2.zero;
    }
}
