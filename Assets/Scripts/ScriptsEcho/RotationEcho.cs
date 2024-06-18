using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEcho : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private EchoTimes _echoTimes;
    public void InvokeRotationLeft()
    {
        Invoke(nameof(RotationLeft), _echoTimes._timeBeforeMove);
    }
    public void InvokeRotationRight()
    {
        Invoke(nameof(RotationRight), _echoTimes._timeBeforeMove);
    }
    public void InvokeRotationUp()
    {
        Invoke(nameof(RotationUp), _echoTimes._timeBeforeMove);
    }
    public void InvokeRotationDown()
    {
        Invoke(nameof(RotationDown), _echoTimes._timeBeforeMove);
    }
    public void RotationLeft()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.left);
    }
    public void RotationRight()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.right);
    }
    public void RotationUp()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.forward);
    }
    public void RotationDown()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.back);
    }
}
