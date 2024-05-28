using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _player;
    [SerializeField] private Transform _transform;
    [SerializeField] private GroundSpawn _groundGenerator;
    [SerializeField] private FollowPlayer _followPlayer;
    [SerializeField] private GroundSpawn _groundSpawn;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private Echo _echo;
    [SerializeField] private RotationPlayer _visuPlayer;
    [SerializeField] private Vehicle _vehicle;
    [SerializeField] private Chrono _timer;
    [SerializeField] private RestartQuit _buttonMenu;
    [SerializeField] private FailedPauseMenu _faildedPauseMenu;
    [SerializeField] private AudioManager _audioManager;

    [Header("Balancing")]
    [SerializeField,Range(0,10)] private float _inputTimer;
    [SerializeField, Range(0, 10)] private float _getKeyTimer;
    [SerializeField] private float _movement = 1.0f;
    [SerializeField] private float _maxRightPosition = 3.0f;
    [SerializeField] private float _maxLeftPosition = 3.0f;
    [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _downKey = KeyCode.DownArrow;
    [SerializeField] private KeyCode _escape = KeyCode.Escape;
    [SerializeField] private float _speed;

    private float _playerSpeed;
    public bool _paused;
    float _score = 0;
    private float _chrono;
    public float _maxPositionZReach;
    public float _currentPositionX = 0.0f;
    public float _currentPositionZ = 0.0f;
    private float xpos;
    private Collider _lastIcePlatform = null;
    private GameObject _currentIcePlatform = null;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _playerSpeed = 0;
        _buttonMenu.UnPause();
        _paused = false;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * _playerSpeed * Time.deltaTime);
        _chrono += Time.deltaTime;
        RaycastHit hit;
        DrawRaycasts();

        if (_paused == true)
        {
            _faildedPauseMenu.PauseMenu();
        }

        if (_paused == false)
        {
            _faildedPauseMenu.Unpause();
        }
        
        if (Input.GetKeyDown(_leftKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.left), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    return;
                }
            }
            else
            {
                MoveLeft();
            }
        }

        if (Input.GetKeyDown(_rightKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.right), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + " right");
                }
            }
            else
            {
                MoveRight();
            }
        }

        if (Input.GetKeyDown(_upKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "up");
                }                        
            }
            else
            {
                MoveUp();
                if (_currentPositionZ <= _maxPositionZReach)
                {
                    return;
                }
                else
                {
                    SpawnGround();
                }
            }
        }

        if (Input.GetKeyDown(_downKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    return;
                }
            }
            else
            {
                MoveDown();
            }
        }

        if (Input.GetKeyDown(_escape) && _paused == false)
        {
            _paused = true;
            Time.timeScale = 0;
        }

        else if(Input.GetKeyDown(_escape) && _paused == true)
        {
            _paused = false;
            Time.timeScale = 1;
        }

        if (Input.GetKey(_leftKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.left), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    return;
                }
            }
            else
            {
                MoveLeft();
            }
        }

        if (Input.GetKey(_rightKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.right), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + " right");
                }
            }
            else
            {
                MoveRight();
            }
        }

        if (Input.GetKey(_upKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "up");
                }
            }
            else
            {
                _player.MoveUp();
                if (_player._currentPositionZ <= _player._maxPositionZReach)
                {
                    return;
                }
                else
                {
                    SpawnGround();
                }
            }
        }

        if (Input.GetKey(_downKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    return;
                }
            }
            else
            {
                MoveDown();
            }
        }

        _textScore.SetText("Score : " + _score.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._deathCrush);
            _audioManager.PlaySFX(_audioManager._deathTruck);
            _groundGenerator._isAlive = false;
            DestroyPlayer();
        }
        else if(other.gameObject.tag == "Water")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._splashSFX);
            _groundGenerator._isAlive = false;
            DestroyPlayer();
        }
        else if (other.gameObject.tag == "Crate")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._deathCrush);
            _audioManager.PlaySFX(_audioManager._deathTruck);
            _groundGenerator._isAlive = false;
            DestroyPlayer();
        }
        else if(other.gameObject.tag == "Echo")
        {
            GameOver();
            _groundGenerator._isAlive = false;
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Trapdoor")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._fall);
            _groundGenerator._isAlive = false;
            DestroyPlayer();
        }
        else if (other.gameObject.tag == "CrackedIce" || other.gameObject.tag == "Laser")
        {
            _faildedPauseMenu.GameOver();
            _groundGenerator._isAlive = false;
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL")
        {
            _currentIcePlatform = null;
            float xPos = collision.transform.position.x;
            int roundXPos = Mathf.RoundToInt(xPos);
            Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
            transform.position = pos;
            _currentPositionX = roundXPos;

            _echo.AddExitPlatformPosition(pos);
            _echo.InvokeExitPlatform();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL" && collision.collider != _lastIcePlatform)
        {
            transform.SetParent(collision.transform);
            _currentIcePlatform = collision.gameObject;
            _lastIcePlatform = collision.collider;

            if (collision.gameObject.tag == "IceBlockL")
            {
                //_playerSpeed = _speed;
                _echo.InvokeEchoSlideNeg();
            }
            else if (collision.gameObject.tag == "IceBlockR")
            {
                //_playerSpeed = -_speed;
                _echo.InvokeEchoSlide();
            }            
        }
    }
    public void MoveRight()
    {
        if (_currentPositionX >= _maxRightPosition)
        {
            return;
        }

        if (_lastIcePlatform != null)
        {
            _currentPositionX = Mathf.RoundToInt(transform.position.x);
        }
        _audioManager.PlaySFX(_audioManager._jump);
        _visuPlayer.RotationRight();
        _currentPositionX += _movement;
        ApplyXPosition();
        _echo.InvokeEchoMoveRight(transform.position);
        if(_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        //_playerSpeed = 0;
        _echo.InvokeEchoStopSlide(transform.position);
    }
    public void MoveLeft()
    {
        if (_currentPositionX <= _maxLeftPosition)
        {
            return;
        }

        if(_lastIcePlatform != null)
        {
            _currentPositionX = Mathf.RoundToInt(transform.position.x);
        }

        _audioManager.PlaySFX(_audioManager._jump);
        _visuPlayer.RotationLeft();
        _currentPositionX -= _movement;
        ApplyXPosition();
        _echo.InvokeEchoMoveLeft(transform.position);
        if(_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        //_playerSpeed = 0;
        _echo.InvokeEchoStopSlide(transform.position);
    }
    public void MoveUp()
    {
        _audioManager.PlaySFX(_audioManager._jump);
        _visuPlayer.RotationUp();
        _currentPositionZ += _movement;
        ApplyZPosition();
        _echo.InvokeEchoMoveUp(transform.position);
        transform.SetParent(null);
        //_playerSpeed = 0;
        _echo.InvokeEchoStopSlide(transform.position);
    }
    public void MoveDown()
    {
        _audioManager.PlaySFX(_audioManager._jump);
        _visuPlayer.RotationDown();
        _currentPositionZ -= _movement;
        ApplyZPosition();
        _echo.InvokeEchoMoveDown(transform.position);
        transform.SetParent(null);
        //_playerSpeed = 0;
        _echo.InvokeEchoStopSlide(transform.position);
    }
    private void ApplyXPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = _currentPositionX;
        _transform.position = newPosition;
    }
    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;
        _transform.position = newPosition;
    }
    public void SpawnGround()
    {
        _followPlayer.MoveUp();
        _maxPositionZReach = _currentPositionZ;
        _score++;
        _groundSpawn.SpawnGround(transform.position);
    }
    private void DrawRaycasts()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.right) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .45f), transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .45f), transform.TransformDirection(Vector3.left) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - 0.45f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);
    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    private void GameOver()
    {
        _faildedPauseMenu._failedMenu.SetActive(true);
        _groundGenerator._truckSfx.mute = true;
        _groundGenerator._forestSfx.mute = true;
        _groundGenerator._futurAmbianceSfx.mute = true;
        _groundGenerator._snowStormSfx.mute = true;
        _groundGenerator._riverSfx.mute = true;
    }
}