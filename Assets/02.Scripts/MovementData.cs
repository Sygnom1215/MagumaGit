using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementData
{
    //�ӽ÷� public
    private float moveInput;
    public float gravity = 10f;
    public float speed = 7f;
    public float jumpForce = 8f;
    public float jumpTimeCounter = 1.2f;
    public  float jumpTime = 0.35f;
    public int jumpCounter = 2;
    public int playerDir = -1;

    public bool isGrounded = false;
    public bool isJumping = false;
    //���� �� �ൿ�� �ϰ� �ִ°�
    public bool isDash = false;
    public bool isRunning = false;
    public bool isDive = false;
    //�� �ൿ�� ����� �� �ִ°�
    public bool isCanDash = false;
    public bool isCanRunning = false;
    public bool isCanDive = false;

    #region ���Ϳ� ����
    public float MoveInput { get => moveInput; set => moveInput = value; }
    public float Gravity { get => gravity; set => gravity = value; }
    public float Speed { get { return speed; } set { speed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public float JumpTime { get { return jumpTime; } set { jumpTime = value; } }
    public float JumpTimeCounter { get { return jumpTimeCounter; } set { jumpTimeCounter = value; } }
    public int JumpCounter { get { return jumpCounter; } set { jumpCounter = value; } }
    public int PlayerDir { get { return playerDir; } set { playerDir = value; } }

    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsDash { get { return isDash; } set { isDash = value; } }
    public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    public bool IsDive { get { return isDive; } set { isDive = value; } }
   

    public bool IsCanDash { get { return isCanDash; } set { isCanDash = value; } }
    public bool IsCanRunning { get { return isCanRunning; } set { isCanRunning = value; } }
    public bool IsCanDive { get { return isCanDive; } set { isCanDive = value; } }
    #endregion
}