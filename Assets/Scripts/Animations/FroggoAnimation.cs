using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggoAnimation : MonoBehaviour
{
    [SerializeField] private Animator _froggoJump;

    private void Start()
    {
        _froggoJump = GetComponent<Animator>();
    }
    public void Jump()
    {
        _froggoJump.SetTrigger("JumpFroggo");
    }
    public void TrapKill()
    {
        _froggoJump.SetTrigger("TrapKill");
    }
    public void EchoKill()
    {
        _froggoJump.SetTrigger("FroggerKillEcho");
    }
}
