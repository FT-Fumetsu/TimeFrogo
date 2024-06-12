using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBindingRefs : MonoBehaviour
{
    public InputActionReference _upRef, _downRef, _leftRef, _rightRef, _pauseRef;

    private void OnEnable()
    {
        _upRef.action.Disable();
        _downRef.action.Disable();
        _leftRef.action.Disable();
        _rightRef.action.Disable();
        _pauseRef.action.Disable();
    }
    private void OnDisable()
    {
        _upRef.action.Enable();
        _downRef.action.Enable();
        _leftRef.action.Enable();
        _rightRef.action.Enable();
        _pauseRef.action.Enable();
    }
}
