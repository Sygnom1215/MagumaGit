using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPress;
    

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();   
    }

    private void GetMovementInput()
    {
        OnMovementKeyPress?.Invoke(
            new Vector2(Input.GetAxisRaw("Horizontal"),/* Input.GetAxisRaw("Vertical")*/0f)
            );
    }
}
