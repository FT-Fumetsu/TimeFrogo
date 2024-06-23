using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAnim : MonoBehaviour
{
    [SerializeField] private Animator _crate;

    void Start()
    {
       _crate = GetComponent<Animator>(); 
    }

    public void CrateKill()
    {
        _crate.SetTrigger("CrateKill");
    }
}
