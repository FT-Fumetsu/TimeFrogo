using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "GroundData", menuName = "GroundData")]
public class GroundData : ScriptableObject
{
    public GameObject ground;
    public int _maxInSuccesion = 0;
}