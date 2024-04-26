using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawn : MonoBehaviour
{
    [SerializeField] private float _chrono;
    [SerializeField] private float _timer = 5.0f;
    [SerializeField] private GameObject _vehicle;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField]

    //private void Start()
    //{
    //    StartCoroutine(SpawnVehicles());
    //}

    //private IEnumerator SpawnVehicles()
    //{
    //    yield returm new WaitFoSeconds(2);
    //    Instantiate(_vehicle, _spawnPosition.position, Quaternion.identity);
    //}
    private void Update()
    {
        _chrono += Time.deltaTime;
        if (_chrono >= _timer)
        {
            _chrono = 0;
            Instantiate(_vehicle, _spawnPosition.position, Quaternion.identity);
        }
    }
}
