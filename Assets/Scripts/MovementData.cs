using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementData : MonoBehaviour
{
    //�ӽ÷� public
    private float gravity = -10f;
    public float speed = 7f;
    private float jumpForce = 8f;
    private float jumpTimeCounter = 1.2f;
    private float jumpTime = 0.35f;
    public int jumpCounter = 2;
    public int playerDir = -1;

    private bool isGrounded = false;
    private bool isJumping = false;
    public bool isDash = false;
    private bool isRunning = false;
    public bool isCanDash = false;  //�뽬��� ������ �����Ѱ�
    public bool isCanRunning = false;

    #region ���Ϳ� ����
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
    public bool IsCanDash { get { return isCanDash; } set { isCanDash = value; } }
    public bool IsCanRunning { get { return isCanRunning; } set { isCanRunning = value; } }
    #endregion
}