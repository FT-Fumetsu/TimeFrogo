using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class HighScore : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private TextMeshProUGUI _textHighscore;

    private float _highscore;

    void Start()
    {
        PlayerPrefs.DeleteKey("Highscore");

        if (PlayerPrefs.HasKey("Highscore"))
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
        if(_playerMove._score >= PlayerPrefs.GetFloat("Highscore"))
        {
            _highscore = _playerMove._score;
            PlayerPrefs.SetFloat("Highscore", _highscore);
        }
        _textHighscore.SetText("Highscore : " + _highscore.ToString());
    }
    public void SetHighscore()
    {
        float highscore = _highscore;
        PlayerPrefs.SetFloat("Highscore", highscore);
    }
    public void LoadHighscore()
    {
        _highscore = PlayerPrefs.GetFloat("Highscore");
        SetHighscore();
    }
}
