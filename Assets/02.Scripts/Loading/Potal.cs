using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : MonoBehaviour
{
    public enum SCENETYPE
    {
        Main = 0,
        Volcano1,
        Volcano2,
        Volcano3,
        Volcano4,
        Volcano5,
        Fore1,
        CaveL,
        CaveR,
    };
    public enum STARTTYPE
    {
        firstStart = 0,
        Volcano1_1,
        Volcano2_1,
        Volcano2_2,
        Volcano3_1,
        Volcano3_2,
        Volcano4_1,
        Volcano4_2,
        Volcano5_1,
        Volcano5_2,
        Volcano5_3,
        CaveL_1,
        CaveL_2,
        CaveR_1,
        CaveR_2,
    };
    [SerializeField]
    private string dirScene; //����Ƽ���� ���� ǥ�õǴ� ���� �̸� ex) 0_Tuto
    [SerializeField]
    private SCENETYPE sceneType; //�̸� ǥ�� �� Enum ��
    [SerializeField]
    private STARTTYPE startType;
    [Space(15)]
    [SerializeField]
    private LoadingDataSO loadingDataSO;
    [SerializeField]
    private MovementDataSO movementDataSO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movementDataSO.startPos = loadingDataSO.startPos[(int)startType];
            LoadingManager.LoadScene(dirScene, ((int)sceneType));
        }
    }

    public void SelectStage()
    {
        LoadingManager.LoadScene(dirScene, ((int)sceneType));
    }
}
