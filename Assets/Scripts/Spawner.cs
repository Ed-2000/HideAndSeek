using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static void Spawn(GameObject _spawnedStuff, GameObject[] _locationsOfStuffGO, int _numberOfStuff = 5, GameObject _stuffParent = null)
    {
        List<Transform> _usedLocationsOfStuff = new List<Transform>();

        List<Transform> _locationsOfStuff = new List<Transform>();
        for (int i = 0; i < _locationsOfStuffGO.Length; i++) //заповнюємо список координат спавну артефактів
        {
            _locationsOfStuff.Add(_locationsOfStuffGO[i].transform);
        }

        List<GameObject> _stuff = new List<GameObject>(); //список предметів
        for (int i = 0; i < _numberOfStuff; i++) //створюємо та заносимо артефакти до списку
        {
            GameObject newStuff = Instantiate(_spawnedStuff);

            if (_stuffParent != null)
                newStuff.transform.SetParent(_stuffParent.transform);

            _stuff.Add(newStuff);
        }


        for (int i = 0; i < _numberOfStuff; i++) //заповнюємо список використовуваних позицій значеннями з всіх позицій для артефактів
        {
            int randomArtefactLocation = Random.Range(0, _locationsOfStuff.Count - 1);
            _usedLocationsOfStuff.Add(_locationsOfStuff[randomArtefactLocation]);
            _locationsOfStuff.RemoveAt(randomArtefactLocation);
        }

        for (int i = 0; i < _numberOfStuff; i++)    //задаємо позицію артефактам
        {
            Vector3 newPosition = _usedLocationsOfStuff[i].position;
            _stuff[i].transform.position = newPosition;
        }
    }
}