using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class RightJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _rightJoystick;
    private Image _touchMarker;
    private Vector2 _rotateVector;

    private void Start()
    {
        _rightJoystick = GetComponent<Image>();
        _touchMarker = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _rotateVector = Vector2.zero;
        _touchMarker.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rightJoystick.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = pos.x / _rightJoystick.rectTransform.sizeDelta.x;
            pos.y = pos.y / _rightJoystick.rectTransform.sizeDelta.x;

            _rotateVector = new Vector2(pos.x * 2, pos.y * 2);
            _rotateVector = (_rotateVector.magnitude > 1.0f) ? _rotateVector.normalized : _rotateVector;

            _touchMarker.rectTransform.anchoredPosition = new Vector2(_rotateVector.x * (_rightJoystick.rectTransform.sizeDelta.x / 2), _rotateVector.y * (_rightJoystick.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (_rotateVector.x != 0) return _rotateVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (_rotateVector.y != 0) return _rotateVector.y;
        else return Input.GetAxis("Vertical");
    }
}
