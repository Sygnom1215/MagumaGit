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
    public UnityEvent OnUseItemKeyPress;
    public UnityEvent OnUseOilBarrierKeyPress;
    public UnityEvent OnConversationPress;
    public UnityEvent OnShortAttackKeyPress;

    [SerializeField]
    private PlayerAnimation playerAnimation;
    [SerializeField]
    private MovementDataSO movementDataSO;

    private PlayerConversation playerCon;

    private void Awake()
    {
        playerCon = GetComponentInChildren<PlayerConversation>();
    }

    void Update()
    {
        GetCamaraMoveInput(); //ī�޶� ������ W / Up Arrow
        GetMoveInput(); // Player Move
        GetJumpPlatformInput(); //���� �÷��� �����ؼ� �ö�
        GetRunningInput(); //�Ϲ� ���� ����
        GetDashInput();
        GetUseItemInput();
        GetUseOilBarrierInput();
        GetConversationInput();
        GetShortAttackInput();
    }

    private void GetConversationInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerCon.isCanConversation == true && playerCon.isTalking == false)
            {
                OnConversationPress?.Invoke();
            }
        }
        else if (playerCon.isCanConversation == true && playerCon.isTalking == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnConversationPress?.Invoke();
            }
        }
    }

    private void GetUseOilBarrierInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OnUseOilBarrierKeyPress?.Invoke();
            }
        }
    }

    private void GetUseItemInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                OnUseItemKeyPress?.Invoke();
            }
        }
    }

    private void GetMoveInput()
    {
        if (playerCon.isTalking == false)
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
    }
    private void GetJumpPlatformInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                OnJumpPlatform?.Invoke();
            }
        }

    }
    /// <summary>
    /// ���� �÷��� ���� ���� �� downŰ�� ������ �÷����� ������
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                OnDownPlatform?.Invoke();
            }
        }
    }
    private void GetCamaraMoveInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                OnCameraMoveKeyPress?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                OnCameraMoveKeyCut?.Invoke();
            }
        }
    }
    private void GetRunningInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnRunningKeyPress?.Invoke();
            }
        }
    }
    private void GetDashInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                OnDashKeyPress?.Invoke();
            }
        }
    }

    private void GetShortAttackInput()
    {
        if (playerCon.isTalking == false)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                OnShortAttackKeyPress?.Invoke();
            }
        }
    }
}