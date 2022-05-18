using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject setPanel;

    private bool isOpenSets = false;

    void Start()
    {
        setPanel.SetActive(false);
    }

    void Update()
    {
        
    }
    
    public void SetButton()
    {
        if(isOpenSets == false)
        {
            setPanel.SetActive(true);
            isOpenSets = true;
        }
        else
        {
            return;
        }
    }

    public void QuitButton()
    {
        setPanel.SetActive(false);
        isOpenSets = false;
    }

    public void ExitGame()
    {
        Debug.Log("GameExit");
        Application.Quit();
    }
}
