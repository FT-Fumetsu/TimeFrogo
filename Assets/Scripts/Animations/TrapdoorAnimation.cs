using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator _trapdoor;

    private void Start()
    {
        _trapdoor = GetComponent<Animator>();
    }
    public void TrapKill()
    {
        _trapdoor.SetTrigger("OpenTrapdoor");
    }
}
