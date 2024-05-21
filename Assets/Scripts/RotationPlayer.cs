using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public void RotationLeft()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.left);
    }
    public void RotationRight()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.right);
    }
    public void RotationUp()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }
    public void RotationDown()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back);
    }
}
