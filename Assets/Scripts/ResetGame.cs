using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void MainMenu()
    {
        Console.WriteLine("menu");
        SceneManager.LoadScene(0);
    }
}
