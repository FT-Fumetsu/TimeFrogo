using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _character = null;
    [SerializeField] private Transform _transform = null;
    [SerializeField] private GroundSpawn _groundGenerator;

    [Header("Balancing")]
    [SerializeField,Range(0,10)] private float _inputTimer;
    [SerializeField] private float _movement = 0.0f;
    [SerializeField] private float _maxRightPosition = 0.0f;
    [SerializeField] private float _maxLeftPosition = 0.0f;
    [SerializeField] private KeyCode _leftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode _rightKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _downKey = KeyCode.DownArrow;
    [SerializeField] private GroundSpawn _groundSpawn;
    [SerializeField] private int _oldPosition = 0;

    private bool _enabled = true;
    private float _actualTime = 0;
    private float _currentPositionX = 0.0f;
    private float _currentPositionZ = 0.0f;
    private Vector3 _anciennePosition;


    private void Update()
    {
        _actualTime += Time.deltaTime;
        _oldPosition = (int)_currentPositionZ - 2;
        if (Input.GetKeyDown(_leftKey) && _enabled == true)
        {
            _actualTime = 0;
            _enabled = false;
            _anciennePosition = _transform.position;
            MoveLeft();
        }

        if (Input.GetKeyDown(_rightKey) && _enabled == true)
        {
            _actualTime = 0;
            _enabled = false;
            _anciennePosition = _transform.position;
            MoveRight();
        }

        if (Input.GetKeyDown(_upKey) && _enabled == true)
        {
            _actualTime = 0;
            _enabled = false;
            _anciennePosition = _transform.position;
            MoveUp();
        }

        if (Input.GetKeyDown(_downKey) && _enabled == true)
        {
            _actualTime = 0;
            _enabled = false;
            _anciennePosition = _transform.position;
            MoveDown();
        }

        if (_actualTime > _inputTimer)
        {
            _enabled = true ;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            Debug.Log(other.gameObject.tag);
            transform.position = _anciennePosition;
            _currentPositionX -= _movement;
            _currentPositionZ -= _movement;
        }
    }

    public void MoveRight()
    {
        if (_currentPositionX >= _maxRightPosition)
        {
            return;
        }

        _currentPositionX += _movement;

        ApplyXPosition();
    }

    public void MoveLeft()
    {
        if (_currentPositionX <= _maxLeftPosition)
        {
            return;
        }

        _currentPositionX -= _movement;

        ApplyXPosition();
    }

    public void MoveUp()
    {
        _currentPositionZ += _movement;
        ApplyZPosition();
        _groundSpawn.SpawnGround(true, transform.position);
    }

    public void MoveDown()
    {
        _currentPositionZ -= _movement;
        ApplyZPosition();
    }

    private void ApplyXPosition()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = _currentPositionX;
        newPosition.y = _transform.position.y;
        newPosition.z = _transform.position.z;

        _transform.position = newPosition;
    }

    private void ApplyZPosition()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = _transform.position.x;
        newPosition.y = _transform.position.y;
        newPosition.z = _currentPositionZ;

        _transform.position = newPosition;
    }
}