using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _musicSoundMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private MusicLobby _musicMenu;
    [SerializeField] private string _game;
    [SerializeField] private string _mainScene;
    private void Start()
    {
        _mainMenu.SetActive(true);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _musicSoundMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }
    public void RetryStartButton()
    {
        SceneManager.LoadScene(_game, LoadSceneMode.Single);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(_mainScene, LoadSceneMode.Single);
    }
    public void MainMenuButton()
    {
        _mainMenu.SetActive(true);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _musicSoundMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }
    public void SettingsButton()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _musicSoundMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    public void CreditsButton()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(true);
    }
    public void ControlsButton() 
    {
        _settingsMenu.SetActive(false);
        _controlsMenu.SetActive(true);
    }
    public void MusicSoundButton()
    {
        _settingsMenu.SetActive(false);
        _musicSoundMenu.SetActive(true);
    }
    public void UnPause()
    {
        _player._paused = false;
        Time.timeScale = 1.0f;
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    //[SerializeField] private string _settings;
    //[SerializeField] private string _credits;
    //[SerializeField] private string _mainMenu;
    //[SerializeField] private string _inputMenu;
    //[SerializeField] private string _soundMenu;
    //public void SettingsButton()
    //{
    //    SceneManager.LoadScene(_settings, LoadSceneMode.Single);
    //    DontDestroyOnLoad(_musicMenu._musicLobby);
    //}
    //public void CreditsButton()
    //{
    //    SceneManager.LoadScene(_credits, LoadSceneMode.Single);
    //    DontDestroyOnLoad(_musicMenu._musicLobby);
    //}
    //public void MainMenuButton()
    //{
    //    SceneManager.LoadScene(_mainMenu, LoadSceneMode.Single);
    //    //DontDestroyOnLoad(_musicMenu._musicLobby);
    //}
    //public void ControlsMenuButton()
    //{
    //    SceneManager.LoadScene(_inputMenu, LoadSceneMode.Single);
    //    DontDestroyOnLoad(_musicMenu._musicLobby);
    //}
    //public void SoundButton()
    //{
    //    SceneManager.LoadScene(_soundMenu, LoadSceneMode.Single);
    //    DontDestroyOnLoad(_musicMenu._musicLobby);
    //}
}