using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    [Range(0f,3f)]
    private float nearCheckRangth;
    [SerializeField]
    private LayerMask playerLayer;
    public bool isNear = false;

    private void Update()
    {
        TalkRangeCheck();   
    }
    private void OnDrawGizmos()
    {
        if(isNear)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.position.x + nearCheckRangth, transform.position.y, 0f));
    }
    private void TalkRangeCheck()
    {
        isNear = Physics2D.OverlapBox(transform.position, new Vector2(transform.position.x + nearCheckRangth, transform.position.y + nearCheckRangth), 0f,playerLayer);
    }


}
