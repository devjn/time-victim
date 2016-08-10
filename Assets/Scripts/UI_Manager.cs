using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class UI_Manager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
        //Application.LoadLevel("1_game");
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("TestScene");
        GameManager.setStartLevel(level);
    }

    public void LoadMenu()
    {
        SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Intro");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }


}