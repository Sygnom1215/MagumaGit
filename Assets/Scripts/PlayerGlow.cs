using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlow : MonoBehaviour
{
    [SerializeField]
    private GameObject glowObject;
    public bool isGlow = false;
    private void Update()
    {
        Glow();
    }
    private void Glow()
    {
        glowObject.SetActive(isGlow);
    }
}
