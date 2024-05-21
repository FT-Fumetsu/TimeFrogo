using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI _scoreText;
    //[SerializeField] private TextMeshProUGUI _gameOverText;
    //[SerializeField] private TextMeshProUGUI _highscoreText;
    //[SerializeField] private Button _retryButton;

    //private Character _player;
    //private GroundSpawn _groundSpawn;
    //private VehicleSpawn _vehicleSpawn;
    //private EchoSpawner _echoSpawn;

    //private float _score;

    //private void Start()
    //{
    //    NewGame();
    //}
    //private void Update()
    //{
    //    _score += Time.deltaTime;
    //    _scoreText.text = Mathf.FloorToInt(_score).ToString("D5");
    //}

    //public void NewGame()
    //{
    //    _score = 0;
    //    enabled = true;

    //    _player.gameObject.SetActive(true);
    //    _vehicleSpawn.gameObject.SetActive(true);
    //    _groundSpawn.gameObject.SetActive(true);
    //    _echoSpawn.gameObject.SetActive(true);
    //    _gameOverText.gameObject.SetActive(false);
    //    _retryButton.gameObject.SetActive(false);

    //    UpdateHighscore();
    //}
    //public void GameOver()
    //{
    //    enabled = false;

    //    _player.gameObject.SetActive(false);
    //    _vehicleSpawn.gameObject.SetActive(false);
    //    _groundSpawn.gameObject.SetActive(false);
    //    _echoSpawn.gameObject.SetActive(false);
    //    _gameOverText.gameObject.SetActive(true);
    //    _retryButton.gameObject.SetActive(true);

    //    UpdateHighscore();
    //}
    //private void UpdateHighscore()
    //{
    //    float highscore = PlayerPrefs.GetFloat("highscore", 0);
    //    if(_score >= highscore)
    //    {
    //        highscore = _score;
    //        PlayerPrefs.SetFloat("Highscore = ", highscore);
    //    }

    //}
}
