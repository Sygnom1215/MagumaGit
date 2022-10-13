using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Idle : MonoBehaviour
{
    //플레이어 위치 반환
    public abstract Vector2 SearchPlayer(Monster owner, float findRange);
}
