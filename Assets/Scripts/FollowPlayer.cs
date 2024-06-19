using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _player;

    [Header("Balancing")]
    [SerializeField, Range(0, 10)] private float _inputTimer;
    [SerializeField, Range(0, 10)] private float _getKeyTimer;
    [SerializeField] private float _movement = 0.0f;
    [SerializeField] private float _camPos = 8f;
    [SerializeField] private float _chrono = 0;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;
    private float _currentPositionZ = 0.0f;
    Vector3 _targetPosition = Vector3.zero;

    private void Start()
    {
        _targetPosition = transform.position;
        _currentPositionZ = _currentPositionZ - _camPos;
    }
    private void Update()
    {
        _chrono += Time.deltaTime;
        Vector3 nextPosition = Vector3.Lerp(transform.position, _targetPosition, .2f);
        transform.position = nextPosition;
    }

    public void MoveUp()
    {
        _currentPositionZ += _movement;
        ApplyZPosition();
    }
    private void ApplyZPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = _currentPositionZ;
        _targetPosition = newPosition;
    }
}
