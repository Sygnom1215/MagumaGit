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
    void Update()
    {
        GetCamaraMoveInput(); //카메라 움직임 W / Up Arrow
        GetMoveInput(); // Player Move
        GetJumpPlatformInput();
    }

    private void GetMoveInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnVelocityChange?.Invoke();
        }
    }
    private void GetJumpPlatformInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            OnJumpPlatform?.Invoke();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
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
}
