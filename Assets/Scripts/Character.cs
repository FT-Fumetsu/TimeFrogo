using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GroundSpawn _groundSpawn;
    [SerializeField] private Echo _echo;
    [SerializeField] private RestartQuit _buttonMenu;
    [SerializeField] private FailedPauseMenu _failedPauseMenu;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PlayerMove _playerMove;

    [Header("Balancing")]
    [SerializeField] private KeyCode _escape = KeyCode.Escape;
    public Transform _graphics = null;

    private float _playerSpeed;
    public bool _paused;
    private Collider _lastIcePlatform = null;
    private GameObject _currentIcePlatform = null;

    private Tween _movementTween = null;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _playerSpeed = 0;
        _buttonMenu.UnPause();
        _paused = false;
        //_graphics.SetParent(null);
    }
    private void Update()
    {
        transform.Translate(Vector3.right * _playerSpeed * Time.deltaTime);
        DrawRaycasts();

        _graphics.transform.localPosition = Vector3.zero;
        if (_paused == true)
        {
            _failedPauseMenu.PauseMenu();
        }

        if (_paused == false)
        {
            _failedPauseMenu.Unpause();
        }
        
        

        if (Input.GetKeyDown(_escape) && _paused == false)
        {
            _paused = true;
            Time.timeScale = 0;
        }

        else if(Input.GetKeyDown(_escape) && _paused == true)
        {
            _paused = false;
            Time.timeScale = 1;
        }
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
            GameOver();
            _groundSpawn._isAlive = false;
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Trapdoor")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._fall);
            _groundSpawn._isAlive = false;
            DestroyPlayer();
        }
        else if (other.gameObject.tag == "Laser")
        {
            GameOver();
            _audioManager.PlaySFX(_audioManager._laserShot);
            _groundSpawn._isAlive = false;
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL")
        {
            _currentIcePlatform = null;
            float xPos = collision.transform.position.x;
            int roundXPos = Mathf.RoundToInt(xPos);
            Vector3 pos = new Vector3(roundXPos, transform.position.y, transform.position.z);
            transform.position = pos;
            _playerMove._currentPositionX = roundXPos;

            _echo.AddExitPlatformPosition(pos);
            _echo.InvokeExitPlatform();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "IceBlockR" || collision.gameObject.tag == "IceBlockL" && collision.collider != _lastIcePlatform)
        {
            //_graphics.transform.SetParent(collision.transform);
            transform.SetParent(collision.transform);
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
        }
    }
  

    private void DrawRaycasts()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.forward) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.right) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.right) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z + .425f), transform.TransformDirection(Vector3.left) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z - .425f), transform.TransformDirection(Vector3.left) * 1, Color.red);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x + .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x - .425f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.back) * 1, Color.red);
    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    private void GameOver()
    {
        _failedPauseMenu._failedMenu.SetActive(true);
        _groundSpawn._truckSfx.mute = true;
        _groundSpawn._forestSfx.mute = true;
        _groundSpawn._futurAmbianceSfx.mute = true;
        _groundSpawn._snowStormSfx.mute = true;
        _groundSpawn._riverSfx.mute = true;
    }
    public void PlayMovementTween(Vector3 newPosition)
    {
        _movementTween?.Kill();
        _graphics.DOMove(newPosition, .1f).SetEase(Ease.InOutBack);
        Transform graphics = _graphics.GetChild(0);
        Sequence s = DOTween.Sequence();
        s.Append(graphics.DOMoveY(2f, .05f).SetEase(Ease.OutQuint));
        s.Append(graphics.DOMoveY(1f, .05f).SetEase(Ease.OutBounce));
        s.Play();
        _movementTween.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }
}