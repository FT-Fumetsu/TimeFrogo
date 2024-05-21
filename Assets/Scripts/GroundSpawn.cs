using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _safeSpawn;
    [SerializeField] private Transform _groundHolder;
    private List<GameObject> _currentGround = new List<GameObject>();
    [SerializeField] private List<GroundData> _groundDatasPresent;
    [SerializeField] private List<GroundData> _groundDatasPast;
    [SerializeField] private List<GroundData> _groundDatasFutur ;
    [SerializeField] private GameObject _vfxSnow;
    [SerializeField] private AudioSource _audioPresent;
    [SerializeField] private AudioSource _audioPast;
    [SerializeField] private AudioSource _audioFutur;
    private int actualList=0;
    private List<GroundData> _choixList;
    public Vector3 _currentPosition = new Vector3(0, 0, 0);

    [SerializeField] private int _maxGroundCount = 0;
    [SerializeField] private int _startSpawnCount = 0;
    [SerializeField] private float _chronoChangeBiome = 0;
    [SerializeField] private float _changeBiome = 60f;
    public bool _isAlive;

    [HideInInspector] public int whichTerrain = 0;
    private int groundInSuccession = 0;

    private GroundData lastGroundData = null;
    private GroundData nextGroundData = null;
    //private bool _present;
    //private bool _past;
    //private bool _future;

    private void Start()
    {
        _isAlive = true;
        actualList = 0;
        _choixList = _groundDatasPresent;
        SafeSpawnStart();
        _audioPresent.mute = false;
        _audioPast.mute = true;
        _audioFutur.mute = true;
    }
    private void Update()
    {
        if (_isAlive == true)
        {
            _chronoChangeBiome += Time.deltaTime;
            if (actualList == 0)
            {
                _choixList = _groundDatasPresent;
                _vfxSnow.SetActive(false);
            }
            if (actualList == 1)
            {
                _choixList = _groundDatasPast;
                _vfxSnow.SetActive(true);
            }
            if (actualList == 2)
            {
                _choixList = _groundDatasFutur;
                _vfxSnow.SetActive(false);
            }
            ChangeBiome();
        }
        else if (_isAlive == false)
        {
            //Debug.Log("Is Dead");
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _chronoChangeBiome = 59f;
        }
    }
    private void SafeSpawn()
    {
        Instantiate(_safeSpawn);
    }

    public void SpawnGround(Vector3 playerPosition)
    {
        if (groundInSuccession == 0)
        {
            whichTerrain = Random.Range(0, _choixList.Count);
            nextGroundData = _choixList[whichTerrain];
            while (nextGroundData == lastGroundData)
            {
                whichTerrain = Random.Range(0, _choixList.Count);
                nextGroundData = _choixList[whichTerrain];
            }
            if (nextGroundData._maxInSuccesion > 0)
            {
                groundInSuccession = Random.Range(1, _choixList[whichTerrain]._maxInSuccesion);
            }           
        }
        lastGroundData = _choixList[whichTerrain];      
        GameObject ground = Instantiate(nextGroundData.ground, _currentPosition, Quaternion.identity);
        _currentGround.Add(ground);
        if (_currentGround.Count > _maxGroundCount)
        {
            Destroy(_currentGround[0]);
            _currentGround.RemoveAt(0);
        }
        groundInSuccession--;
        _currentPosition.z++;        
    }
    private void SafeSpawnStart()
    {
        SafeSpawn();
        _currentPosition.z++;
        for (int i = 0; i < _startSpawnCount; i++)
        {
            SpawnGround(new Vector3(0, 0, 1));
        }
    }

    private void ChangeBiome()
    {
        if (_chronoChangeBiome >= _changeBiome) //3 fois le if car sinon, si on passe du passé au futur, on passe instantanément du futur au présent.
        {
            if (actualList == 2)
            {
                _chronoChangeBiome = 0;
                //actualList = Random.Range(0, 1);
                actualList = 0;
                _choixList = _groundDatasPresent;
            }
        }
        if (_chronoChangeBiome >= _changeBiome)
        {
            if (actualList == 1)
            {
                _audioPast.mute = true;
                _audioPresent.mute = false;
                _chronoChangeBiome = 0;
                actualList = 0;
                _choixList = _groundDatasPresent;
            }
        }
        if (_chronoChangeBiome >= _changeBiome)
        {
            if (actualList == 0)
            {
                _audioPresent.mute = true;
                _audioPast.mute = false;
                _chronoChangeBiome = 0;
                //actualList = Random.Range(1, 2);
                actualList = 1;
                _choixList = _groundDatasPast;
            }
        }
    }
}