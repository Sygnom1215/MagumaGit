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
    private string dirScene; //����Ƽ���� ���� ǥ�õǴ� ���� �̸� ex) 0_Tuto
    [SerializeField]
    private SCENETYPE sceneType; //�̸� ǥ�� �� Enum ��
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadingManager.LoadScene(dirScene,((int)sceneType));
    }
}
