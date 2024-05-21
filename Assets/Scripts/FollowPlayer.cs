using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _player;
    //[SerializeField] private FollowPlayer _followPlayer;

    [Header("Balancing")]
    [SerializeField, Range(0, 10)] private float _inputTimer;
    [SerializeField, Range(0, 10)] private float _getKeyTimer;
    [SerializeField] private float _movement = 0.0f;
    //[SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
    [SerializeField] private float _camPos = 8f;
    [SerializeField] private float _chrono = 0;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;
    private float _currentPositionZ = 0.0f;

    private void Start()
    {
        _currentPositionZ = _currentPositionZ - _camPos;
    }
    private void Update()
    {
        _chrono += Time.deltaTime;
    }
    //public void MoveCamera()
    //{
    //    if (Input.GetKeyDown(_upKey) && _chrono >= _inputTimer)
    //    {
    //        _chrono = 0;
    //        MoveUp();
    //    }
    //    if (Input.GetKey(_upKey) && _chrono >= _getKeyTimer)
    //    {
    //        _chrono = 0;
    //        MoveUp();
    //    }
    //}
    public void MoveUp()
    {
        _currentPositionZ += _movement;
        ApplyZPosition();
    }
    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;

        _transform.position = newPosition;
    }
    //public void SmoothedCamera()
    //{
    //    Vector3 desiredPosition = _player.position + _offset;
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
    //    transform.position = smoothedPosition;
    //}
}
