using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _echo;
    [SerializeField] private GameObject _echoSfx;
    [SerializeField] private Transform _echoSpawner;

    [Header("Balancing")]
    public float _timeBeforeSpawn = 2.4f;

    private void Start()
    {
        _echoSfx.SetActive(false);
        _echo.SetActive(false);
        InvokeEcho();
        InvokeEchoSfx();
    }

    public void SpawnEcho()
    {
        _echo.SetActive(true);
    }
    public void SpawnEchoSfx()
    {
        _echoSfx.SetActive(true);
    }
    public void DispawnEcho()
    {
        _echo.SetActive(false);
    }
    public void InvokeEcho()
    {
        Invoke(nameof(SpawnEcho), _timeBeforeSpawn);
    }
    public void InvokeEchoSfx()
    {
        Invoke(nameof(SpawnEchoSfx), _timeBeforeSpawn);
    }
}
