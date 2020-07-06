using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    //змінні для контролю камери та повороту
    private float _sensitivityRotate = 0.2f;
    private float _verticalRotateLimit = 65f;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _rotationZ = 0;

    private bool _isTintLeft = false;
    private bool _isTintRight = false;
    private float _koef = 1;
    private float _rotationZAngle = 15f;

    private GameObject _player;

    //джойстик та тачпад
    private FixedTouchField _touchField;

    private void Start()
    {
        _touchField = GameObject.FindGameObjectWithTag("TouchField").GetComponent<FixedTouchField>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _rotationX -= _touchField.TouchDist.y * _sensitivityRotate;
        _rotationX = Mathf.Clamp(_rotationX, -_verticalRotateLimit, _verticalRotateLimit);

        _rotationY = transform.localEulerAngles.y;

        if (!_isTintLeft && !_isTintRight)
            _rotationZ = 0;

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY, _rotationZ);
    }

    public void TintLeft()
    {
        if (!_isTintLeft)
        {
            Tint(-_koef, _rotationZAngle);
            _isTintLeft = true;
        }
        else
        {
            Tint();
            _isTintLeft = false;
        }
    }

    public void TintRight()
    {
        if (!_isTintRight)
        {
            Tint(_koef, -_rotationZAngle);
            _isTintRight = true;
        }
        else
        {
            Tint();
            _isTintRight = false;
        }
    }

    private void Tint(float koef = 0, float angle = 0)
    {
        Vector3 cameraPos = transform.localPosition;
        cameraPos.x = 0;
        transform.localPosition = cameraPos;
        _isTintLeft = false;
        _isTintRight = false;

        transform.localPosition = transform.localPosition + new Vector3(koef * 0.25f, 0, 0);
        _rotationZ = angle;
    }
}
