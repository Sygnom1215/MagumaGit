using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowArm : MonoBehaviour
{
    private Vector2 dir;
    private void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = transform.position;

        dir = MousePos - bowPos; //방향 계산
        FaceMouse();
    }
    private void FaceMouse()
    {
        transform.right = dir; 
    }
}
