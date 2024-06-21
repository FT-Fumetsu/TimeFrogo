using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FailedPauseMenu : MonoBehaviour
{
    [SerializeField] private Character _player;
    public GameObject _failedMenu;
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if(_player = null)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        _failedMenu.SetActive(true);
    }
    public void StopGameOver()
    {
        _failedMenu.SetActive(false);
    }
    public void PauseMenu()
    {
        _player._paused = true;
        _pauseMenu.SetActive(true);
    }
    public void UnPause()
    {
        _player._paused = false;
        _pauseMenu.SetActive(false);
    }
}
