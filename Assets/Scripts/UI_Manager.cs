﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("TestScene");
        //Application.LoadLevel("1_game");
    }
}