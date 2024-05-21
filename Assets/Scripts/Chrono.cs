using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chrono : MonoBehaviour
{
    private TextMeshProUGUI _timerText;
    public bool _keepTime;
    float _elapsedTime;

    private void Start()
    {
        _keepTime = true;
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_keepTime == true)
        {
            Timer();
        }
        else if(_keepTime == false)
        {
            Debug.Log("Timer is off");
        }
        int minutes = Mathf.FloorToInt( _elapsedTime / 60 );
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);

        _timerText.SetText("Timer : " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    private void Timer()
    {
        _elapsedTime += Time.deltaTime;
    }
}