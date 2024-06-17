using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _froggo;
    [SerializeField] private Animator _echo;
    [SerializeField] private Animator _laserFroggo;
    [SerializeField] private Animator _trapdoor;


    public void JumpFroggo()
    {
        _froggo.SetTrigger("JumpFroggo");
    }
    public void EchoJump()
    {
        _echo.SetTrigger("EchoJump");
    }
    public void KillLaser()
    {
        _laserFroggo.SetTrigger("LaserKill");
    }
    public void KillTrapdoor()
    {
        _froggo.SetTrigger("TrapKill");
        _laserFroggo.SetTrigger("OpenTrapdoor");
    }
    public void KillEcho()
    {
        _froggo.SetTrigger("FroggerKillEcho");
        _echo.SetTrigger("EchoKill");
    }
}
