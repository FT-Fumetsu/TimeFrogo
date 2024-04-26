using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterPoint _character = null;
    [SerializeField] private Transform _transform = null;

    [Header("Balancing")]
    [SerializeField] private float _movement = 0.0f;
    [SerializeField] private KeyCode _upKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _downKey = KeyCode.DownArrow;

    private float _currentPositionZ = 0.0f;

    public void MoveUp()
    {
        _currentPositionZ += _movement;
        ApplyZPosition();
    }

    public void MoveDown()
    {
        _currentPositionZ -= _movement;
        ApplyZPosition();
    }

    private void ApplyZPosition()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = _transform.position.x;
        newPosition.y = _transform.position.y;
        newPosition.z = _currentPositionZ;

        _transform.position = newPosition;
    }
    public void Update()
    {
        if (Input.GetKeyDown(_upKey))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(_downKey))
        {
            MoveDown();
        }
    }
}