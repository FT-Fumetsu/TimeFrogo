using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Jump()
    {
        _animator.SetTrigger("EchoJump");
    }
    public void EchoKill()
    {
        _animator.SetTrigger("EchoKill");
    }
}
