using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundData", menuName = "GroundData")]
public class GroundData : ScriptableObject
{
    public GameObject ground;

    [Min(1)]
    public int _maxInSuccesion = 0;
}