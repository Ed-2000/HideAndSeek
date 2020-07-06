using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text _fpsText;

    private int _fps;
    private int _iGlobal;
    private float[] _fpsArr;

    private void Start()
    {
        _fpsArr = new float[60];
        _iGlobal = 0;
    }

    private void Update()
    {
        if (_iGlobal < 60)
        {
            _fpsArr[_iGlobal] = (1 / Time.unscaledDeltaTime);
            _iGlobal++;
        }
        else
        {
            _fps = (int)(_fpsArr.Sum()) / _fpsArr.Length;
            _fpsText.text = "fps:" + _fps.ToString();
            _iGlobal = 0;
        }
    }
}