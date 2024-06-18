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
        _froggoJump.SetTrigger("Jump");
        AnimatorStateInfo info = _froggoJump.GetCurrentAnimatorStateInfo(0);
        float jumpLenght = info.length;
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
