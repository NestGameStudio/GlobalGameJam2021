using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance { get; private set; }

    private void Awake()
    {
        //lida com duplicatas de instancia
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void GoToMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void morte()
    {
        SceneManager.LoadScene("FinalSceneDefeat");
    }
    public void vitoria()
    {
        SceneManager.LoadScene("FinalSceneVictory");
    }
}
