using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewObject",menuName = "SO/Object")]
public class ObejctSO : ScriptableObject
{
    public bool isRespawn = false;       //가능 여부 
    public bool isDamage = false;        //가능 여부 
    public bool isHeal = false;          //가능 여부 
    public float respawnTime = 0f;
    public float heal = 0f;
    public float damage = 0f;
}
