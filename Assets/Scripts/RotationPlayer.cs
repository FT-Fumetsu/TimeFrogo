using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    [SerializeField] private RotationEcho _rotationEcho;
    public void RotationLeft()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.left);
        _rotationEcho.InvokeRotationLeft();
    }
    public void RotationRight()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.right);
        _rotationEcho.InvokeRotationRight();
    }
    public void RotationUp()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        _rotationEcho.InvokeRotationUp();
    }
    public void RotationDown()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back);
        _rotationEcho.InvokeRotationDown();
    }
}
