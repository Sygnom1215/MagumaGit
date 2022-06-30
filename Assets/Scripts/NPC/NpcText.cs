using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcText : MonoBehaviour
{
    public int npcId;
    public Transform textPos;

    private void Awake()
    {
        textPos = GetComponentInChildren<Transform>();
    }
}
