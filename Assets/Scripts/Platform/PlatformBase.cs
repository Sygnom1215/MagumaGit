using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBase : MonoBehaviour
{
    [SerializeField]
    protected Platform platform = null;
    private PlayerHp playerHp;

    //TODO:나중에 FindObjectOfType말고 다른 걸로 찾아오게 수정해야함
    void Start()
    {
        platform = GetComponent<Platform>();
        playerHp = FindObjectOfType<PlayerHp>();
    }
    /// <summary>
    /// 베이스 
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //데미지 입히는 거 필요 없으면 안 받아와도 됨
        if (collision.transform.CompareTag("Player"))
        {
            if (platform.isDamage == true)
            {
                playerHp.HpDecrease(platform.damage);
            }
        }
    }

}