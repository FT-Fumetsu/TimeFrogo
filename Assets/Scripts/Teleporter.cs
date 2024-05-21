using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject _teleporterA;
    [SerializeField] private GameObject _teleporterB;
    [SerializeField] private Character _player;

    private float _teleporterBPositionZ;
    private float _teleporterBPositionX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag ("TeleporterA"))
        {
            _player.transform.position = _teleporterB.transform.position;
        }
    }
}
