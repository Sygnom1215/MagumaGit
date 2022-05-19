using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnCameraMoveKeyPress;
    public UnityEvent OnCameraMoveKeyCut;
    public UnityEvent OnVelocityChange;
    public UnityEvent OnJumpPlatform;
    public UnityEvent OnDownPlatform;
    public UnityEvent OnRunningKeyPress;
    public UnityEvent OnDashKeyPress;

    [SerializeField]
    private PlayerAnimation playerAnimation;
    [SerializeField]
    private MovementDataSO movementDataSO;
    void Update()
    {
        GetCamaraMoveInput(); //ī�޶� ������ W / Up Arrow
        GetMoveInput(); // Player Move
        GetJumpPlatformInput(); //���� �÷��� �����ؼ� �ö�
        GetRunningInput(); //�Ϲ� ���� ����
        GetDashInput();
    }

    private void GetMoveInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnVelocityChange?.Invoke();
            if (!movementDataSO._movementData.IsJumping)
            {
                playerAnimation.SetBool(true);
            }
        }
        else
        {
            playerAnimation.SetBool(false);
        }

    }
    private void GetJumpPlatformInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnJumpPlatform?.Invoke();
        }
    }
    /// <summary>
    /// ���� �÷��� ���� ���� �� downŰ�� ������ �÷����� ������
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            OnDownPlatform?.Invoke();
        }
    }
    private void GetCamaraMoveInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            OnCameraMoveKeyPress?.Invoke();
        }
        else
        {
            OnCameraMoveKeyCut?.Invoke();
        }
    }
    private void GetRunningInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnRunningKeyPress?.Invoke();
        }
    }
    private void GetDashInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnDashKeyPress?.Invoke();
        }
    }
}
