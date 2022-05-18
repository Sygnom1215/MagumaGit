using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 nearCheckPos;
    [SerializeField]
    private Vector2 boxSize;
    [SerializeField]
    private LayerMask playerLayer;
    private bool isNear = false;
    private void Check()
    {
        isNear = Physics2D.OverlapBox(nearCheckPos, boxSize, 0f,playerLayer);
    }
}
