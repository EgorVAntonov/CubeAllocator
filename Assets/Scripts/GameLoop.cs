using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public delegate void GameFinished();
    public event GameFinished OnGameGinished;

    public void SetGameOver()
    {
        Debug.Log("game over");
        FindObjectOfType<SceneSwitcher>().SetMenuScene();
    }
}
