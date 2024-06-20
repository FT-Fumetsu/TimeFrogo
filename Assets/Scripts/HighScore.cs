using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;

public class HighScore : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private TextMeshProUGUI _textHighscore;
    [SerializeField] private TextMeshProUGUI _textScore;

    public float _highscore;
    private float _score;

    void Start()
    {
        PlayerPrefs.DeleteKey("_highscore");

        if (PlayerPrefs.HasKey("_highscore"))
        {
            LoadHighscore();
        }
        else
        {
            _highscore = 0f;
        }
    }
    void Update()
    {
        _textHighscore.text = _highscore.ToString();
        if(_score >= PlayerPrefs.GetFloat("_highscore"))
        {
            _highscore = _score;
            PlayerPrefs.SetFloat("_highscore", _highscore);
        }
        _textScore.SetText("Score : " + _score.ToString());
        _textHighscore.SetText("Highscore : " + _highscore.ToString());
    }
    public void AddScore()
    {
        _score++;
    }
    public void SetHighscore()
    {
        PlayerPrefs.SetFloat("_highscore", _highscore);
    }
    public void LoadHighscore()
    {
        _highscore = PlayerPrefs.GetFloat("_highscore");
        SetHighscore();
    }
}
