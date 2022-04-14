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

    void Update()
    {
        GetCamaraMoveInput(); //ī�޶� ������ W / Up Arrow
        GetMoveInput(); // Player Move
    }

    private void GetMoveInput()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            OnVelocityChange?.Invoke();
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
