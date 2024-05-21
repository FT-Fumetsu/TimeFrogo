using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLobby : MonoBehaviour
{
    public AudioSource _musicLobby;

    private void Start()
    {
        _musicLobby.mute = false;
    }
}
