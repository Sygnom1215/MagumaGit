using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private GameObject menuPanel;

    private bool isOpenMenu = false;

    private void Awake()
    {
        Instance = this;
    }
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // �ڽ��� �ִ� Scene�� Reroad �Ǵ� �ڵ�� ������ ��.
        Debug.Log("Restart");
        Time.timeScale = 1;
    }


}
