using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinPlatformManage : MonoBehaviour
{
    private PlatformEffector2D effector;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }
   
    public void SetOffset(float value)
    {
            effector.rotationalOffset = value;
    }

}
