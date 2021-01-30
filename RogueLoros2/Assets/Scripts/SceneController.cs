﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToMenu() {
        SceneManager.LoadScene("MenuScene");
    }
    public void GoToEndScene()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void GoToGame() {
        SceneManager.LoadScene("GameScene");
    }
}
