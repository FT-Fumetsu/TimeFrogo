using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedPauseMenu : MonoBehaviour
{
    public GameObject _failedMenu;
    [SerializeField] private GameObject _pauseMenu;

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
        _pauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        _pauseMenu.SetActive(false);
    }
}
