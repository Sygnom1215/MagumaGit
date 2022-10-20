using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject setPanel;
    [SerializeField]
    private GameObject potal;
    [SerializeField]
    private GameObject titleCanvas;

    private bool isOpenSetsPanel = false;



    void Start()
    {
        titleCanvas.SetActive(true); // Title

        setPanel.SetActive(false); // 설정창
    }

    // Title관련
    public void SetButton()
    {
        if (isOpenSetsPanel == false)
        {
            setPanel.SetActive(true);
            isOpenSetsPanel = true;
        }
    }

    public void QuitButton()
    {
        setPanel.SetActive(false);
        isOpenSetsPanel = false;
    }

    public void ExitGame()
    {
        Debug.Log("GameExit");
        Application.Quit();
    }
}
