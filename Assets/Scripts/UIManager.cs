using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPenal;

    private bool isOpenMenu = false;

    void Start()
    {
        menuPenal.SetActive(false);
    }

    void Update()
    {
        // ESC
        if(Input.GetKeyDown(KeyCode.Escape))
            OpenMenu();
    }

    public void OpenMenu()
    {
        if (isOpenMenu == false)
        {
            menuPenal.SetActive(true);
            isOpenMenu = true;
        }
        else if (isOpenMenu == true)
        {
            menuPenal.SetActive(false);
            isOpenMenu = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("TestMapScene");
        Time.timeScale = 1;
    }
}
