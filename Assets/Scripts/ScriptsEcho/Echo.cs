using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _player;
    [SerializeField] private Transform _transform = null;
    [SerializeField] private EchoTimes _echoTimes;
    [SerializeField] private MoveEchoSfx _moveEchoSfx;
    [SerializeField] private Animations _animations;

    [Header("Balancing")]
    [SerializeField] private float _movement = 1f;
    [SerializeField] private float _maxRightPosition = 3.0f;
    [SerializeField] private float _maxLeftPosition = -3.0f;
    [SerializeField] private float _echoSpeed = 0;
    [SerializeField] private float _speed = 2;

    [SerializeField] private float _currentPositionx = 0.0f;
    [SerializeField] private float _currentPositionZ = 0.0f;


    private List<Vector3> _exitPlatformPosition = new List<Vector3>();

    private void Start()
    {
        _echoSpeed = 0;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * _echoSpeed * Time.deltaTime);
    }
    public void InvokeEchoMoveUp(Vector3 nextPlayerPos)
    {
        Invoke(nameof(EchoMoveUp), _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveDown(Vector3 nextPlayerPos) 
    {
        Invoke(nameof(EchoMoveDown), _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveLeft(Vector3 nextPlayerPos)
    {
        Invoke(nameof (EchoMoveLeft), _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoMoveRight(Vector3 nextPlayerPos)
    {
        Invoke(nameof(EchoMoveRight), _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoSlideNeg()
    {        
        Invoke(nameof(EchoSlideNeg), _echoTimes._timeBeforeMove);
    }
    public void InvokeEchoStopSlide(Vector3 nextPlayerPos) 
    {
        Invoke(nameof(EchoStopSlide), _echoTimes._timeBeforeMove);
    }

    public void InvokeExitPlatform()
    {
        Invoke(nameof(ExitPlatform), _echoTimes._timeBeforeMove);
    }

    public void InvokeEchoSlide()
    {        
        Invoke(nameof(EchoSlide), _echoTimes._timeBeforeMove);
    }
    public void EchoSlide()
    {
        _echoSpeed = -_speed;
    }
    public void EchoSlideNeg()
    {
        _echoSpeed = _speed;
    }

    public void ExitPlatform()
    {
        Vector3 fixedPosition = _exitPlatformPosition[0];
        _exitPlatformPosition.RemoveAt(0);

        transform.position = fixedPosition;
    }
    public void EchoStopSlide()
    {
        _echoSpeed = 0;

        float xPos = transform.position.x;
        int roundXPos = Mathf.RoundToInt(xPos);
        Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
        transform.position = pos;
        _currentPositionx = roundXPos;

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
        if (_currentPositionx >= _maxRightPosition)
        {
            return;
        }
        _currentPositionx += _movement;
        ApplyXPosition();
    }

    public void EchoMoveLeft()
    {
        if (_currentPositionx <= _maxLeftPosition)
        {
            return;
        }

        _currentPositionx -= _movement;

        ApplyXPosition();
    }

    public void EchoMoveUp()
    {
        _currentPositionZ += _movement;
        Debug.Log("EchoMoveUp");
        ApplyZPosition();
    }

    public void EchoMoveDown()
    {
        _currentPositionZ -= _movement;
        Debug.Log("EchoMoveDown");
        ApplyZPosition();
    }

    private void ApplyXPosition()
    {
        _animations.EchoJump();
        Vector3 newPosition = transform.position;
        newPosition.x = _currentPositionx;
        _transform.position = newPosition;
    }

    private void ApplyZPosition()
    {
        _animations.EchoJump();
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;
        _transform.position = newPosition;
    }

    public void AddExitPlatformPosition(Vector3 position)
    {
        _exitPlatformPosition.Add(position);
    }
}