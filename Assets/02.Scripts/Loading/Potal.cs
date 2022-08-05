using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    public enum SCENETYPE
    {
        Main = 0,
        Tuto,
        Fore1,
        Fore2,
        Tree,
        Lake,
    };
    public enum STARTTYPE
    {
        test1 = 0,
        test2
    };
    [SerializeField]
    private string dirScene; //유니티에서 실제 표시되는 씬의 이름 ex) 0_Tuto
    [SerializeField]
    private SCENETYPE sceneType; //이름 표시 용 Enum 값

    [SerializeField]
    private LoadingDataSO loadingDataSO;
    [SerializeField]
    private MovementDataSO movementDataSO;
    [SerializeField]
    private STARTTYPE startType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementDataSO.startPos = loadingDataSO.startPos[(int)startType];
        LoadingManager.LoadScene(dirScene, ((int)sceneType));
    }

    public void SelectStage()
    {
        LoadingManager.LoadScene(dirScene, ((int)sceneType));
    }
}
