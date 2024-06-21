using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObstacles : MonoBehaviour
{
    [SerializeField] private Transform[] _spawner;
    [SerializeField] private GameObject[] _obstaclesPrefabs;

    private void Start()
    {
        foreach (Transform spawner in _spawner)
        {
            GameObject randomObstacles = _obstaclesPrefabs[Random.Range(0, _obstaclesPrefabs.Length)];
            Instantiate(randomObstacles, spawner);
        }
    }
}
