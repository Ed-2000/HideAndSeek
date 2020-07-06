using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _normalScale;
    private GameObject _flashlight;
    private GameObject[] _playerSpawnPosGO;
    private List<Vector3> _playerSpawnPos;

    //змінні для переміщення
    private float _speed = 4f;
    private float _speedKoef = 3f;
    private float _gravityForce = -9.8f;
    private Vector3 _moveVector;
    private CharacterController _chController;
    private AudioSource _audio;


    //джойстик та тачпад
    private Joystick _leftJoystick;
    private FixedTouchField _touchField;

    //змінні для контролю камери та повороту
    private float _sensitivityRotate = 0.2f;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _normalScale = transform.localScale;
        _flashlight = GameObject.FindGameObjectWithTag("Flashlight");

        _chController = GetComponent<CharacterController>();
        _leftJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        _touchField = GameObject.FindGameObjectWithTag("TouchField").GetComponent<FixedTouchField>();

        _playerSpawnPosGO = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        _playerSpawnPos = new List<Vector3>();

        for (int i = 0; i < _playerSpawnPosGO.Length; i++) //заповнюємо список координат можливого спавну гравця
        {
            _playerSpawnPos.Add(_playerSpawnPosGO[i].transform.position);
        }

        transform.position = _playerSpawnPos[Random.Range(0, _playerSpawnPos.Count)] + new Vector3(0, 1.5f, 0);

        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    private void Update()
    {
        Move();
        Look();
        Gravity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Dead();
        }
    }

    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _leftJoystick.Horizontal() * _speed; 
        _moveVector.z = _leftJoystick.Vertical() * _speed;

        if (_moveVector.x !=0 || _moveVector.z != 0)
        {
            if (!_audio.isPlaying)
            {
                _audio.Play();
            }
        }
        
        _moveVector = Vector3.ClampMagnitude(_moveVector, _speed);

        _moveVector.y = _gravityForce;

        _moveVector *= Time.deltaTime;
        _moveVector = transform.TransformDirection(_moveVector);
        _chController.Move(_moveVector);
    }

    private void Look()
    {
        transform.Rotate(0, _touchField.TouchDist.x * _sensitivityRotate, 0);
    }

    public void SitDownAndStandUp()
    {
        if (transform.localScale.y == _normalScale.y)
        {
            transform.localScale = transform.localScale / 2;
            _speed /= _speedKoef;
            _audio.volume /= _speedKoef;
        }
        else
        {
            transform.localScale = _normalScale;
            _speed *= _speedKoef;
            _audio.volume *= _speedKoef;
        }
    }

    public void OfOnFlashlight()
    {
        _flashlight.SetActive(!_flashlight.activeSelf);
    }

    private void Dead()
    {
        Debug.Log("You Dead...");
    }

    private void Gravity()
    {
        if (!_chController.isGrounded) _gravityForce -= 20f;
        else _gravityForce = -1f;
    }
}

//private void Rotate()
//{
//    //_cameraAngle += _touchField.TouchDist.x * _cameraAngleSpeed;

//    //Camera.main.transform.position = transform.position + Quaternion.AngleAxis(_cameraAngle, Vector3.up) * new Vector3(0, 2.5f, -3f);
//    //Camera.main.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - Camera.main.transform.position, Vector3.up);
//}