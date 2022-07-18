using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public void SetBool(bool toggle)
    {
        anim.SetBool("IsWalk",toggle);
    }
    public void SetWalkSpeed(float value)
    {
        anim.SetFloat("Speed", value);
    }
}
