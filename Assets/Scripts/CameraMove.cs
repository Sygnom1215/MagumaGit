using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private CinemachineVirtualCamera cm;
    private CinemachineFramingTransposer cmf;
    private void Awake()
    {
        cm = GetComponent<CinemachineVirtualCamera>();
        cmf = cm.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    public void CameraMoveTrue()
    {
        cmf.m_TrackedObjectOffset.y = 4f;
    }
    public void CameraMoveFalse()
    {
        cmf.m_TrackedObjectOffset.y = 0f;
    }
}
