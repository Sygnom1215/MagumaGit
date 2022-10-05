using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadContainer : MonoBehaviour
{
    private static bool isFirstLoad = true;
    public static bool IsFirstLoad { get => isFirstLoad; set { isFirstLoad = value; } }
    void Start()
    {
        DontDestroyOnLoad(this);    
    }
}
