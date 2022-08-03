using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new Platform",menuName = "SO/Platform")]
public class Platform :ScriptableObject
{
    public float damage;
    public float healAmount;
    public bool isDamage;
    public bool isHeal;
    public bool isDie;
}