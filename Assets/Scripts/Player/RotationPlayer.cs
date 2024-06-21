using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private RotationEcho _rotationEcho;
    public void RotationLeft()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.left);
        _rotationEcho.InvokeRotationLeft();
    }
    public void RotationRight()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.right);
        _rotationEcho.InvokeRotationRight();
    }
    public void RotationUp()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.forward);
        _rotationEcho.InvokeRotationUp();
    }
    public void RotationDown()
    {
        _pivot.rotation = Quaternion.LookRotation(Vector3.back);
        _rotationEcho.InvokeRotationDown();
    }
}
