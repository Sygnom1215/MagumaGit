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
    public UnityEvent OnTeleportingKeyPress;
    public UnityEvent OnUseItemKeyPress;
    public UnityEvent OnUseOilBarrierKeyPress;
    public UnityEvent OnConversationPress;

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
        GetCamaraMoveInput(); //카메라 움직임 W / Up Arrow
        GetMoveInput(); // Player Move
        GetJumpPlatformInput(); //얇은 플랫폼 점프해서 올라감
        GetRunningInput(); //일반 공격 감지
        GetDashInput();
        GetTeleportInput();
        GetUseItemInput();
        GetUseOilBarrierInput();
        GetConversationInput();
    }

    private void GetConversationInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(playerCon.isCanConversation == true && playerCon.isTalking == false)
            {
                OnConversationPress?.Invoke();
            }
        }
        else if (playerCon.isCanConversation == true && playerCon.isTalking == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                OnConversationPress?.Invoke();
            }
        }
    }

    private void GetUseOilBarrierInput()
    {
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OnUseOilBarrierKeyPress?.Invoke();
            }
        }
    }

    private void GetUseItemInput()
    {
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                OnUseItemKeyPress?.Invoke();
            }
        }
    }

    private void GetMoveInput()
    {
        if (playerCon.isCanConversation == false)
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
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                OnJumpPlatform?.Invoke();
            }
        }
        
    }
    /// <summary>
    /// 얇은 플랫폼 위에 있을 때 down키를 누르면 플랫폼을 내려감
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                OnDownPlatform?.Invoke();
            }
        }
    }
    private void GetCamaraMoveInput()
    {
        if (playerCon.isCanConversation == false)
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
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OnRunningKeyPress?.Invoke();
            }
        }
    }
    private void GetDashInput()
    {
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                OnDashKeyPress?.Invoke();
            }
        }
    }
            
    private void GetTeleportInput()
    {
        if (playerCon.isCanConversation == false)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                OnTeleportingKeyPress?.Invoke();
            }
        }
    }
}
