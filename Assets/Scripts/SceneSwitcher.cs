using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
