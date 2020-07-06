using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _joystick;
    private Image _touchMarker;
    private Vector2 _inputVector;

    private void Start()
    {
        _joystick = GetComponent<Image>();
        _touchMarker = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _inputVector = Vector2.zero;
        _touchMarker.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = pos.x / _joystick.rectTransform.sizeDelta.x;
            pos.y = pos.y / _joystick.rectTransform.sizeDelta.x;

            _inputVector = new Vector2(pos.x * 2, pos.y * 2);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            _touchMarker.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystick.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystick.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (_inputVector.x != 0) return _inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (_inputVector.y != 0) return _inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}