using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGoldenDoors : MonoBehaviour
{
    private GameObject _leftDoor;
    private GameObject _rightDoor;
    private GameObject _goldenDoors;

    private void Start()
    {
        _goldenDoors = gameObject;
        _leftDoor = _goldenDoors.transform.Find("LeftDoor").gameObject;
        _rightDoor = _goldenDoors.transform.Find("RightDoor").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        _leftDoor.transform.Rotate(0, -90, 0);
        _rightDoor.transform.Rotate(0, 90, 0);
    }
}
