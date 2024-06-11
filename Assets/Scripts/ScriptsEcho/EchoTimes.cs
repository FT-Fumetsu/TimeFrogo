using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EchoTimes : MonoBehaviour
{
    [SerializeField] private EchoSpawner _echoSpawner;

    public float _timeBeforeMove = 3f;
    [SerializeField] private float _chrono = 0f;

    private void FixedUpdate()
    {
        _chrono += Time.deltaTime;
    }

    private void Update()
    {
        if (_timeBeforeMove == .5f)
        {
            _chrono = 0f;
            return;
        }
        if (_chrono >= 60f)
        {
            _chrono = 0f;
            _timeBeforeMove -= .25f;
        } 
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            _chrono = 59f;
        }
    }
}
