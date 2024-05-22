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
    [SerializeField] private GameObject _failedMenu;
    [SerializeField] private GameObject _pauseMenu;
    public bool _paused;
    float _score = 0;
    private float _chrono;
    public float _maxPositionZReach;
    public float _currentPositionX = 0.0f;
    public float _currentPositionZ = 0.0f;
    private float xpos;
    //private bool _isNull;

    private Collider _lastIcePlatform = null;
    private GameObject _currentIcePlatform = null;

    private void Start()
    {
        //_isNull = false;
        _buttonMenu.UnPause();
        _paused = false;
    }
    private void Update()
    {
        _chrono += Time.deltaTime;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1, Color.red);

        if (_paused == true)
        {
            _pauseMenu.SetActive(true);
        }

        //if (_chrono >= .2)
        //{
        //    _isNull = true;
        //}

        if (_paused == false)
        {
            _pauseMenu.SetActive(false);
        }

        //if (_isNull == false)
        //{
        //    return;
        //}
        //else if (_isNull == true)
        //{
        //}

        if (Input.GetKeyDown(_leftKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "left");
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "right");
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "up");
                }
            }
            else
            {
                MoveUp();
                if(_currentPositionZ <= _maxPositionZReach) 
                {
                    Debug.Log("Max Position Z Reach not up");
                }
                else
                {
                    _followPlayer.MoveUp();
                    _maxPositionZReach = _currentPositionZ;
                    _score++;
                    _groundSpawn.SpawnGround(transform.position);
                }
            }
        }

        if (Input.GetKeyDown(_downKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "down");
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "left");
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "right");
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
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
                    Debug.Log("Max Position Z Reach not up");
                }
                else
                {
                    _followPlayer.MoveUp();
                    _maxPositionZReach = _currentPositionZ;
                    _score++;
                    _groundSpawn.SpawnGround(transform.position);
                }
            }
        }

        if (Input.GetKey(_downKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1))
            {
                if (hit.transform.tag == "Obstacles")
                {
                    Debug.Log(hit.transform.name + "down");
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
        if (other.gameObject.tag == "Vehicle" || other.gameObject.tag == "Water" || other.gameObject.tag == "Echo")
        {
            _failedMenu.SetActive(true);
            _groundGenerator._isAlive = false;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "CrackedIce" || other.gameObject.tag == "Case" || other.gameObject.tag == "Trapdoor" || other.gameObject.tag == "Laser")
        {
            _failedMenu.SetActive(true);
            _groundGenerator._isAlive = false;
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Obstacles")
        {
            _echo.InvokeEchoStopSpeed();
            transform.SetParent(null);
        }
        else if (other.gameObject.tag == "LeftTP")
        {
            _currentPositionX = -3;
        }
        else if (other.gameObject.tag == "RightTP")
        {
            _currentPositionX = 3;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL")
        {
            _currentIcePlatform = null;

            Debug.Log("Exit Collision");
            //_echo.InvokeEchoStopSlide();
            //transform.SetParent(null);
            float xPos = collision.transform.position.x;
            int roundXPos = Mathf.RoundToInt(xPos);
            Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
            transform.position = pos;
            _currentPositionX = roundXPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL" && collision.collider != _lastIcePlatform)
        {
            transform.SetParent(collision.transform);

            //float exactPositionX = collision.transform.position.x;
            //transform.position = new Vector3(exactPositionX, transform.position.y, transform.position.z);

            _currentIcePlatform = collision.gameObject;
            _lastIcePlatform = collision.collider;

            if (collision.gameObject.tag == "IceBlockL")
            {
                _echo.InvokeEchoSlideNeg();

                /*IceBlocMovementCommand iceBlocMovementCommand = new IceBlocMovementCommand(_echo, IceBlockMovementDir.Left);
                _echo.EchoCommands.Add(iceBlocMovementCommand);*/
            }
            else if (collision.gameObject.tag == "IceBlockR")
            {
                _echo.InvokeEchoSlide();
                /*IceBlocMovementCommand iceBlocMovementCommand = new IceBlocMovementCommand(_echo, IceBlockMovementDir.Right);
                _echo.EchoCommands.Add(iceBlocMovementCommand);*/
            }            
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.TryGetComponent(out Vehicle vehicle))
    //    {
    //        Vector3 translatePlatform = transform.position;
    //        translatePlatform.x += vehicle.vitesse * Time.deltaTime;
    //        transform.position = translatePlatform;
    //    }
    //}
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

        _visuPlayer.RotationRight();
        _currentPositionX += _movement;
        ApplyXPosition();
        _echo.InvokeEchoMoveRight();
        if(_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        _echo.InvokeEchoStopSlide();
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

        _visuPlayer.RotationLeft();
        _currentPositionX -= _movement;
        ApplyXPosition();
        _echo.InvokeEchoMoveLeft();
        if(_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        _echo.InvokeEchoStopSlide();
    }
    public void MoveUp()
    {
        _visuPlayer.RotationUp();
        _currentPositionZ += _movement;
        ApplyZPosition();
        _echo.InvokeEchoMoveUp();
        transform.SetParent(null);
        _echo.InvokeEchoStopSlide();
    }
    public void MoveDown()
    {
        _visuPlayer.RotationDown();
        _currentPositionZ -= _movement;
        ApplyZPosition();
        _echo.InvokeEchoMoveDown();
        transform.SetParent(null);
        _echo.InvokeEchoStopSlide();
    }
    private void ApplyXPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = _currentPositionX;

        _transform.position = newPosition;

        /*if(_currentIcePlatform != null)
        {
            IceBlocMovementCommand iceBlocMovementCommand = new IceBlocMovementCommand(_echo, IceBlockMovementDir.None);
            _echo.EchoCommands.Add(iceBlocMovementCommand);
        }

        AddMovementEchoCommand(newPosition);*/
    }

    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;

        _transform.position = newPosition;

        //AddMovementEchoCommand(newPosition);
    }

    /*private void AddMovementEchoCommand(Vector3 newPosition)
    {
        if (_echo != null)
        {
            if(_currentIcePlatform != null)
            {
                Debug.Log("Is On Platform.");
            }

            MovementEchoCommand command = new MovementEchoCommand(_echo, newPosition);
            _echo.EchoCommands.Add(command);
        }
    }*/
}