using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private GameObject menuPenal;

    private bool isOpenMenu = false;

    private void Awake()
    {
        Instance = this;
    }
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // 자신이 있는 Scene만 Reroad 되는 코드로 변경할 것.
        Debug.Log("Restart");
        Time.timeScale = 1;
    }
}
