using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPress;

    public UnityEvent OnCameraMoveKeyPress;
    public UnityEvent OnCameraMoveKeyCut;

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        GetCamaraMoveInput();
    }

    private void GetMovementInput()
    {
        OnMovementKeyPress?.Invoke(
            new Vector2(Input.GetAxisRaw("Horizontal"),/* Input.GetAxisRaw("Vertical")*/0f)
            );
    }
    private void GetCamaraMoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            OnCameraMoveKeyPress?.Invoke();
        }
        else
        {
            OnCameraMoveKeyCut?.Invoke();
        }
    }
}
