using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _echo;
    [SerializeField] private Transform _echoSpawner;

    [Header("Balancing")]
    public float _timeBeforeSpawn = 2.4f;


    private void Start()
    {
        _echo.SetActive(false);
        //Instantiate(_echo);
        InvokeEcho();
    }

    public void SpawnEcho()
    {
        //Instantiate(_echo);
        _echo.SetActive(true);
    }
    public void DispawnEcho()
    {
        _echo.SetActive(false);
    }
    public void InvokeEcho()
    {
        Invoke("SpawnEcho", _timeBeforeSpawn);
    }
}
