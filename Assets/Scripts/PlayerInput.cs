using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{

    public UnityEvent OnCameraMoveKeyPress;
    public UnityEvent OnCameraMoveKeyCut;
    public UnityEvent OnVelocityChange;
    // Update is called once per frame
    void Update()
    {
        GetCamaraMoveInput();
    }
    private void GetCamaraMoveInput()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            OnVelocityChange?.Invoke();
        }
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
