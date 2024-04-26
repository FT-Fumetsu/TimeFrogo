using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _timeDeath = 0f;
    [SerializeField] private float _timer = 5f;
    [SerializeField] private Character _character;
    private void Start()
    {
        if (transform.position.x > 0)
        {
            _speed = -(_speed);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        _timeDeath += Time.deltaTime;
        if (_timeDeath >= _timer)
        {
            _timeDeath = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
