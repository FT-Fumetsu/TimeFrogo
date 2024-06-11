using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float _speed = 0f;
    public float vitesse { get { return _speed; } }
    [SerializeField] private float _timeDeath = 0f;
    [SerializeField] private float _timer = 5f;
    private void Start()
    {
        if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }

    private void Update()
    {
        TranslatePosition();
        _timeDeath += Time.deltaTime;
        if (_timeDeath >= _timer)
        {
            _timeDeath = 0;
            Destroy(gameObject);
        }
    }
    private void TranslatePosition()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}