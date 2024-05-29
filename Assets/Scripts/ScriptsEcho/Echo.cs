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
    [SerializeField] private Vehicle _vehicle;
    [SerializeField] private Transform _graphics = null;

    [Header("Balancing")]
    [SerializeField] private float _movement = 1f;
    [SerializeField] private float _maxRightPosition = 3.0f;
    [SerializeField] private float _maxLeftPosition = -3.0f;
    [SerializeField] private float _echoSpeed = 0;
    [SerializeField] private float _speed = 1;

    private float _currentPositionX = 0.0f;
    private float _currentPositionZ = 0.0f;

    private Tween _movementTween = null;

    private List<Vector3> _exitPlatformPosition = new List<Vector3>();

    private void Start()
    {
        _echoSpeed = 0;
        _graphics.SetParent(null);
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
        Debug.Log("EXIT PLATFORM");
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
        _currentPositionX = roundXPos;

        Debug.Log("1");
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

        PlayMovementTween(newPosition);
    }

    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;
        _transform.position = newPosition;

        PlayMovementTween(newPosition);
    }

    public void AddExitPlatformPosition(Vector3 position)
    {
        _exitPlatformPosition.Add(position);
    }
    private void PlayMovementTween(Vector3 newPosition)
    {
        _movementTween?.Kill();
        _graphics.DOMove(newPosition, .1f).SetEase(Ease.InOutBack);
        Transform graphics = _graphics.GetChild(0);
        Sequence s = DOTween.Sequence();
        s.Append(graphics.DOMoveY(2f, .05f).SetEase(Ease.OutQuint));
        s.Append(graphics.DOMoveY(1f, .05f).SetEase(Ease.OutBounce));
        s.Play();
        _movementTween.Play();
    }
}