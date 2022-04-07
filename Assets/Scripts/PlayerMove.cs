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
    private LayerMask groundType = 0; //땅이 물인지/일반 땅인지/이속 디버프가 있는 땅인지 구분
    [SerializeField]
    private float jumpForce = 0f;
    private float currentVelocity = 3;
    private Vector2 _movementDir;
    private bool isGround = false;
    float moveinput;
    public UnityEvent<float> OnVelocityChange; //플레이어 속도가 바뀔 때 실행
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
        //플레이어의 움직임은 방향과 속도의 곱
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
        if (inputMovement.sqrMagnitude > 0) //백터의 길이가 0보다 클 때
        {
            if(Vector2.Dot(inputMovement,_movementDir) < 0)//들어온 백터와 현재 움직이는 백터를 내적연산이 음수이면
            {
                currentVelocity = 0;
            }
            _movementDir = inputMovement.normalized;
        }
        currentVelocity = CalculateSpeed(inputMovement);
    }
    /// <summary>
    /// 속도를 계산하는 함수
    /// </summary>
    /// <param name="inputMovement">현재 입력된 움직임</param> 
    /// <returns>범위를 제한한 속도</returns>
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
    /// 넉백 구현에 사용 
    /// </summary>
    public void StopImmediatelly()
    {
        currentVelocity = 0;
        rigid.velocity = Vector2.zero;
    }
}
