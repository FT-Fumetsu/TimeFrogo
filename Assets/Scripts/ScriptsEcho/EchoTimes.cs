using System;
using System.Collections.Generic;
using UnityEngine;

public class EchoTimes : MonoBehaviour
{
    /*[SerializeField] private List<float> _durationsPerSteps = new List<float>();
    [SerializeField] private float _durationBeforeNextStep = 4.0f;

    private float _elapsedTime = 0.0f;
    private float _elapsedTimeStep = 0.0f;
    private int _currentStep = 0;

    private Action _onTimerReach = null;
    public event Action OnTimerReach
    {
        add { _onTimerReach += value;}
        remove { _onTimerReach -= value;}
    }

    private void Update()
    {
        ComputeCurrentStepIteration();
        ComputeTimedEvent();
    }

    private void ComputeCurrentStepIteration()
    {
        if(_elapsedTimeStep < _durationBeforeNextStep)
        {
            _elapsedTimeStep += Time.deltaTime;
            return;
        }

        _currentStep++;

        if(_currentStep >= _durationsPerSteps.Count) 
        {
            _currentStep = _durationsPerSteps.Count - 1;
        }
    }

    private void ComputeTimedEvent()
    {
        if (_elapsedTime < _durationsPerSteps[_currentStep])
        {
            _elapsedTime += Time.deltaTime;
            return;
        }

        _elapsedTime = 0.0f;

        _onTimerReach?.Invoke();
    }*/

    [SerializeField] private EchoSpawner _echoSpawner;

    public float _timeBeforeMove = 3f;
    [SerializeField] private float _chrono = 0f;

    private void FixedUpdate()
    {
        _chrono += Time.deltaTime;
    }

    private void Update()
    {
        if (_chrono > 60f && _chrono < 120f)
        {
            _echoSpawner._timeBeforeSpawn = 2.2f;
            _timeBeforeMove = 2.75f;
        }
        if (_chrono > 120f && _chrono < 180f)
        {
            _echoSpawner._timeBeforeSpawn = 2f;
            _timeBeforeMove = 2.5f;
        }
        if (_chrono > 180f && _chrono < 240f)
        {
            _echoSpawner._timeBeforeSpawn = 1.8f;
            _timeBeforeMove = 2.25f;
        }
        if (_chrono > 240f && _chrono < 300f)
        {
            _echoSpawner._timeBeforeSpawn = 1.6f;
            _timeBeforeMove = 2f;
        }
        if (_chrono > 300f && _chrono < 360f)
        {
            _echoSpawner._timeBeforeSpawn = 1.4f;
            _timeBeforeMove = 1.75f;
        }
        if (_chrono > 360f && _chrono < 420f)
        {
            _echoSpawner._timeBeforeSpawn = 1.2f;
            _timeBeforeMove = 1.5f;
        }
        if (_chrono > 420f && _chrono < 480f)
        {
            _echoSpawner._timeBeforeSpawn = 1f;
            _timeBeforeMove = 1.25f;
        }
        if (_chrono > 480f && _chrono < 540f)
        {
            _echoSpawner._timeBeforeSpawn = .8f;
            _timeBeforeMove = 1f;
        }
        if (_chrono > 530f && _chrono < 600f)
        {
            _echoSpawner._timeBeforeSpawn = .7f;
            _timeBeforeMove = .75f;
        }
        if (_chrono > 600f && _chrono < 660f)
        {
            _echoSpawner._timeBeforeSpawn = .5f;
            _timeBeforeMove = .5f;
        }
    }
}
