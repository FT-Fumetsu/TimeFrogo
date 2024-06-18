using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private FroggoAnimation _froggo;
    [SerializeField] private EchoAnimation _echo;
    [SerializeField] private LaserFroggoAnimation _laserFroggo;
    [SerializeField] private TrapdoorAnimation _trapdoor;

    public void EchoJump()
    {
        _echo.Jump();
    }
    public void FroggoJump()
    {
        _froggo.Jump();
    }
    public void EchoKill()
    {
        _echo.EchoKill();
        _froggo.EchoKill();
    }
    public void TrapKill()
    {
        _froggo.TrapKill();
        _trapdoor.TrapKill();
    }
    public void LaserKill()
    {
        _laserFroggo.LaserKill();
    }
}
