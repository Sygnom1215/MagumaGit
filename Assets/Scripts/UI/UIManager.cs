using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;

    private bool isOpenMenu = false;

    void Start()
    {
        menuPanel.SetActive(false);
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
            menuPanel.SetActive(true);
            isOpenMenu = true;
        }
        else if (isOpenMenu == true)
        {
            menuPanel.SetActive(false);
            isOpenMenu = false;
        }
    }

    public void Restart()
    {
        //SceneManager.LoadScene("TestMapScene"); // 자신이 있는 Scene만 Reroad 되는 코드로 변경할 것.
        Debug.Log("Restart");
        Time.timeScale = 1;
    }


}
