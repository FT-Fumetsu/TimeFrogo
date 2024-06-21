using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _player;
    [SerializeField] private GroundSpawn _groundSpawn;
    [SerializeField] private Echo _echo;
    [SerializeField] private RestartQuit _restartQuit;
    [SerializeField] private FailedPauseMenu _failedPauseMenu;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animations _animations;

    [Header("Balancing")]
    public Transform _graphics = null;

    public bool _paused;
    private Collider _lastIcePlatform = null;
    private GameObject _currentIcePlatform = null;

    public bool IsOnMovingPlatform { get; private set; } = false;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _restartQuit.UnPause();
        _paused = false;
    }
    private void Update()
    {
        if (_paused == true)
        {
            _failedPauseMenu.PauseMenu();
        }

        if (_paused == false)
        {
            _restartQuit.UnPause();
        }

        DrawRaycasts();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._deathCrush);
            _audioManager.PlaySFX(_audioManager._deathTruck);
            _groundSpawn._isAlive = false;
            DestroyPlayer();
        }
        else if(other.gameObject.tag == "Water")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._splashSFX);
            _groundSpawn._isAlive = false;
            DestroyPlayer();
        }
        else if (other.gameObject.tag == "Crate")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._deathCrush);
            _audioManager.PlaySFX(_audioManager._deathTruck);
            _groundSpawn._isAlive = false;
            DestroyPlayer();
        }
        else if(other.gameObject.tag == "Echo")
        {
            _animations.EchoKill();
            Invoke(nameof(DestroyPlayer), 2f);
            GameOver();
            _groundSpawn._isAlive = false;

        }
        else if(other.gameObject.tag == "Trapdoor")
        {
            _animations.TrapKill();
            Invoke(nameof(DestroyPlayer), 0.6052631f);
            GameOver();
            _audioManager.PlaySFX(_audioManager._fall);
            _groundSpawn._isAlive = false;

        }
        else if (other.gameObject.tag == "Laser")
        {
            _animations.LaserKill();
            Invoke(nameof(DestroyPlayer), 0.4827586f);
            GameOver();
            _audioManager.PlaySFX(_audioManager._laserShot);
            _groundSpawn._isAlive = false;            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL")
        {
            _currentIcePlatform = null;

            float xPos = collision.transform.position.x;
            int roundXPos = (int)xPos;
            Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
            transform.position = pos;
            Debug.Log(transform.position);
            _playerMove._currentPositionX = roundXPos;
            //transform.position = new Vector3 (_playerMove._currentPositionX, transform.position.y, transform.position.z);            
            _echo.AddExitPlatformPosition(pos);
            _echo.InvokeExitPlatform();

            IsOnMovingPlatform = false;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL" && collision.collider != _lastIcePlatform)
        {
            transform.SetParent(collision.transform);
            _playerMove.DontMoveOnIceBlocks();
            _currentIcePlatform = collision.gameObject;
            _lastIcePlatform = collision.collider;

            if (collision.gameObject.tag == "IceBlockL")
            {
                _echo.InvokeEchoSlideNeg();
            }
            else if (collision.gameObject.tag == "IceBlockR")
            {                
                _echo.InvokeEchoSlide();
            }

            IsOnMovingPlatform = true;
        }
    }
    private void DrawRaycasts()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .4f), transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .4f), transform.TransformDirection(Vector3.right) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .4f), transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .4f), transform.TransformDirection(Vector3.left) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);

        Debug.DrawRay(new Vector3(transform.position.x + .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down) * 3f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - .4f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down) * 3f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .4f), transform.TransformDirection(Vector3.down) * 3f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .4f), transform.TransformDirection(Vector3.down) * 3f, Color.red);
    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    private void GameOver()
    {
        //GetComponent(PlayerMove).enable = false;
        _failedPauseMenu._failedMenu.SetActive(true);
        _groundSpawn._truckSfx.mute = true;
        _groundSpawn._forestSfx.mute = true;
        _groundSpawn._futurAmbianceSfx.mute = true;
        _groundSpawn._snowStormSfx.mute = true;
        _groundSpawn._riverSfx.mute = true;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    BoxCollider collider = GetComponent<BoxCollider>();
    //    Gizmos.DrawCube(transform.position + collider.center, collider.size);
    //}
    public void Escape(CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
        {
            return;
        }
        if (_paused == false)
        {
            _paused = true;
            Time.timeScale = 0;
        }

        else if (_paused == true)
        {
            _paused = false;
            Time.timeScale = 1;
        }
    }
    public void KillWater()
    {
        _animations.TrapKill();
        GameOver();
        _audioManager.PlaySFX(_audioManager._splashSFX);
        _groundSpawn._isAlive = false;
        DestroyPlayer();
    }
}