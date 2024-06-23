using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnimation : MonoBehaviour
{
    [SerializeField] private Animator _laser;
    
    void Start()
    {
        _laser = GetComponent<Animator>();
    }

    public void LaserKill()
    {
        _laser.SetTrigger("LaserKill");
    }

}
