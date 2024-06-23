using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartQuit : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private FailedPauseMenu _failedPauseMenu;
    [SerializeField] private string _game;
    [SerializeField] private string _mainScene;

    public void RetryStartButton()
    {
        SceneManager.LoadScene(_game, LoadSceneMode.Single);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(_mainScene, LoadSceneMode.Single);
    }
    public void UnPause()
    {
        _player._paused = false;
        _failedPauseMenu.UnPause();
        Time.timeScale = 1.0f;
    }
}

