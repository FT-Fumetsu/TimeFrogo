using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoAnimation : MonoBehaviour
{
    [SerializeField] private Animator _echoAssets;

    private void Start()
    {
        _echoAssets = GetComponent<Animator>();
    }
    public void Jump()
    {
        _echoAssets.SetTrigger("EchoJump");
    }
    public void EchoKill()
    {
        _echoAssets.SetTrigger("EchoKill");
    }
}
