using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _player;
    [SerializeField] private GameObject _safeSpawn;
    [SerializeField] private Transform _groundHolder;
    private List<GameObject> _currentGround = new List<GameObject>();
    [SerializeField] private List<GroundData> _groundDatasPresent;
    [SerializeField] private List<GroundData> _groundDatasPast;
    [SerializeField] private List<GroundData> _groundDatasFutur ;
    [SerializeField] private GameObject _vfxSnow;
    [SerializeField] private AudioSource _audioPresent;
    public AudioSource _forestSfx;
    public AudioSource _truckSfx;
    [SerializeField] private AudioSource _audioPast;
    public AudioSource _riverSfx;
    public AudioSource _snowStormSfx;
    [SerializeField] private AudioSource _audioFutur;
    public AudioSource _futurAmbianceSfx;
    [SerializeField] private GroundData _presentDefaultGround = null;
    [SerializeField] private GroundData _pastDefaultGround = null;
    [SerializeField] private GroundData _futurDefaultGround = null;
    [SerializeField] private GameObject _lightPresent;
    [SerializeField] private GameObject _lightPast;
    [SerializeField] private GameObject _lightFutur;

    [Header("Balancing")]
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
    private bool _started;

    private GroundData _lastGroundData = null;
    private GroundData _nextGroundData = null;
    private GroundData _defaultGround = null;

    private int _consecutiveObstacleCount = 0;
    private void Start()
    {
        _started = true;
        _defaultGround = _pastDefaultGround;
        _isAlive = true;
        actualList = 1;
        _choixList = _groundDatasPast;
        SafeSpawnStart();
        PlayPastMusic();
        StopPresentMusic();
        StopFuturMusic();
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
        if (_started == true)
        {
            Instantiate(_defaultGround.ground, _currentPosition, Quaternion.identity);
            _currentPosition.z++;
            _started = false;
        }

        if (groundInSuccession == 0)
        {
            bool water = true;
            whichTerrain = Random.Range(0, _choixList.Count);
            _nextGroundData = _choixList[whichTerrain];
            while (_nextGroundData == _lastGroundData)
            {
                whichTerrain = Random.Range(0, _choixList.Count);
                _nextGroundData = _choixList[whichTerrain];
            }
            while (_nextGroundData.GetComponent<Water>)
            if (_nextGroundData._maxInSuccesion > 0)
            {
                groundInSuccession = Random.Range(1, _choixList[whichTerrain]._maxInSuccesion);
            }           
        }
        _lastGroundData = _choixList[whichTerrain];

        GameObject ground = Instantiate(_nextGroundData.ground, _currentPosition, Quaternion.identity);

        if(ground.TryGetComponent(out Chunk chunk))
        {
            if(chunk.WallCount == 0)
            {
                _consecutiveObstacleCount = 0;
            }

            _consecutiveObstacleCount += chunk.WallCount;

            if(_consecutiveObstacleCount > 6)
            {
                Destroy(ground);
                ground = Instantiate(_defaultGround.ground, _currentPosition, Quaternion.identity);

                _consecutiveObstacleCount = 0;
            }
        }


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
        if (_chronoChangeBiome >= _changeBiome)
        {
            if (actualList == 2)
            {
                StopFuturMusic();
                PlayPastMusic();
                _chronoChangeBiome = 0;
                actualList = 1;
                _choixList = _groundDatasPast;
                _defaultGround = _pastDefaultGround;
            }
        }
        if (_chronoChangeBiome >= _changeBiome)
        {
            if (actualList == 1)
            {
                StopPastMusic();
                PlayPresentMusic();
                _chronoChangeBiome = 0;
                actualList = 0;
                _choixList = _groundDatasPresent;
                _defaultGround = _presentDefaultGround;
            }
        }
        if (_chronoChangeBiome >= _changeBiome)
        {
            if (actualList == 0)
            {
                StopPresentMusic();
                PlayFuturMusic();
                _chronoChangeBiome = 0;
                actualList = 2;
                _choixList = _groundDatasFutur;
                _defaultGround = _futurDefaultGround;
            }
        }
    }
    private void PlayPresentMusic()
    {
        _lightPresent.SetActive(true);
        _lightPast.SetActive(false);
        _lightFutur.SetActive(false);
        _audioPresent.mute = false;
        _forestSfx.mute = false;
        _truckSfx.mute = false;
    }
    private void PlayPastMusic()
    {
        _lightPresent.SetActive(false);
        _lightPast.SetActive(true);
        _lightFutur.SetActive(false);
        _audioPast.mute = false;
        _snowStormSfx.mute = false;
        _riverSfx.mute = false;
    }
    private void PlayFuturMusic()
    {
        _lightPresent.SetActive(false);
        _lightPast.SetActive(false);
        _lightFutur.SetActive(true);
        _audioFutur.mute = false;
        _futurAmbianceSfx.mute = false;
    }
    private void StopPresentMusic()
    {
        _audioPresent.mute = true;
        _forestSfx.mute = true;
        _truckSfx.mute = true;
    }
    private void StopPastMusic()
    {
        _audioPast.mute = true;
        _snowStormSfx.mute = true;
        _riverSfx.mute = true;
    }
    private void StopFuturMusic()
    {
        _audioFutur.mute = true;
        _futurAmbianceSfx.mute= true;
    }
}