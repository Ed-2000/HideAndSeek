using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{
    public void RotateButton(GameObject button)
    {
        button.transform.Rotate(0, 0, 180);
    }
}
