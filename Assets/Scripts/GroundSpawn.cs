using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    private List<GameObject> _currentGround = new List<GameObject>();
    [SerializeField] private List<GroundData> _groundDatas = new List<GroundData>();
    public Vector3 _currentPosition = new Vector3(0, 0, 0);
    [SerializeField] private int _maxGroundCount = 0;
    [SerializeField] private int _startSpawnCount = 0;
    [SerializeField] private GameObject _safeSpawn;
    [SerializeField] private float _minDistanceFromPlayer = 0;
    [SerializeField] private Transform _groundHolder;


    private void Start()
    {
        SafeSpawn();
        _currentPosition.z++;
        for (int i = 0; i < _startSpawnCount; i++)
        {
            SpawnGround(true, new Vector3(0, 0, 1));
        }
        //_maxGroundCount = _currentGround.Count;
    }

    private void SafeSpawn()
    {
        Instantiate(_safeSpawn);
    }

    [HideInInspector] public int whichTerrain = 0;
    [HideInInspector] public int groundInSuccession = 0;
    public void SpawnGround(bool isStart, Vector3 playerPosition)
    {
        if (isStart)
        {
            if (groundInSuccession == 0)
            {
                whichTerrain = Random.Range(0, _groundDatas.Count);
                groundInSuccession = Random.Range(1, _groundDatas[whichTerrain]._maxInSuccesion);
            }

            GameObject ground = Instantiate(_groundDatas[whichTerrain].ground, _currentPosition, Quaternion.identity);
            _currentGround.Add(ground);

            if (_currentGround.Count > _maxGroundCount)
            {
                Destroy(_currentGround[0]);
                _currentGround.RemoveAt(0);
            }

            groundInSuccession--;
            _currentPosition.z++;
            Debug.Log(groundInSuccession + " , " + ground);
        }
    }
}