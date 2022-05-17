using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject setPanel;
    [SerializeField]
    private GameObject potals;
    [SerializeField]
    private GameObject titleCanvas;
    [SerializeField]
    private GameObject canvas;

    private bool isOpenSetsPanel = false;

    void Start()
    {
        titleCanvas.SetActive(true); // Title

        setPanel.SetActive(false); // 설정창
        potals.SetActive(false); // 포탈
        canvas.SetActive(false); // BackButton
    }

    public void PlayButton()
    {
            potals.SetActive(true);
            canvas.SetActive(true);
            
            titleCanvas.SetActive(false);
    }

    public void BackButton()
    {
        canvas.SetActive(false);
        potals.SetActive(false);

        titleCanvas.SetActive(true);
    }
    



    // Title관련
    public void SetButton()
    {
        if(isOpenSetsPanel == false)
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
