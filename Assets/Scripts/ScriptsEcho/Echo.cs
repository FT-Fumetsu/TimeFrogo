using System;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _player;
    [SerializeField] private Transform _transform = null;
    [SerializeField] private EchoTimes _echoTimes;
    [SerializeField] private Vehicle _vehicle;

    [Header("Balancing")]
    [SerializeField] private float _movement = 1f;
    [SerializeField] private float _maxRightPosition = 3.0f;
    [SerializeField] private float _maxLeftPosition = -3.0f;
    [SerializeField] private float _echoSpeed = 0;
    [SerializeField] private float _speed = 1;

    private float _currentPositionX = 0.0f;
    private float _currentPositionZ = 0.0f;

    private void Start()
    {
        _echoSpeed = 0;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * _echoSpeed * Time.deltaTime);       
    }
    public void InvokeEchoMoveUp()
    {
        Invoke("EchoMoveUp", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveDown() 
    {
        Invoke("EchoMoveDown", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveLeft()
    {
        Invoke("EchoMoveLeft", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveRight()
    {
        Invoke("EchoMoveRight", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoSlide()
    {
        Invoke("EchoSlide", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoStopSlide() 
    {
        Invoke("EchoStopSlide", _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoSlideNeg()
    {
        Invoke("EchoSlideNeg", _echoTimes._timeBeforeMove);
    }
    public void EchoSlide()
    {
        _echoSpeed = -_speed;
    }
    public void EchoSlideNeg()
    {
        _echoSpeed = _speed;
    }
    public void EchoStopSlide()
    {
        _echoSpeed = 0;

        float xPos = transform.position.x;
        int roundXPos = Mathf.RoundToInt(xPos);
        Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
        //transform.position = pos;
        _currentPositionX = pos.x;
    }

    public void InvokeEchoStopSpeed()
    {
        Invoke("EchoStopSpeed", _echoTimes._timeBeforeMove);
    }

    private void EchoStopSpeed()
    {
        _echoSpeed = 0f;
    }
    public void EchoMoveRight()
    {
        if (_currentPositionX >= _maxRightPosition)
        {
            return;
        }

        _currentPositionX += _movement;

        ApplyXPosition();
    }

    public void EchoMoveLeft()
    {
        if (_currentPositionX <= _maxLeftPosition)
        {
            return;
        }

        _currentPositionX -= _movement;

        ApplyXPosition();
    }

    public void EchoMoveUp()
    {
        _currentPositionZ += _movement;
        ApplyZPosition();
    }

    public void EchoMoveDown()
    {
        _currentPositionZ -= _movement;
        ApplyZPosition();
    }

    private void ApplyXPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = _currentPositionX;

        _transform.position = newPosition;
    }

    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;

        _transform.position = newPosition;
    }
}