using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("Referencences")]
    [SerializeField] private Character _player;
    [SerializeField] private Echo _echo;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private RotationPlayer _visuPlayer;
    [SerializeField] private Transform _transform;
    [SerializeField] private FollowPlayer _followPlayer;
    [SerializeField] private GroundSpawn _groundSpawn;
    [SerializeField] private TextMeshProUGUI _textScore;

    [Header("Balancing")]
    [SerializeField, Range(0, 10)] private float _inputTimer;
    [SerializeField, Range(0, 10)] private float _getKeyTimer;
    [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _downKey = KeyCode.DownArrow;
    [SerializeField] private float _movement = 1.0f;
    [SerializeField] private float _maxRightPosition = 3.0f;
    [SerializeField] private float _maxLeftPosition = -3.0f;

    private float _chrono;
    public float _maxPositionZReach;
    public float _currentPositionX = 0.0f;
    public float _currentPositionZ = 0.0f;
    float _score = 0;
    private Collider _lastIcePlatform = null;
    private void Update()
    {
        _chrono += Time.deltaTime;
        RaycastHit hit;
        if (Input.GetKeyDown(_leftKey) && _chrono > _inputTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.left), out hit, 1))
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.right), out hit, 1))
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
            if (Physics.Raycast(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1))
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
        if (Input.GetKey(_leftKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.left), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.left), out hit, 1))
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.right), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.right), out hit, 1))
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
            if (Physics.Raycast(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 1) || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
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

        if (Input.GetKey(_downKey) && _chrono > _getKeyTimer)
        {
            _chrono = 0;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1) || Physics.Raycast(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back), out hit, 1))
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
        if (_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        //_player._graphics.transform.SetParent(null);
        _echo.InvokeEchoStopSlide(transform.position);
    }
    public void MoveLeft()
    {
        if (_currentPositionX <= _maxLeftPosition)
        {
            return;
        }

        if (_lastIcePlatform != null)
        {
            _currentPositionX = Mathf.RoundToInt(transform.position.x);
        }

        _audioManager.PlaySFX(_audioManager._jump);
        _visuPlayer.RotationLeft();
        _currentPositionX -= _movement;
        ApplyXPosition();
        _echo.InvokeEchoMoveLeft(transform.position);
        if (_lastIcePlatform != null)
        {
            _lastIcePlatform.enabled = false;
        }
        transform.SetParent(null);
        //_player._graphics.transform.SetParent(null);
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
        //_player._graphics.transform.SetParent(null);
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
        //_player._graphics.transform.SetParent(null);
        _echo.InvokeEchoStopSlide(transform.position);
    }
    private void ApplyXPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = _currentPositionX;
        _transform.position = newPosition;

        _player.PlayMovementTween(newPosition);
    }
    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;
        _transform.position = newPosition;

        _player.PlayMovementTween(newPosition);
    }
    public void SpawnGround()
    {
        _followPlayer.MoveUp();
        _maxPositionZReach = _currentPositionZ;
        _score++;
        _groundSpawn.SpawnGround(transform.position);
    }
}