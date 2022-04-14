using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    //�ӽ÷� public
    public float speed = 7f;
    public float jumpForce = 8f;
    public float jumpTimeCounter = 1.2f;
    public float jumpTime = 0.35f;
    public int jumpCounter = 1;
    public bool isGrounded;
    public bool isJumping;

    #region ���Ϳ� ����
    public float Speed { get { return speed; } set { speed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public float JumpTime { get { return jumpTime; } set { jumpTime = value; } }
    public float JumpTimeCounter { get { return jumpTimeCounter; } set { jumpTimeCounter = value; } }
    public int JumpCounter { get { return jumpCounter; } set { jumpCounter = value; } }

    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    #endregion
}