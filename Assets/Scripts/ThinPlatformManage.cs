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
    /// <summary>
    /// 플랫폼의 뚫리는 방향 설정.
    /// </summary>
    /// <param name="value">0 : 정방향 180:역방향</param>    
    public void SetOffset(float value)
    {
        effector.rotationalOffset = value;
    }

}
