using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button lvl2;

    public void PlayBtn()
    {
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            lvl2.interactable = true;
        }
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
