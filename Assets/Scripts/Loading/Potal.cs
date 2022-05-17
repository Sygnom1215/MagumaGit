using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public enum SCENETYPE
    {
        Main = 0,
        Tuto,
        Fore1,
        Fore2,
        Tree,
        Lake
    };
    [SerializeField]
    private string dirScene; //유니티에서 실제 표시되는 씬의 이름 ex) 0_Tuto
    [SerializeField]
    private SCENETYPE sceneType; //이름 표시 용 Enum 값
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadingManager.LoadScene(dirScene,((int)sceneType));
    }
}
