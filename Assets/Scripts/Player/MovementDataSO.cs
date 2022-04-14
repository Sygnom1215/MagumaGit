using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    //임시로 public
    private float speed = 7f;
    private float jumpForce = 8f;
    private float jumpTimeCounter = 1.2f;
    private float jumpTime = 0.35f;
    private int jumpCounter = 1;
    private bool isGrounded;
    private bool isJumping;

    #region 겟터와 셋터
    public float Speed { get { return speed; } set { speed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public float JumpTime { get { return jumpTime; } set { jumpTime = value; } }
    public float JumpTimeCounter { get { return jumpTimeCounter; } set { jumpTimeCounter = value; } }
    public int JumpCounter { get { return jumpCounter; } set { jumpCounter = value; } }

    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    #endregion
}