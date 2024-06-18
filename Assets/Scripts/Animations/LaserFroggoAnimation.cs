using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFroggoAnimation : MonoBehaviour
{
    [SerializeField] private Animator _laserFroggo;

    private void Start()
    {
        _laserFroggo = GetComponent<Animator>();
    }
    public void LaserKill()
    {
        _laserFroggo.SetTrigger("LaserKill");
    }
}
